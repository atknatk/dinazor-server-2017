using System;
using System.Data.Entity;
using System.Linq;
using System.Threading.Tasks;
using Dinazor.Core.Common.Enum;
using Dinazor.Core.Common.Model;
using Dinazor.Core.Database.Context;
using Dinazor.Core.Database.Entity.User;
using Dinazor.Core.Interfaces.User; 

namespace Dinazor.Module.UserManagement.BusinessLayer
{
    public class RelationalOperation: IRelationalOperation
    {

        private readonly IUserOperation _userOperation;
        private readonly IRoleOperation _roleOperation;

        private readonly IUserGroupOperation _userGroupOperation;
        private readonly IRoleGroupOperation _roleGroupOperation;


        public RelationalOperation(IUserOperation userOperation,
            IRoleOperation roleOperation,
            IUserGroupOperation userGroupOperation,
            IRoleGroupOperation roleGroupOperation
        )
        {
            _userOperation = userOperation;
            _roleOperation = roleOperation;
            _userGroupOperation = userGroupOperation;
            _roleGroupOperation = roleGroupOperation;
        }

        #region User User Group

        public async Task<DinazorResult> AssignUserToUserGroup(long idUser, long idUserGroup)
        {
            var result = new DinazorResult();
            // check user id
            var exists = _userOperation.Exists(idUser);
            if (!exists.IsSuccess)
            {
                return result;
            }
            if (!exists.Data)
            {
                result.Status = ResultStatus.NoSuchObject;
                return result;
            }
            //check user group id
            exists = _userGroupOperation.Exists(idUserGroup);
            if (!exists.IsSuccess)
            {
                return result;
            }
            if (!exists.Data)
            {
                result.Status = ResultStatus.NoSuchObject;
                return result;
            }
            //so far so good
            using (var ctx = new DinazorContext())
            {
                //already added
                var alreadyAdded = ctx.RelUserUserGroup.Any(l => l.IdUser == idUser && l.IdUserGroup == idUserGroup);
                if (alreadyAdded)
                {
                    result.Status = ResultStatus.AlreadyAdded;
                    return result;
                }
                RelUserUserGroup userUserGroup = new RelUserUserGroup();
                userUserGroup.IdUser = idUser;
                userUserGroup.IdUserGroup = idUserGroup;

                var willBeAdded = ctx.RelUserUserGroup.Add(userUserGroup);
                try
                {
                    await ctx.SaveChangesAsync();
                    result.Success();
                    //result.ObjectId = willBeAdded;
                }
                catch (Exception e)
                {
                    result.Status = ResultStatus.UnknownError;
                }
            }
            return result;
        }

        public async Task<DinazorResult> DetachUserFromUserGroup(long idUser, long idUserGroup)
        {
            var result = new DinazorResult();
            // check user id
            var exists = _userOperation.Exists(idUser);
            if (!exists.IsSuccess)
            {
                return result;
            }
            if (!exists.Data)
            {
                result.Status = ResultStatus.NoSuchObject;
                return result;
            }
            //check user group id
            exists = _userGroupOperation.Exists(idUserGroup);
            if (!exists.IsSuccess)
            {
                return result;
            }
            if (!exists.Data)
            {
                result.Status = ResultStatus.NoSuchObject;
                return result;
            }
            //so far so good
            using (var ctx = new DinazorContext())
            {
                //already added
                var data = await ctx.RelUserUserGroup.FirstOrDefaultAsync(l => l.IdUser == idUser && l.IdUserGroup == idUserGroup);
                if (data == null)
                {
                    result.Status = ResultStatus.NoSuchObject;
                    return result;
                }
                ctx.RelUserUserGroup.Remove(data);
                try
                {
                    await ctx.SaveChangesAsync();
                    result.Success();
                }
                catch (Exception e)
                {
                    result.Status = ResultStatus.UnknownError;
                }
            }
            return result;
        }

        #endregion


        #region Role Role Group

        public async Task<DinazorResult> AssignRoleToRoleGroup(long idRole, long idRoleGroup)
        {
            var result = new DinazorResult();
            // check role id
            var exists = _roleOperation.Exists(idRole);
            if (!exists.IsSuccess)
            {
                return result;
            }
            if (!exists.Data)
            {
                result.Status = ResultStatus.NoSuchObject;
                return result;
            }
            //check role group id
            exists = _roleGroupOperation.Exists(idRoleGroup);
            if (!exists.IsSuccess)
            {
                return result;
            }
            if (!exists.Data)
            {
                result.Status = ResultStatus.NoSuchObject;
                return result;
            }
            //so far so good
            using (var ctx = new DinazorContext())
            {
                //already added
                var alreadyAdded = ctx.RelRoleRoleGroup.Any(l => l.IdRole == idRole && l.IdRoleGroup == idRoleGroup);
                if (alreadyAdded)
                {
                    result.Status = ResultStatus.AlreadyAdded;
                    return result;
                }
                RelRoleRoleGroup roleRoleGroup = new RelRoleRoleGroup();
                roleRoleGroup.IdRole = idRole;
                roleRoleGroup.IdRoleGroup = idRoleGroup;

                var willBeAdded = ctx.RelRoleRoleGroup.Add(roleRoleGroup);
                try
                {
                    await ctx.SaveChangesAsync();
                    result.Success();
                    //result.ObjectId = willBeAdded;
                }
                catch (Exception e)
                {
                    result.Status = ResultStatus.UnknownError;
                }
            }
            return result;
        }

        public async Task<DinazorResult> DetachRoleFromRoleGroup(long idRole, long idRoleGroup)
        {
            var result = new DinazorResult();
            // check role id
            var exists = _roleOperation.Exists(idRole);
            if (!exists.IsSuccess)
            {
                return result;
            }
            if (!exists.Data)
            {
                result.Status = ResultStatus.NoSuchObject;
                return result;
            }
            //check role group id
            exists = _roleGroupOperation.Exists(idRoleGroup);
            if (!exists.IsSuccess)
            {
                return result;
            }
            if (!exists.Data)
            {
                result.Status = ResultStatus.NoSuchObject;
                return result;
            }

            //so far so good
            using (var ctx = new DinazorContext())
            {
                //already added
                var data = await ctx.RelRoleRoleGroup.FirstOrDefaultAsync(l => l.IdRole == idRole && l.IdRoleGroup == idRoleGroup);
                if (data == null)
                {
                    result.Status = ResultStatus.NoSuchObject;
                    return result;
                }
                ctx.RelRoleRoleGroup.Remove(data);
                try
                {
                    await ctx.SaveChangesAsync();
                    result.Success();
                }
                catch (Exception e)
                {
                    result.Status = ResultStatus.UnknownError;
                }
            }
            return result;
        }

        #endregion

        #region User Group Role Group

        public async Task<DinazorResult> AssignRoleGroupToUserGroup(long idRoleGroup, long idUserGroup)
        {
            var result = new DinazorResult();

            //check role group id
            var exists = _roleGroupOperation.Exists(idRoleGroup);
            if (!exists.IsSuccess)
            {
                return result;
            }
            if (!exists.Data)
            {
                result.Status = ResultStatus.NoSuchObject;
                return result;
            }

            //check user group id
            exists = _userGroupOperation.Exists(idUserGroup);
            if (!exists.IsSuccess)
            {
                return result;
            }
            if (!exists.Data)
            {
                result.Status = ResultStatus.NoSuchObject;
                return result;
            }

            //so far so good
            using (var ctx = new DinazorContext())
            {
                //already added
                var data = await ctx.Authorization.FirstOrDefaultAsync(l => l.IdUserGroup == idUserGroup && l.IdRoleGroup == idRoleGroup);
                if (data != null)
                {
                    result.Status = ResultStatus.AlreadyAdded;
                    return result;
                }
                Authorization authorization = new Authorization();
                authorization.IdUserGroup = idUserGroup;
                authorization.IdRoleGroup = idRoleGroup;
                ctx.Authorization.Add(authorization);
                try
                {
                    await ctx.SaveChangesAsync();
                    result.Success();
                }
                catch (Exception e)
                {
                    result.Status = ResultStatus.UnknownError;
                }
            }
            return result;
        }

        public async Task<DinazorResult> DetachRoleGroupFromUserGroup(long idRoleGroup, long idUserGroup)
        {
            var result = new DinazorResult();

            //check role group id
            var exists = _roleGroupOperation.Exists(idRoleGroup);
            if (!exists.IsSuccess)
            {
                return result;
            }
            if (!exists.Data)
            {
                result.Status = ResultStatus.NoSuchObject;
                return result;
            }

            //check user group id
            exists = _userGroupOperation.Exists(idUserGroup);
            if (!exists.IsSuccess)
            {
                return result;
            }
            if (!exists.Data)
            {
                result.Status = ResultStatus.NoSuchObject;
                return result;
            }

            //so far so good
            using (var ctx = new DinazorContext())
            {
                //already added
                var data = await ctx.Authorization.FirstOrDefaultAsync(l => l.IdUserGroup == idUserGroup && l.IdRoleGroup == idRoleGroup);
                if (data == null)
                {
                    result.Status = ResultStatus.NoSuchObject;
                    return result;
                }
                ctx.Authorization.Remove(data);
                try
                {
                    await ctx.SaveChangesAsync();
                    result.Success();
                }
                catch (Exception e)
                {
                    result.Status = ResultStatus.UnknownError;
                }
            }
            return result;
        }

        #endregion

    }
}