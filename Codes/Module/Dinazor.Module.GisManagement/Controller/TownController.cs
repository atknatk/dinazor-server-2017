using Dinazor.Core.Dto;
using Dinazor.Core.GenericOperation.Controller;
using Dinazor.Core.Interfaces.Gis;

namespace Dinazor.Module.GisManagement.Controller
{
    public class TownController : DinazorController<ITownManager, TownDto>, IDinazorCrudController<TownDto>
    {
    }
}
