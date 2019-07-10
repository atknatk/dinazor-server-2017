
using System.Collections.Generic;
using System.Threading.Tasks;
using Dinazor.Core.Common.Model;
using Dinazor.Core.Dto;
using Dinazor.Core.Interfaces.Gis;
using Dinazor.Core.Model;

namespace Dinazor.Module.GisManagement.Manager
{
    public class TownManager : ITownManager
    {
        private readonly ITownOperation _townOperation;
        public TownManager(ITownOperation townOperation)
        {
            _townOperation = townOperation;
        }

        public async Task<DinazorResult<TownDto>> GetById(long id)
        {
            return await _townOperation.GetById(id);
        }

        public async Task<DinazorResult<List<TownDto>>> GetAll()
        {
            return await _townOperation.GetAll();
        }

        public Task<DinazorResult<TownDto>> GetAllWithJoin(long id)
        {
            throw new System.NotImplementedException();
        }

        public Task<DinazorResult<PagingReply<TownDto>>> Paging(PagingRequest pagingRequest)
        {
            throw new System.NotImplementedException();
        }

        #region WillNotBeImplemented
        public async Task<DinazorResult> Save(TownDto t)
        {
            return await _townOperation.Save(t);
        }

        public Task<DinazorResult> SaveList(List<TownDto> tList)
        {
            throw new System.NotImplementedException();
        }

        public async Task<DinazorResult> Delete(long id)
        {
            return await _townOperation.Delete(id);
        }

        public async Task<DinazorResult> Update(TownDto t)
        {
            return await _townOperation.Update(t);
        }

        #endregion
    }
}
