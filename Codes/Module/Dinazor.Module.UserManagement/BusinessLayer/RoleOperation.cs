
using Dinazor.Core.Database.Entity.User;
using Dinazor.Core.Dto;
using Dinazor.Core.GenericOperation.Abstracts;
using Dinazor.Core.Interfaces.User;

namespace Dinazor.Module.UserManagement.BusinessLayer
{
    public class RoleOperation : RetrieveOperation<Role, RoleDto> , IRoleOperation
    {
    }
}
