using System.Threading.Tasks;
using Dinazor.Core.Common.Model;
using Dinazor.Core.Dto.Interfaces;
using Dinazor.Core.Model; 

namespace Dinazor.Core.GenericOperation.Controller
{
    public interface IPagingController<TDto> where TDto : IDto
    {
        Task<DinazorResult<PagingReply<TDto>>> Paging(string token,PagingRequest pagingRequest);
    }
}
