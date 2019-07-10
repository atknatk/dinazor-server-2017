 using Dinazor.Core.Common.Model;

namespace Dinazor.Core.Dto.Interfaces
{
    public interface IDto
    {
        long Id { get; set; }

        DinazorResult IsValid();
    }
}
