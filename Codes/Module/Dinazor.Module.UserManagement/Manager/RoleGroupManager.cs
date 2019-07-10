using System.Collections.Generic;
using System.Threading.Tasks;
using Dinazor.Core.Common.Model;
using Dinazor.Core.Dto;
using Dinazor.Core.Interfaces.User;
using Dinazor.Core.Model;

namespace Dinazor.Module.UserManagement.Manager
{
    public class RoleGroupManager : IRoleGroupManager
    {
        private readonly IRoleGroupOperation _roleGroupOperation;

        public RoleGroupManager(IRoleGroupOperation roleGroupOperation)
        {
            _roleGroupOperation = roleGroupOperation;
        }

        public async Task<DinazorResult> Save(RoleGroupDto roleGroupDto)
        {
            return await _roleGroupOperation.Save(roleGroupDto);
        }

        public Task<DinazorResult> SaveList(List<RoleGroupDto> tList)
        {
            throw new System.NotImplementedException();
        }

        public async Task<DinazorResult> Delete(long id)
        {
            return await _roleGroupOperation.Delete(id);
        }

        public async Task<DinazorResult> Update(RoleGroupDto roleGroupDto)
        {
            return await _roleGroupOperation.Update(roleGroupDto);
        }
        public async Task<DinazorResult<RoleGroupDto>> GetById(long id)
        {
            return await _roleGroupOperation.GetById(id);
        }

        public async Task<DinazorResult<List<RoleGroupDto>>> GetAll()
        {
            return await _roleGroupOperation.GetAll();
        }

        public Task<DinazorResult<RoleGroupDto>> GetAllWithJoin(long id)
        {
            throw new System.NotImplementedException();
        }

        public async Task<DinazorResult<PagingReply<RoleGroupDto>>> Paging(PagingRequest pagingRequest)
        {
            return await _roleGroupOperation.Paging(pagingRequest);
        }

        public async Task<DinazorResult<List<RoleDto>>> GetRoleByIdRoleGroup(long idRoleGroup)
        {
            return await _roleGroupOperation.GetRoleByIdRoleGroup(idRoleGroup);
        }
    }
}
