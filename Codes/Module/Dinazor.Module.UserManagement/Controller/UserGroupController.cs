using System.Collections.Generic;
using System.Threading.Tasks;
using System.Web.Http;
using Dinazor.Core.Common.Model;
using Dinazor.Core.Dto;
using Dinazor.Core.GenericOperation.Controller;
using Dinazor.Core.Interfaces.User;
using Dinazor.Core.IoC;

namespace Dinazor.Module.UserManagement.Controller
{
    public class UserGroupController : DinazorController<IUserGroupManager, UserGroupDto>,IDinazorCrudController<UserGroupDto>
    {
        [HttpGet]
        [Route("api/UserGroup/{idUserGroup}/User")]
        public async Task<DinazorResult<List<UserDto>>> GetUserByIdUserGroup(string token, long idUserGroup)
        {
            var manager = IocManager.Instance.Resolve<IUserGroupManager>();
            return await manager.GetUserByIdUserGroup(idUserGroup);
        }

        [HttpGet]
        [Route("api/UserGroup/{idUserGroup}/RoleGroup")]
        public async Task<DinazorResult<List<RoleGroupDto>>> GetRoleGroupsByIdUserGroup(string token, long idUserGroup)
        {
            var manager = IocManager.Instance.Resolve<IUserGroupManager>();
            return await manager.GetRoleGroupsByIdUserGroup(idUserGroup);
        }
    }
}
