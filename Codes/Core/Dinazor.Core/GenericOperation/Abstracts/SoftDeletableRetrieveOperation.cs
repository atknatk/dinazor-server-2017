using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Threading.Tasks;
using System.Linq;
using Dinazor.Core.Common.Enum;
using Dinazor.Core.Common.Model;
using Dinazor.Core.Database.Context;
using Dinazor.Core.Dto.Abstracts;
using Dinazor.Core.GenericOperation.Interfaces;
using Dinazor.Core.Interfaces.Databases; 
using Dinazor.Core.Model;
using Dinazor.Core.Dto.Interfaces;
using Dinazor.Core.Database.Paging;
using log4net;
using log4net.Core;
using LinqKit;

namespace Dinazor.Core.GenericOperation.Abstracts
{
    public abstract class SoftDeletableRetrieveOperation<TEntity, TDto> : SoftDeletableRetrieveOperation<TEntity, TDto, DinazorContext>
        where TEntity : class, ISoftDelete, new()
        where TDto : BaseDto<TEntity, TDto>, new()
    {
    }

    public abstract class SoftDeletableRetrieveOperation<TEntity, TDto, TContext> : DinazorOperation<TEntity, TDto, TContext>,
        ISoftDeletableRetrieveOperation<TEntity, TDto>,
        IDinazorPagingOperatation<TDto>
        where TEntity : class, ISoftDelete, new()
        where TDto : BaseDto<TEntity, TDto>, new()
        where TContext : DbContext, IDinazorDbContext, new()
    {

        private readonly bool _isGreaterThanInfo;

        protected SoftDeletableRetrieveOperation()
        {
            _isGreaterThanInfo = LogLevel == Level.All || LogLevel == Level.Debug;
        }


        private readonly ILog _log = LogManager.GetLogger(System.Reflection.MethodBase.GetCurrentMethod().DeclaringType);

        public async Task<DinazorResult<List<TDto>>> GetAll()
        {
            var result = new DinazorResult<List<TDto>>();
            var selectFn = new TDto().Select();
            if (selectFn == null)
            {
                _log.Error("GetAll - selectFn is null");
                result.Status = ResultStatus.MissingRequiredParamater;
                return result;
            }
           

            using (var ctx = new TContext())
            {
                ShowSql(ctx);
                result.Data = ctx.Set<TEntity>().AsNoTracking().AsExpandable().Select(selectFn).ToList();
                result.Success();
            }
            return result;
        }

        public async Task<DinazorResult<TDto>> GetAllWithJoin(long id)
        {
            var result = new DinazorResult<TDto>();
            TDto dto = new TDto();
            var joinDto = dto as ISelectWithJoinDto<TEntity, TDto>;
            if (joinDto != null)
            {
                var selectFn = joinDto.SelectWithJoin();
                if (selectFn == null)
                {
                    _log.Error("GetAllWithJoin - SelectWithJoin is null");
                    result.Status = ResultStatus.MissingRequiredParamater;
                    return result;
                }
                using (var ctx = new TContext())
                {
                    result.Data = ctx.Set<TEntity>().AsNoTracking().AsExpandable().Where(l => !l.IsDeleted && l.Id == id).Select(selectFn).FirstOrDefault();
                    result.Success();
                }
            }
            return result;
        }

        public async Task<DinazorResult<TDto>> GetById(long id)
        {
            var result = new DinazorResult<TDto>();
            var selectFn = new TDto().Select();
            if (selectFn == null)
            {
                _log.Error("GetAll - selectFn is null");
                result.Status = ResultStatus.MissingRequiredParamater;
                return result;
            }
            using (var ctx = new TContext())
            {
                ShowSql(ctx);
                var tmp = ctx.Set<TEntity>().AsNoTracking().AsExpandable().Where(l => !l.IsDeleted && l.Id == id).Select(selectFn);

                _log.DebugFormat("Select - SQL Query : {0}", tmp.ToString());
                var data = await tmp.FirstOrDefaultAsync();
                if (data == null)
                {
                    result.Status = Common.Enum.ResultStatus.NoSuchObject;
                }
                else
                {
                    result.Data = data;
                    result.Success();
                }
            }
            return result;
        }

        public DinazorResult<bool> Exists(long id)
        {
            var result = new DinazorResult<bool>();
            using (var ctx = new TContext())
            {
                result.Data = ctx.Set<TEntity>().Any(l => !l.IsDeleted && l.Id == id);
                result.Success();
            }
            return result;
        }

        public async Task<DinazorResult<PagingReply<TDto>>> Paging(PagingRequest pagingRequest)
        {
            var result = new DinazorResult<PagingReply<TDto>>();
            TDto dto = new TDto();
            var selectFn = dto.Select();
            if (selectFn == null)
            {
                _log.Error("GetAll - selectFn is null");
                result.Status = ResultStatus.MissingRequiredParamater;
                return result;
            }
            var pagingDto = dto as IPagingDto<TEntity>;
            if (pagingDto == null)
            {
                result.Status = ResultStatus.UnsupportedOperation;
                return result;
            }

            using (var ctx = new TContext())
            {
                ShowSql(ctx);
                var entity = ctx.Set<TEntity>().Where(l => !l.IsDeleted);//.Select(selectFn);
                var parser = new PagingParser<TEntity, TDto>(entity, pagingDto.PagingProperty());
                var data = await parser.ParseAsync(pagingRequest, selectFn);
                result.Data = data;
                result.Success();
            }
            return result;
        }

        //private void ShowSql(TContext ctx)
        //{
        //    if (logLevel == Level.Debug)
        //    {
        //        var serviceProvider = ctx.GetInfrastructure<IServiceProvider>();
        //        var loggerFactory = serviceProvider.GetService<ILoggerFactory>();
        //        loggerFactory.AddProvider(new MyFilteredLoggerProvider());

        //    }
        //}

      
    }
}
