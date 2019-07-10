using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Threading.Tasks;
using Dinazor.Core.Common.Enum;
using Dinazor.Core.Common.Extensions;
using Dinazor.Core.Common.Model;
using Dinazor.Core.Database.Context;
using Dinazor.Core.Dto.Abstracts;
using Dinazor.Core.GenericOperation.Interfaces;
using Dinazor.Core.Interfaces.Databases;
using Dinazor.Core.Model;
using log4net;
using log4net.Core;
using Newtonsoft.Json;

namespace Dinazor.Core.GenericOperation.Abstracts
{
    public abstract class DinazorOperation<TEntity, TDto> : DinazorOperation<TEntity, TDto, DinazorContext>
        where TEntity : class, IEntity, new()
        where TDto : BaseDto<TEntity, TDto>, new()
    {
    }

    public abstract class DinazorOperation<TEntity, TDto, TContext> : DinazorOperationHelper
        where TEntity : class, IEntity, new()
        where TDto : BaseDto<TEntity, TDto>, new()
        where TContext : DbContext, IDinazorDbContext, new()
    {
        private readonly ILog _log = LogManager.GetLogger(System.Reflection.MethodBase.GetCurrentMethod().DeclaringType);
        private readonly bool _isGreaterThanInfo;

        protected DinazorOperation()
        {
            var logLevel = ((log4net.Repository.Hierarchy.Hierarchy)LogManager.GetRepository()).Root.Level;
            _isGreaterThanInfo = logLevel == Level.All || logLevel == Level.Debug;
        }

        public async Task<DinazorResult> SaveList(List<TDto> dtoList, DinazorSaveListOption option = null)
        {
            var result = new DinazorResult();
            if (dtoList == null || dtoList.Count == 0)
            {
                _log.Warn("Save - Dto List is NULL");
                result.Status = ResultStatus.MissingRequiredParamater;
                return result;
            }

            if (option == null)
            {
                option = new DinazorSaveListOption();
            }

            using (var ctx = new TContext())
            {
                if (option.UseTransaction)
                {
                    using (var tx = ctx.Database.BeginTransaction())
                    {
                        if (option.UseIteration)
                        {
                            for (int i = 0; i < dtoList.Count; i++)
                            {
                                try
                                {
                                    var saveResult = await SaveWithContext(dtoList[i], ctx);
                                    if (!saveResult.IsSuccess)
                                    {
                                        result.AddToPersistErrorList(dtoList[i].ToString(), saveResult);
                                        tx.Rollback();
                                        result.Status = ResultStatus.TransactionRollback;
                                        return result;
                                    }
                                }
                                catch (Exception ex)
                                {
                                    result.Status = ResultStatus.UnknownError;
                                    result.Exception = ex.GetAllMessages();
                                    tx.Rollback();
                                    _log.Error("Save - UnknownError Message : " + ex.GetAllMessages(), ex);
                                    return result;
                                }
                            }
                        }
                        try
                        {
                            tx.Commit();
                            result.Success();
                        }
                        catch (Exception ex)
                        {
                            result.Status = ResultStatus.UnknownError;
                            result.Exception = ex.GetAllMessages();
                            tx.Rollback();
                            _log.Error("Save - UnknownError Message : " + ex.GetAllMessages(), ex);
                            return result;
                        }
                    }
                }
                else
                {
                    if (option.UseIteration)
                    {
                        foreach (var dto in dtoList)
                        {
                            try
                            {
                                var saveResult = await SaveWithContext(dto, ctx);
                                if (!saveResult.IsSuccess)
                                {
                                    result.AddToPersistErrorList(dto.ToString(), saveResult);
                                    result.Status = ResultStatus.AllOfDataNotCommitted;
                                }
                            }
                            catch (Exception ex)
                            {
                                result.Status = ResultStatus.UnknownError;
                                result.Exception = ex.GetAllMessages();
                                _log.Error("Save - UnknownError Message : " + ex.GetAllMessages(), ex);
                                return result;
                            }
                        }
                    }
                }
            }
            return result;
        }

        private async Task<DinazorResult> SaveWithContext(TDto dto, TContext context = null)
        {
            DinazorResult result = new DinazorResult();

            var entity = dto.ToEntity();

            var ctx = context ?? new TContext();

            try
            {
                result = Validate(dto, ctx);

                if (!result.IsSuccess)
                {
                    if (context == null)
                    {
                        ctx.Dispose();
                    }
                    return result;
                }
                bool doRollback;
                ctx.GetDbEntityEntry(entity, DinazorEntityState.Save, out doRollback);


                _log.Info("Save - BeginTransaction()");
                await ctx.SaveChangesAsync();
                result.Success();
                result.ObjectId = entity.Id;
                _log.DebugFormat("Save - Succesful Entity : {0}", _isGreaterThanInfo ? JsonConvert.SerializeObject(entity) : "");

                if (context == null)
                {
                    ctx.Dispose();
                }
            }
            catch (Exception ex)
            {
                result.Status = ResultStatus.UnknownError;
                result.Exception = ex.GetAllMessages();
                _log.Error("Save - UnknownError Message : " + ex.GetAllMessages(), ex);
            }

            return result;
        }

        public async Task<DinazorResult> Save(TDto dto)
        {
            return await SaveWithContext(dto);
        }

        public async Task<DinazorResult> Delete(long id)
        {
            _log.InfoFormat("Delete - Entity : {0} with id : {1} is started", typeof(TEntity).Name, id);
            var result = new DinazorResult();

            var isSoftDeletable = new TEntity() is ISoftDelete;
            _log.InfoFormat("Delete - isDeletable is {0}", isSoftDeletable);

            using (var ctx = new TContext())
            {
                //control the data if it is deletable or not
                var isDeleteControl = new TDto() is IDinazorDeleteControl<TEntity>;
                if (isDeleteControl)
                {
                    var deleteControlFn = (new TDto() as IDinazorDeleteControl<TEntity>).IsDeletable();
                    if (deleteControlFn == null)
                    {
                        _log.Error("Delete - IsDeletable is null");
                        result.Status = Common.Enum.ResultStatus.PropertyNotFound;
                        return result;
                    }

                    bool isDeletable = await ctx.Set<TEntity>().Where(z => z.Id == id).AnyAsync((new TDto() as IDinazorDeleteControl<TEntity>).IsDeletable());
                    if (!isDeletable)
                    {
                        _log.Warn("Delete Control Error");
                        result.Status = ResultStatus.DeleteControlError;
                        return result;
                    }
                }


                var data = await ctx.Set<TEntity>().Where(l => l.Id == id).FirstOrDefaultAsync();
                if (data == null)
                {
                    _log.WarnFormat("Delete - Entity : {0} - id : {1} - NoSuchObject", typeof(TEntity).Name, id);
                    result.Status = Common.Enum.ResultStatus.NoSuchObject;
                    return result;
                }

                if (isSoftDeletable)
                {
                    bool res = (bool)data.GetValue("IsDeleted");
                    if (res)
                    {
                        result.Status = ResultStatus.AlreadyDeleted;
                        return result;
                    }

                    bool setValueResult = data.SetValue("IsDeleted", true);
                    if (!setValueResult)
                    {
                        _log.Error("Delete - IsDeeted Attribute Cannot Be Set");
                    }
                }
                else
                {
                    var entry = ctx.Entry(data);
                    entry.State = EntityState.Deleted;
                }
                try
                {
                    await ctx.SaveChangesAsync();
                    result.Success();
                    _log.InfoFormat("Delete - dbObject.SetValue() is {0}", id);
                }
                catch (Exception e)
                {
                    _log.Error("Delete - Cannnot Be Deleted");
                    result.Status = ResultStatus.UnknownError;
                }
            }
            return result;
        }

        private async Task<DinazorResult> UpdateWithContext(TDto dto, TContext context = null)
        {
            DinazorResult result;
            var ctx = context ?? new TContext();

            result = Validate(dto, ctx);

            if (!result.IsSuccess)
            {
                if (context == null)
                {
                    ctx.Dispose();
                }
                return result;
            }
            var entity = dto.ToEntity();
            try
            {
                bool doRollback = false;

                ctx.GetDbEntityEntry(entity, DinazorEntityState.Update, out doRollback);

                if (doRollback)
                {
                    _log.Warn("Update Bad Data");
                    result.Status = ResultStatus.BadData;
                    return result;
                }

                await ctx.SaveChangesAsync();
                result.Success();
                _log.DebugFormat("Update - Succesful Entity : {0}", _isGreaterThanInfo ? JsonConvert.SerializeObject(entity) : "");

                if (context == null)
                {
                    ctx.Dispose();
                }
            }
            catch (Exception e)
            {
                result.Exception = e.GetAllMessages();
                result.Status = ResultStatus.UnknownError;
            }

            return result;
        }

        public async Task<DinazorResult> Update(TDto dto)
        {
            return await UpdateWithContext(dto);
        }

        public async Task<DinazorResult> SaveOrUpdate(TDto dto)
        {
            DinazorResult result;
            using (var ctx = new TContext())
            {
                result = Validate(dto, ctx);

                if (!result.IsSuccess)
                {
                    return result;
                }
                var entity = dto.ToEntity();
                try
                {
                    bool doRollback;
                    ctx.GetDbEntityEntry(entity, DinazorEntityState.SaveOrUpdate, out doRollback);

                    await ctx.SaveChangesAsync();
                    result.Success();
                    result.ObjectId = entity.Id;
                    _log.DebugFormat("Update - Succesful Entity : {0}", _isGreaterThanInfo ? JsonConvert.SerializeObject(entity) : "");
                }
                catch (Exception e)
                {
                    result.Exception = e.GetAllMessages();
                    result.Status = ResultStatus.UnknownError;
                }
            }
            return result;
        }

        private DinazorResult Validate(TDto dto, TContext ctx)
        {
            _log.InfoFormat("Update has been started");
            var result = new DinazorResult();
            if (dto == null)
            {
                _log.Warn("Update - dto is null");
                result.Status = Common.Enum.ResultStatus.MissingRequiredParamater;
                return result;
            }

            //valid control
            result = dto.IsValid();
            if (!result.IsSuccess)
            {
                _log.InfoFormat("Update - dto is not valid Message : {0}", result.Message);
                return result;
            }

            //unique control
            var uniqueDto = dto as UniqueDto<TEntity, TDto>;

            if (uniqueDto != null)
            {
                var isExistFn = uniqueDto.IsExist();
                if (isExistFn == null)
                {
                    _log.Error("Save - isExistFunc is null");
                    result.Status = Common.Enum.ResultStatus.MissingRequiredParamater;
                    return result;
                }
                // already added control
                bool exists = ctx.Set<TEntity>().Where(l => l.Id != dto.Id).Any(uniqueDto.IsExist());
                if (exists)
                {
                    _log.WarnFormat("Update - Same data already exists.");
                    result.Status = ResultStatus.AlreadyAdded;
                    return result;
                }
            }
            result.Status = ResultStatus.Success;
            return result;
        }

    }
}