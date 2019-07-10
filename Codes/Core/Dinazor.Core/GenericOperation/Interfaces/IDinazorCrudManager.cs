using Dinazor.Core.Dto.Interfaces;

namespace Dinazor.Core.GenericOperation.Interfaces
{
    public interface IDinazorCrudManager<TDto> : IDinazorSaveManager<TDto>,
                                                 IDinazorDeleteManager,
                                                 IDinazorUpdateManager<TDto>,
                                                 IDinazorRetrieveManager<TDto> where TDto : IDto
    {
    }
}
