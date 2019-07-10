using Dinazor.Core.Dto.Interfaces;

namespace Dinazor.Core.GenericOperation.Controller
{
    public interface IDinazorCrudController<TDto> : ISaveController<TDto>,
                                          IDeleteController,
                                          IUpdateController<TDto>,
                                          IRetrieveController<TDto>
        where TDto : IDto
    {
    }
}
