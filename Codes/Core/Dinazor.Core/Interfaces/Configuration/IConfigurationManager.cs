
using System.Collections.Generic;

namespace Dinazor.Core.Interfaces.Configuration
{
    public interface IConfigurationManager
    {
        List<string> GetValue(string key);
    }
}
