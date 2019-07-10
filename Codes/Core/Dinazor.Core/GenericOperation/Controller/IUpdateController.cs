using System.Threading.Tasks;
using System.Web.Http;
using Dinazor.Core.Common.Model;
using Dinazor.Core.Dto.Interfaces; 

namespace Dinazor.Core.GenericOperation.Controller
{
    public interface IUpdateController<in TDto> where TDto : IDto
    {
        Task<DinazorResult> Update(string token, long id, [FromBody] TDto dto);
    }
}
