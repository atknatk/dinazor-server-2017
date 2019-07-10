

using System.Collections.Generic;
using System.Threading.Tasks;
using Dinazor.Core.Common.Model;
using Dinazor.Core.Dto;
using Dinazor.Core.Interfaces.User;
using Dinazor.Core.Model;

namespace Dinazor.Module.UserManagement.Manager
{
    public class RoleManager : IRoleManager
    {
        private readonly IRoleOperation _roleOperation;

        public RoleManager(IRoleOperation roleOperation)
        {
            _roleOperation = roleOperation;
        }

        public async Task<DinazorResult<RoleDto>> GetById(long id)
        {
            return await _roleOperation.GetById(id);
        }

        public async Task<DinazorResult<List<RoleDto>>> GetAll()
        {
            return await _roleOperation.GetAll();
        }

        public Task<DinazorResult<RoleDto>> GetAllWithJoin(long id)
        {
            throw new System.NotImplementedException();
        }

        public async Task<DinazorResult<PagingReply<RoleDto>>> Paging(PagingRequest pagingRequest)
        {
            throw new System.NotImplementedException();
        }
        // will not be implemented
        public async Task<DinazorResult> Save(RoleDto t)
        {
            throw new System.NotImplementedException();
        }

        public Task<DinazorResult> SaveList(List<RoleDto> tList)
        {
            throw new System.NotImplementedException();
        }

        public async Task<DinazorResult> Delete(long id)
        {
            throw new System.NotImplementedException();
        }

        public async Task<DinazorResult> Update(RoleDto t)
        {
            throw new System.NotImplementedException();
        }
    }
}
