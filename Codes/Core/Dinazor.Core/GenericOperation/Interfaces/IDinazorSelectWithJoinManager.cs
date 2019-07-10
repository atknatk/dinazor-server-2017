
using System.Collections.Generic;
using System.Threading.Tasks;
using Dinazor.Core.Common.Model;
using Dinazor.Core.Dto.Interfaces;

namespace Dinazor.Core.GenericOperation.Interfaces
{
    public interface IDinazorSelectWithJoinManager<TDto> : IBaseDinazorManager where TDto : IDto
    {
        Task<DinazorResult<List<TDto>>> GetAllWithJoin();
    }
}
