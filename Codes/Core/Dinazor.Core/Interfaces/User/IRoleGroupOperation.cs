using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Dinazor.Core.Common.Model; 
using Dinazor.Core.Database.Entity.User;
using Dinazor.Core.Dto;
using Dinazor.Core.GenericOperation.Interfaces;

namespace Dinazor.Core.Interfaces.User
{
    public interface IRoleGroupOperation : IRetrieveOperation<RoleGroup, RoleGroupDto>, IDinazorPagingOperatation<RoleGroupDto>
    {
        Task<DinazorResult<Guid>> GetAuthorizationIdByRoleGroupId(long idRoleGroup);
        Task<DinazorResult<List<RoleDto>>> GetRoleByIdRoleGroup(long idRoleGroup);
    }
}
