using Dinazor.Core.Common.Model;
using Dinazor.Core.Dto.Interfaces;
using Dinazor.Core.Model; 
using System.Threading.Tasks;

namespace Dinazor.Core.GenericOperation.Interfaces
{
    public interface IDinazorPagingOperatation<TDto>   where TDto : IDto
    {
        Task<DinazorResult<PagingReply<TDto>>> Paging(PagingRequest pagingRequest);
    }
}
