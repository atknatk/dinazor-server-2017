
using Dinazor.Core.Common.Model;
using Dinazor.Core.Dto.Configuration;

namespace Dinazor.Core.Interfaces.Configuration
{
    public interface IConfigurationOperation
    {
        DinazorResult<ConfigurationDto> GetAllConfiguration();

    }
}
