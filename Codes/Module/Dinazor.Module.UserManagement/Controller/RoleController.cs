using System.Threading.Tasks;
using System.Web.Http;
using Dinazor.Core.Common.Model;
using Dinazor.Core.Dto;
using Dinazor.Core.GenericOperation.Controller;
using Dinazor.Core.Interfaces.User;
using Dinazor.Core.IoC;

namespace Dinazor.Module.UserManagement.Controller
{
    public class RoleController : DinazorController<IRoleManager, RoleDto>, IDinazorCrudController<RoleDto>
    {
        private readonly IRelationalManager _relationalManager;

        public RoleController()
        {
            _relationalManager = IocManager.Instance.Resolve<IRelationalManager>();
        }
        
        [HttpPost]
        [Route("api/Role/{idRole}/RoleGroup/{idRoleGroup}")]
        public async Task<DinazorResult> AssignRoleToRoleGroup(string token, long idRole, long idRoleGroup)
        {
            return await _relationalManager.AssignRoleToRoleGroup(idRole, idRoleGroup);
        }

        [HttpDelete]
        [Route("api/Role/{idRole}/RoleGroup/{idRoleGroup}")]
        public async Task<DinazorResult> DetachRoleFromRoleGroup(string token, long idRole, long idRoleGroup)
        {
            return await _relationalManager.DetachRoleFromRoleGroup(idRole, idRoleGroup);
        }
    }
}
