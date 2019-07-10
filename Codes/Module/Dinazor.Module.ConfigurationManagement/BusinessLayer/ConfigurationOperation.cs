using System.Collections.Generic;
using System.Linq;
using Dinazor.Core.Common.Model;
using Dinazor.Core.Database.Context;
using Dinazor.Core.Dto.Configuration;
using Dinazor.Core.Interfaces.Configuration;

namespace Dinazor.Module.ConfigurationManagement.BusinessLayer
{
    public class ConfigurationOperation : IConfigurationOperation
    {
        public DinazorResult<ConfigurationDto> GetAllConfiguration()
        {
            var result = new DinazorResult<ConfigurationDto>();
            result.Data = new ConfigurationDto();
            
            using (var ctx = new DinazorContext())
            {
                var dataSet = ctx.Configurations.ToList();

                foreach (var item in dataSet)
                {
                    if (result.Data.ConfigurationMap.ContainsKey(item.Key))
                    {
                        (result.Data.ConfigurationMap[item.Key]).Add(item.Value);
                    }
                    else
                    {
                        result.Data.ConfigurationMap[item.Key] = new List<string>();
                        (result.Data.ConfigurationMap[item.Key]).Add(item.Value);
                    }
                }
                result.Success();
            }
            return result;
        }
    }
}
