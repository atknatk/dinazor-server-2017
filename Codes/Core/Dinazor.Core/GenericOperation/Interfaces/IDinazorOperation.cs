using System.Collections.Generic;
using System.Threading.Tasks;  
using Dinazor.Core.Common.Model;
using Dinazor.Core.Dto.Interfaces;
using Dinazor.Core.Interfaces.Databases;
using Dinazor.Core.Model;

namespace Dinazor.Core.GenericOperation.Interfaces
{
    public interface IDinazorOperation<TEntity, TDto> where TEntity : IEntity where TDto : IDto
    {
        Task<DinazorResult> Save(TDto dto);
        Task<DinazorResult> SaveOrUpdate(TDto dto);
        Task<DinazorResult> SaveList(List<TDto> dtoList, DinazorSaveListOption option = null);
        Task<DinazorResult> Delete(long id);
        Task<DinazorResult> Update(TDto dto);
    }
}
