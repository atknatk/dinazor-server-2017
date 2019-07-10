using System.Collections.Generic; 
using System.Linq;
using System.Threading.Tasks;
using Dinazor.Core.Common.Model;
using Dinazor.Core.Database.Context;
using Dinazor.Core.Database.Entity;
using Dinazor.Core.Dto;
using Dinazor.Core.GenericOperation.Abstracts;
using Dinazor.Core.Interfaces.Gis; 

namespace Dinazor.Module.GisManagement.BusinessLayer
{
    public class CityOperation : RetrieveOperation<City, CityDto>, ICityOperation
    {
        public async Task<DinazorResult<List<TownDto>>> GetTownsByCityId(long idCity)
        {
            var result = new DinazorResult<List<TownDto>>();
            using (var ctx = new DinazorContext())
            {
                result.Data = ctx.Town.Where(l => l.City.Id == idCity).Select(new TownDto().Select()).ToList();
                result.Success();
            }
            return result;
        }
    }
}
