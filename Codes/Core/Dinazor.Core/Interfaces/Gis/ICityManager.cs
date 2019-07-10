using System.Collections.Generic;
using System.Threading.Tasks;
using Dinazor.Core.Common.Model;
using Dinazor.Core.Dto;
using Dinazor.Core.GenericOperation.Interfaces;

namespace Dinazor.Core.Interfaces.Gis
{
    public interface ICityManager : IDinazorManager<CityDto>
    {
        Task<DinazorResult<List<TownDto>>> GetTownsByCityId(long idCity);
    }
}
