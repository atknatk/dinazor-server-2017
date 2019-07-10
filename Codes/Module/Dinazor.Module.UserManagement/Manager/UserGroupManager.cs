using System.Collections.Generic;
using System.Threading.Tasks;
using Dinazor.Core.Common.Model;
using Dinazor.Core.Dto;
using Dinazor.Core.Interfaces.User;
using Dinazor.Core.Model;

namespace Dinazor.Module.UserManagement.Manager
{
    public class UserGroupManager : IUserGroupManager
    {
        private readonly IUserGroupOperation _userGroupOperation;

        public UserGroupManager(IUserGroupOperation userGroupOperation)
        {
            _userGroupOperation = userGroupOperation;
        }

        public async Task<DinazorResult> Save(UserGroupDto userGroupDto)
        {
            return await _userGroupOperation.Save(userGroupDto);
        }

        public Task<DinazorResult> SaveList(List<UserGroupDto> tList)
        {
            throw new System.NotImplementedException();
        }

        public async Task<DinazorResult> Delete(long id)
        {
            return await _userGroupOperation.Delete(id);
        }
        public async Task<DinazorResult> Update(UserGroupDto userGroupDto)
        {
            return await _userGroupOperation.Update(userGroupDto);
        }
        public async Task<DinazorResult<UserGroupDto>> GetById(long id)
        {
            return await _userGroupOperation.GetById(id);
        }

        public async Task<DinazorResult<List<UserGroupDto>>> GetAll()
        {
            return await _userGroupOperation.GetAll();
        }

        public Task<DinazorResult<UserGroupDto>> GetAllWithJoin(long id)
        {
            throw new System.NotImplementedException();
        }

        public async Task<DinazorResult<PagingReply<UserGroupDto>>> Paging(PagingRequest pagingRequest)
        {
            return await _userGroupOperation.Paging(pagingRequest);
        }

        public async Task<DinazorResult<List<UserDto>>> GetUserByIdUserGroup(long idUserGroup)
        {
            return await _userGroupOperation.GetUserByIdUserGroup(idUserGroup);
        }

        public async Task<DinazorResult<List<RoleGroupDto>>> GetRoleGroupsByIdUserGroup(long idUserGroup)
        {
            return await _userGroupOperation.GetRoleGroupsByIdUserGroup(idUserGroup);
        }
    }
}
