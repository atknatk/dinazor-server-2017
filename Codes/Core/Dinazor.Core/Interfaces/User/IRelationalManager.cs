
using System.Threading.Tasks;
using Dinazor.Core.Common.Model;

namespace Dinazor.Core.Interfaces.User
{
    public interface IRelationalManager
    {
        Task<DinazorResult> AssignUserToUserGroup(long idUser, long idUserGroup);
        Task<DinazorResult> DetachUserFromUserGroup(long idUser, long idUserGroup);

        Task<DinazorResult> AssignRoleToRoleGroup(long idRole, long idRoleGroup);
        Task<DinazorResult> DetachRoleFromRoleGroup(long idRole, long idRoleGroup);

        Task<DinazorResult> AssignRoleGroupToUserGroup(long idRoleGroup, long idUserGroup);
        Task<DinazorResult> DetachRoleGroupFromUserGroup(long idRoleGroup, long idUserGroup);
    }
}
