using System;
using System.Linq.Expressions; 
using Dinazor.Core.Database.Entity.User;
using Dinazor.Core.Dto.Abstracts;
using Dinazor.Core.Dto.Interfaces;

namespace Dinazor.Core.Dto
{
    public class RoleGroupDto : UniqueDto<RoleGroup, RoleGroupDto>, IPagingDto<RoleGroup>
    {
        public string Name { get; set; }

        public override Expression<Func<RoleGroup, bool>> IsExist()
        {
            return l => l.Name == Name;
        }

        public override Expression<Func<RoleGroup, RoleGroupDto>> Select()
        {
            return l => new RoleGroupDto()
            {
                Id = l.Id,
                Name = l.Name
            };
        }

        public override RoleGroup ToEntity()
        {
            return new RoleGroup()
            {
                Id = Id,
                Name = Name
            };
        }

        public IDtoProperty<RoleGroup>[] PagingProperty()
        {
            return new IDtoProperty<RoleGroup>[]
                    {
                        new DtoProperty<RoleGroup, string>(Name(() => Name), l => l.Name)
                    };
        }
    }
}
