using System;
using System.Linq.Expressions; 
using Dinazor.Core.Database.Entity.User;
using Dinazor.Core.Dto.Abstracts;
using Dinazor.Core.Dto.Interfaces;

namespace Dinazor.Core.Dto
{
    public class RoleDto : UniqueDto<Role, RoleDto>, IPagingDto<Role>
    {
        public string Name { get; set; }

        public override Expression<Func<Role, bool>> IsExist()
        {
            return l => l.Name == Name;
        }

        public IDtoProperty<Role>[] PagingProperty()
        {
            return new IDtoProperty<Role>[]
                    {
                        new DtoProperty<Role, string>(Name(() => Name), l => l.Name)
                    };
        }

        public override Expression<Func<Role, RoleDto>> Select()
        {
            return l => new RoleDto()
            {
                Id = l.Id,
                Name = l.Name
            };
        }

        public override Role ToEntity()
        {
            return new Role()
            {
                Id = Id,
                Name = Name
            };
        }
    }
}
