using System.Collections.Generic;
using System.Threading.Tasks;
using Dinazor.Core.Common.Model;
using Dinazor.Core.Dto.Abstracts;
using Dinazor.Core.Interfaces.Databases;

namespace Dinazor.Core.GenericOperation.Interfaces
{
    public interface ISoftDeletableRetrieveOperation<TEntity, TDto>
        : IDinazorOperation<TEntity, TDto>
        where TEntity : class, ISoftDelete, new()
        where TDto : BaseDto<TEntity, TDto>, new()

    {
        Task<DinazorResult<TDto>> GetById(long id);
        Task<DinazorResult<List<TDto>>> GetAll();
        Task<DinazorResult<TDto>> GetAllWithJoin(long id);
        DinazorResult<bool> Exists(long id);
    }
}
