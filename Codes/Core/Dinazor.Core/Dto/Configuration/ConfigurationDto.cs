using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using Dinazor.Core.Dto.Abstracts;

namespace Dinazor.Core.Dto.Configuration
{
    public class ConfigurationDto : BaseDto<Database.Entity.Configuration.Configuration, ConfigurationDto>
    {
        public ConfigurationDto()
        {
            ConfigurationMap = new Dictionary<string, List<string>>();
        }

        public Dictionary<string, List<string>> ConfigurationMap { get; set; }

        public override Database.Entity.Configuration.Configuration ToEntity()
        {
            throw new NotImplementedException();
        }

        public override Expression<Func<Database.Entity.Configuration.Configuration, ConfigurationDto>> Select()
        {
            throw new NotImplementedException();
        }
    }
}
