
using System.Threading.Tasks;
using Dinazor.Core.Common.Model;
using Dinazor.Core.Interfaces.User;

namespace Dinazor.Module.UserManagement.Manager
{
    public class RelationalManager : IRelationalManager
    {
        private readonly IRelationalOperation _relationalOperation;

        public RelationalManager(IRelationalOperation relationalOperation)
        {
            _relationalOperation = relationalOperation;
        }

        public async Task<DinazorResult> AssignUserToUserGroup(long idUser, long idUserGroup)
        {
            return await _relationalOperation.AssignUserToUserGroup(idUser, idUserGroup);
        }

        public async Task<DinazorResult> DetachUserFromUserGroup(long idUser, long idUserGroup)
        {
            return await _relationalOperation.DetachUserFromUserGroup(idUser,idUserGroup);
        }

        public async Task<DinazorResult> AssignRoleToRoleGroup(long idRole, long idRoleGroup)
        {
            return await _relationalOperation.AssignRoleToRoleGroup(idRole, idRoleGroup);
        }

        public async Task<DinazorResult> DetachRoleFromRoleGroup(long idRole, long idRoleGroup)
        {
            return await _relationalOperation.DetachRoleFromRoleGroup(idRole,idRoleGroup);
        }

        public async Task<DinazorResult> AssignRoleGroupToUserGroup(long idRoleGroup, long idUserGroup)
        {
            return await _relationalOperation.AssignRoleGroupToUserGroup(idRoleGroup,idUserGroup);
        }

        public async Task<DinazorResult> DetachRoleGroupFromUserGroup(long idRoleGroup, long idUserGroup)
        {
            return await _relationalOperation.DetachRoleGroupFromUserGroup(idRoleGroup,idUserGroup);
        }
    }
}
