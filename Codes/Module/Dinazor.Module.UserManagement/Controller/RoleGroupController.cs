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
    public class RoleGroupController : DinazorController<IRoleGroupManager, RoleGroupDto>,IDinazorCrudController<RoleGroupDto>
    {
        private readonly IRelationalManager _relationalManager;

        public RoleGroupController()
        {
            _relationalManager = IocManager.Instance.Resolve<IRelationalManager>();
        }

        [HttpGet]
        [Route("api/RoleGroup/{idRoleGroup}/Role/")]
        public async Task<DinazorResult<List<RoleDto>>> GetRoleByIdRoleGroup(string token, long idRoleGroup)
        {
            var manager = IocManager.Instance.Resolve<IRoleGroupManager>();
            return await manager.GetRoleByIdRoleGroup(idRoleGroup);
        }

        [HttpPost]
        [Route("api/RoleGroup/{idRoleGroup}/UserGroup/{idUserGroup}")]
        public async Task<DinazorResult> AssignRoleGroupToUserGroup(string token, long idRoleGroup, long idUserGroup)
        {
            return await _relationalManager.AssignRoleGroupToUserGroup(idRoleGroup, idUserGroup);
        }

        [HttpDelete]
        [Route("api/RoleGroup/{idRoleGroup}/UserGroup/{idUserGroup}")]
        public async Task<DinazorResult> DetachRoleGroupFromUserGroup(string token, long idRoleGroup, long idUserGroup)
        {
            return await _relationalManager.DetachRoleGroupFromUserGroup(idRoleGroup, idUserGroup);
        }
    }
} 