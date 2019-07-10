using System.Collections.Generic;
using System.Threading.Tasks;
using Dinazor.Core.Common.Model;
using Dinazor.Core.Dto.Interfaces;

namespace Dinazor.Core.GenericOperation.Interfaces
{
    public interface IDinazorRetrieveManager<TDto> : IBaseDinazorManager where TDto : IDto
    {
        Task<DinazorResult<TDto>> GetById(long id);
        Task<DinazorResult<List<TDto>>> GetAll();
    }
}
