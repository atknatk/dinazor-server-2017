using System.Threading.Tasks;
using System.Web.Http;
using Dinazor.Core.Common.Model;
using Dinazor.Core.Dto;
using Dinazor.Core.GenericOperation.Controller;
using Dinazor.Core.Interfaces.User;
using Dinazor.Core.IoC;

namespace Dinazor.Module.UserManagement.Controller
{
    public class UserController : DinazorController<IUserManager, UserDto>,IDinazorCrudController<UserDto>, 
        IPagingController<UserDto>,
        ISelectWithJoinController<UserDto>,
        ISaveListController<UserDto>
    {
        private readonly IRelationalManager _relationalManager;

        public UserController()
        {
            _relationalManager = IocManager.Instance.Resolve<IRelationalManager>();
        }

        [HttpPost]
        [Route("api/User/{idUser}/UserGroup/{idUserGroup}")]
        public async Task<DinazorResult> AssignUserToUserGroup(string token, long idUser, long idUserGroup)
        {
            return await _relationalManager.AssignUserToUserGroup(idUser, idUserGroup);
        }

        [HttpDelete]
        [Route("api/User/{idUser}/UserGroup/{idUserGroup}")]
        public async Task<DinazorResult> DetachUserFromUserGroup(string token, long idUser, long idUserGroup)
        {
            return await _relationalManager.DetachUserFromUserGroup(idUser, idUserGroup);
        }
    }
}
