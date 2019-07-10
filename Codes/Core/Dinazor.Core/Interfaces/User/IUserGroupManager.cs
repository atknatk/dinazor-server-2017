
using System.Collections.Generic;
using System.Threading.Tasks;
using Dinazor.Core.Common.Model;
using Dinazor.Core.Dto;
using Dinazor.Core.GenericOperation.Interfaces;

namespace Dinazor.Core.Interfaces.User
{
    public interface IUserGroupManager : IDinazorManager<UserGroupDto>
    {
        Task<DinazorResult<List<UserDto>>> GetUserByIdUserGroup(long idUserGroup);
        Task<DinazorResult<List<RoleGroupDto>>> GetRoleGroupsByIdUserGroup(long idUserGroup);
    }
}
