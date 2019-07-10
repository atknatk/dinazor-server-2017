using System;
using System.Linq.Expressions;
using Dinazor.Core.Database.Entity; 
using Dinazor.Core.Dto.Abstracts;
using Dinazor.Core.Dto.Crm;

namespace Dinazor.Core.Dto
{
    public class ModuleDto : UniqueDto<ModuleEnum, ModuleDto>
    {
        public string Name { get; set; }

        public override ModuleEnum ToEntity()
        {
            return new ModuleEnum()
            {
                Id = Id,
                Name = Name
            };
        }

        public override Expression<Func<ModuleEnum, ModuleDto>> Select()
        {
            return l => new ModuleDto()
            {
                Id = l.Id,
                Name = l.Name
            };
        }

        public override Expression<Func<ModuleEnum, bool>> IsExist()
        {
            throw new NotImplementedException();
        }
    }
}
