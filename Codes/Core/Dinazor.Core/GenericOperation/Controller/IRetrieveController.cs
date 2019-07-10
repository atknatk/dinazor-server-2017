using System.Threading.Tasks; 
using Dinazor.Core.Common.Model;
using System.Collections.Generic;
using Dinazor.Core.Dto.Interfaces;

namespace Dinazor.Core.GenericOperation.Controller
{
    public interface IRetrieveController<TDto> where TDto : IDto
    {
        Task<DinazorResult<TDto>> GetById(string token, long id);
        Task<DinazorResult<List<TDto>>> GetAll( string token);
    }
}
