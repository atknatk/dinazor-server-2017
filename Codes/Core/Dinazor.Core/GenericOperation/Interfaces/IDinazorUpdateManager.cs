using System.Threading.Tasks;
using Dinazor.Core.Common.Model;
using Dinazor.Core.Dto.Interfaces;

namespace Dinazor.Core.GenericOperation.Interfaces
{
    public interface IDinazorUpdateManager<TDto> : IBaseDinazorManager where TDto : IDto
    {
        Task<DinazorResult> Update(TDto t);
    }
}
