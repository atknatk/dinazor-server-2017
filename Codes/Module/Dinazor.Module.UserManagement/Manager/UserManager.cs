using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Threading.Tasks;
using Dinazor.Core.Common.Constants;
using Dinazor.Core.Common.Enum;
using Dinazor.Core.Common.Model;
using Dinazor.Core.Dto;
using Dinazor.Core.Interceptor.Attribute;
using Dinazor.Core.Interceptor.Model;
using Dinazor.Core.Interfaces.User;
using Dinazor.Core.Model;

namespace Dinazor.Module.UserManagement.Manager
{
    public class UserManager : DinazorContextBound, IUserManager
    {
        private readonly IUserOperation _userOperation;

        public UserManager(IUserOperation userOperation)
        {
            _userOperation = userOperation;
        }

        public async Task<DinazorResult<List<long>>> GetUserByIdWithRoles(long idUser)
        {
            return await _userOperation.GetUserByIdWithRoles(idUser);
        }

        [DinazorRequired]
        public async Task<DinazorResult> Save([Required]UserDto dto)
        {
            return await _userOperation.Save(dto);
        }

        public async Task<DinazorResult> SaveList(List<UserDto> tList)
        {
            return await _userOperation.SaveList(tList);
        }

        public async Task<DinazorResult> Delete(long id)
        {
            if (id == DinazorConstant.SuperUserId)
            {
                var result = new DinazorResult();
                result.Status = ResultStatus.Unauthorized;
                return result;
            }
            return await _userOperation.Delete(id);
        }

        public async Task<DinazorResult> Update(UserDto dto)
        {
            if (dto.Id == DinazorConstant.SuperUserId)
            {
                var result = new DinazorResult();
                result.Status = ResultStatus.Unauthorized;
                return result;
            }
            return await _userOperation.Update(dto);
        }

        public async Task<DinazorResult<UserDto>> GetById(long id)
        {
            return await _userOperation.GetById(id);
        }

        [Authorization(Role.CameraView)]
        public async Task<DinazorResult<List<UserDto>>> GetAll()
        {
            return await _userOperation.GetAll();
        }

        public Task<DinazorResult<UserDto>> GetAllWithJoin(long id)
        {
            throw new System.NotImplementedException();
        }

        public async Task<DinazorResult<PagingReply<UserDto>>> Paging(PagingRequest pagingRequest)
        {
            return await _userOperation.Paging(pagingRequest);
        }
    }
}
