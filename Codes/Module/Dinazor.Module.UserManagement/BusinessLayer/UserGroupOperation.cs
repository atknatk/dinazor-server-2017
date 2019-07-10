using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Threading.Tasks;
using Dinazor.Core.Common.Enum;
using Dinazor.Core.Common.Model;
using Dinazor.Core.Database.Context; 
using Dinazor.Core.Database.Entity.User;
using Dinazor.Core.Dto;
using Dinazor.Core.GenericOperation.Abstracts;
using Dinazor.Core.Interfaces.User;
using System.Linq;
using LinqKit;

namespace Dinazor.Module.UserManagement.BusinessLayer
{
    public class UserGroupOperation : RetrieveOperation<UserGroup, UserGroupDto> , IUserGroupOperation
    {
        public async Task<DinazorResult<Guid>> GetAuthorizationIdByUserGroupId(long idUserGroup)
        {
            var result = new DinazorResult<Guid>();
            using (var ctx = new DinazorContext())
            {
                var userGroup = await ctx.UserGroup.FirstOrDefaultAsync(l => l.Id == idUserGroup);
                if (userGroup==null)
                {
                    result.Status=ResultStatus.NoSuchObject;
                    return result;
                }
                //result.Data = userGroup.AuthorizationId;
                result.Success();
            }
            return result;
        }

        public async Task<DinazorResult<List<UserDto>>> GetUserByIdUserGroup(long idUserGroup)
        {
            var result = new DinazorResult<List<UserDto>>();

            using (var ctx = new DinazorContext())
            {
                var select = new UserDto().Select().Expand();
                result.Data = ctx.RelUserUserGroup.Where(l => l.IdUserGroup == idUserGroup && !l.User.IsDeleted)
                    .Select(l => l.User).Select(select).ToList();
                result.Success();
            }
            return result;
        }

        public async Task<DinazorResult<List<RoleGroupDto>>> GetRoleGroupsByIdUserGroup(long idUserGroup)
        {
            var result = new DinazorResult<List<RoleGroupDto>>();

            using (var ctx = new DinazorContext())
            {
                var select = new RoleGroupDto().Select().Expand();
                result.Data = ctx.Authorization.Where(l => l.IdUserGroup == idUserGroup).Select(l => l.RoleGroup)
                    .Select(select).ToList();
                result.Success();
            }

            return result;
        }

    }
}
