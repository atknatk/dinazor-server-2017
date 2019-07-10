
using System.Collections.Generic;
using System.Threading.Tasks;
using Dinazor.Core.Common.Model;
using Dinazor.Core.Dto.Interfaces; 

namespace Dinazor.Core.GenericOperation.Controller
{
    public interface ISelectWithJoinController<TDto> where TDto : IDto
    {
        Task<DinazorResult<TDto>> GetAllWithJoin( string token, long id,string dummy);
    }
}
