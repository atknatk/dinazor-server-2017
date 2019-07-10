using Dinazor.Core.Database.Entity;
using Dinazor.Core.Database.Entity.User;
using Dinazor.Core.Dto;
using Dinazor.Core.GenericOperation.Interfaces;

namespace Dinazor.Core.Interfaces.User
{
    public interface IRoleOperation : IRetrieveOperation<Role, RoleDto>, IDinazorPagingOperatation<RoleDto>
    {
    
    }
}
