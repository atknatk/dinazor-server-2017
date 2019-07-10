
using System.Threading.Tasks;
using Dinazor.Core.Common.Model;
using Dinazor.Core.Dto.Interfaces;
using Dinazor.Core.Model;

namespace Dinazor.Core.GenericOperation.Interfaces
{
    public interface IDinazorPagingManager<TDto> : IBaseDinazorManager where TDto : IDto
    {
        Task<DinazorResult<PagingReply<TDto>>> Paging(PagingRequest pagingRequest);
    }
}
