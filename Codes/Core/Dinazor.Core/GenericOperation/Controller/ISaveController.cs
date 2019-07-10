using System.Threading.Tasks;
using System.Web.Http;
using Dinazor.Core.Common.Model;
using Dinazor.Core.Dto.Interfaces; 

namespace Dinazor.Core.GenericOperation.Controller
{
    public interface ISaveController<in TDto> where TDto : IDto
    {
        Task<DinazorResult> Save(string token, [FromBody] TDto dto);
    }
}
