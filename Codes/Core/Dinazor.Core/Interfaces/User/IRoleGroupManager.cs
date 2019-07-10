using System.Collections.Generic;
using System.Threading.Tasks;
using Dinazor.Core.Common.Model;
using Dinazor.Core.Dto;
using Dinazor.Core.GenericOperation.Interfaces;

namespace Dinazor.Core.Interfaces.User
{
    public interface IRoleGroupManager : IDinazorManager<RoleGroupDto>
    {
        Task<DinazorResult<List<RoleDto>>> GetRoleByIdRoleGroup(long idRoleGroup);
    }
}
