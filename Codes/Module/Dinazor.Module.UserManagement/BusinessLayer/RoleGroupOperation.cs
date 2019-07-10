using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Threading.Tasks;
using Dinazor.Core.Common.Enum;
using Dinazor.Core.Common.Model;
using Dinazor.Core.Database.Context; 
using Dinazor.Core.Database.Entity.User;
using Dinazor.Core.Dto;
using Dinazor.Core.GenericOperation.Abstracts;
using Dinazor.Core.Interfaces.User;
using LinqKit;

namespace Dinazor.Module.UserManagement.BusinessLayer
{
    public class RoleGroupOperation : RetrieveOperation<RoleGroup, RoleGroupDto>, IRoleGroupOperation
    {
        public async Task<DinazorResult<Guid>> GetAuthorizationIdByRoleGroupId(long idRoleGroup)
        {
            var result = new DinazorResult<Guid>();
            using (var ctx = new DinazorContext())
            {
                var userGroup = await ctx.RoleGroup.FirstOrDefaultAsync(l => l.Id == idRoleGroup);
                if (userGroup == null)
                {
                    result.Status = ResultStatus.NoSuchObject;
                    return result;
                }
              //  result.Data = userGroup.AuthorizationId;
                result.Success();
            }
            return result;
        }

        public async Task<DinazorResult<List<RoleDto>>> GetRoleByIdRoleGroup(long idRoleGroup)
        {
            var result = new DinazorResult<List<RoleDto>>();

            using (var ctx = new DinazorContext())
            {
                var select = new RoleDto().Select().Expand();
                result.Data = ctx.RelRoleRoleGroup.Where(l => l.IdRoleGroup == idRoleGroup)
                    .Select(l => l.Role).Select(select).ToList();
                result.Success();
            }
            return result;
        }
    }
}
