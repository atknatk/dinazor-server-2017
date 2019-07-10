using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Dinazor.Core.Common.Enum;
using Dinazor.Core.Common.Model;
using Dinazor.Core.Dto;
using Dinazor.Core.Interceptor.Attribute;
using Dinazor.Core.Interfaces.Gis;
using Dinazor.Core.Model;

namespace Dinazor.Module.GisManagement.Manager
{
    public class CityManager : ICityManager
    {
        private readonly ICityOperation _cityOperation;

        public CityManager(ICityOperation cityOperation)
        {
            _cityOperation = cityOperation;
        }

        public async Task<DinazorResult<CityDto>> GetById(long id)
        {
            return await _cityOperation.GetById(id);
        }

       
        public async Task<DinazorResult<List<CityDto>>> GetAll()
        {
            return await _cityOperation.GetAll();
        }

        public async Task<DinazorResult<List<TownDto>>> GetTownsByCityId(long idCity)
        {
            return await _cityOperation.GetTownsByCityId(idCity);
        }

        #region WillNotBeImplemented

        public async Task<DinazorResult> Save(CityDto t)
        {
            return await _cityOperation.Save(t);
        }

        public async Task<DinazorResult> Delete(long id)
        {
            return await _cityOperation.Delete(id);
        }

        public Task<DinazorResult> Update(CityDto t)
        {
            throw new System.NotImplementedException();
        }

        public async Task<DinazorResult> SaveList(List<CityDto> tList)
        {
            return await _cityOperation.SaveList(tList);
        }

        public Task<DinazorResult<CityDto>> GetAllWithJoin(long id)
        {
            throw new NotImplementedException();
        }

        public Task<DinazorResult<PagingReply<CityDto>>> Paging(PagingRequest pagingRequest)
        {
            throw new NotImplementedException();
        }

        #endregion
    }
}
