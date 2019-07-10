using System.Threading.Tasks;
using Dinazor.Core.Common.Model;
using Dinazor.Core.Dto.Interfaces;

namespace Dinazor.Core.GenericOperation.Interfaces
{
    public interface IDinazorSaveManager<in TDto> : IBaseDinazorManager where TDto : IDto
    {
        Task<DinazorResult> Save(TDto t);
    }
}
