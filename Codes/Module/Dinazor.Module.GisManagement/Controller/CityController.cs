using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Web.Http;
using Dinazor.Core.Common.Model;
using Dinazor.Core.Database.Context;
using Dinazor.Core.Dto;
using Dinazor.Core.GenericOperation.Controller;
using Dinazor.Core.Interfaces.Gis;
using Dinazor.Core.IoC;

namespace Dinazor.Module.GisManagement.Controller
{
    public class CityController : DinazorController<ICityManager, CityDto> , IDinazorCrudController<CityDto>, ISaveListController<CityDto>
    {
        private readonly ICityManager _cityManager;

        public CityController()
        {
            _cityManager = IocManager.Instance.Resolve<ICityManager>();
        }

    /*    [HttpGet("{idCity}/Towns")]
        public async Task<DinazorResult<List<TownDto>>> GetTownsByCityId(long idCity)
        {
            return await _cityManager.GetTownsByCityId(idCity);
        }*/

    }
}