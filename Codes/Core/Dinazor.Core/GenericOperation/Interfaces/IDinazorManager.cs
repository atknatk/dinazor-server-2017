using System.Collections.Generic;
using System.Threading.Tasks;
using Dinazor.Core.Common.Model;
using Dinazor.Core.Dto.Interfaces;
using Dinazor.Core.Interfaces.IoC;
using Dinazor.Core.Model;

namespace Dinazor.Core.GenericOperation.Interfaces
{
    public interface IBaseDinazorManager : ISingletonDependency
    {

    }

    public interface IDinazorManager<TDto> : IBaseDinazorManager where TDto : IDto
    {
        Task<DinazorResult> Save(TDto t);
        Task<DinazorResult> SaveList(List<TDto> tList);
        Task<DinazorResult> Delete(long id);
        Task<DinazorResult> Update(TDto t);
        Task<DinazorResult<TDto>> GetById(long id);
        Task<DinazorResult<List<TDto>>> GetAll();
        Task<DinazorResult<TDto>> GetAllWithJoin(long id);
        Task<DinazorResult<PagingReply<TDto>>> Paging(PagingRequest pagingRequest);
    }

}
