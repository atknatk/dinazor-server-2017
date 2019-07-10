
using System.Collections.Generic;
using Dinazor.Core.Dto.Configuration;
using Dinazor.Core.Interfaces.Configuration;
using Dinazor.Core.IoC;

namespace Dinazor.Module.ConfigurationManagement.Manager
{
    public class ConfigurationManager : IConfigurationManager
    {
        private readonly ConfigurationDto ConfigurationDto;

        public ConfigurationManager()
        {
            var operation = IocManager.Instance.Resolve<IConfigurationOperation>();
            var result = operation.GetAllConfiguration();
            if (result.IsSuccess)
            {
                ConfigurationDto = result.Data;
            }
            else
            {
                //write log
            }
        }

        public List<string> GetValue(string key)
        {
            if (!ConfigurationDto.ConfigurationMap.ContainsKey(key))
            {
                return null;
            }
            return ConfigurationDto.ConfigurationMap[key];
        }

    }
}
