using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Threading.Tasks;
using Dinazor.Core.Common.Model;
using Dinazor.Core.Database.Context; 
using Dinazor.Core.Database.Entity.User;
using Dinazor.Core.Dto;
using Dinazor.Core.GenericOperation.Abstracts; 
using Dinazor.Core.Interfaces.User;

namespace Dinazor.Module.UserManagement.BusinessLayer
{
    public class UserOperation : SoftDeletableRetrieveOperation<User, UserDto>, IUserOperation
    {
        public async Task<DinazorResult<List<long>>> GetUserByIdWithRoles(long idUser)
        {
            var result = new DinazorResult<List<long>>();
            using (var ctx = new DinazorContext())
            {

                var queryResult = from ruug in ctx.RelUserUserGroup
                    join auth in ctx.Authorization on ruug.IdUserGroup equals auth.IdUserGroup
                    join rrrg in ctx.RelRoleRoleGroup on auth.IdRoleGroup equals rrrg.IdRoleGroup
                    join r in ctx.Role on rrrg.IdRole equals r.Id
                    select r.Id;
                result.Success();
                result.Data = await queryResult.ToListAsync();
            }
            return result;
        }
    }
}