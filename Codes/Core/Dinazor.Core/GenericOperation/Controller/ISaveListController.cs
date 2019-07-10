using Dinazor.Core.Common.Model;
using System.Collections.Generic;
using System.Threading.Tasks;
using Dinazor.Core.Dto.Interfaces;

namespace Dinazor.Core.GenericOperation.Controller
{
    public interface ISaveListController<TDto> where TDto : IDto
    {
        Task<DinazorResult> SaveList(string token, List<TDto> dtoList);
    }
}
