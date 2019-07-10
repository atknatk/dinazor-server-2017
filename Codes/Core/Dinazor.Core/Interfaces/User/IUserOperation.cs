using System.Collections.Generic;
using System.Threading.Tasks;
using Dinazor.Core.Common.Model;
using Dinazor.Core.Dto;
using Dinazor.Core.GenericOperation.Interfaces;

namespace Dinazor.Core.Interfaces.User
{
    public interface IUserOperation : ISoftDeletableRetrieveOperation<Database.Entity.User.User, UserDto>, IDinazorPagingOperatation<UserDto>
    {
        Task<DinazorResult<List<long>>> GetUserByIdWithRoles(long idUser);
    }
}
