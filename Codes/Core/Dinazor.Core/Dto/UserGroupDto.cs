
using System;
using System.Linq.Expressions; 
using Dinazor.Core.Database.Entity.User;
using Dinazor.Core.Dto.Abstracts;
using Dinazor.Core.Dto.Interfaces; 

namespace Dinazor.Core.Dto
{
    public class UserGroupDto : UniqueDto<UserGroup, UserGroupDto>, IPagingDto<UserGroup>
    { 
        public string Name { get; set; } 
        public string Siktir { get; set; }

        public override UserGroup ToEntity()
        {
            return new UserGroup()
            {
                Id = Id,
                Name =  Name
            };
        }

        public override Expression<Func<UserGroup, UserGroupDto>> Select()
        {
            return l=> new UserGroupDto()
            {
                Id =  l.Id,
                Name = l.Name
            };
        }

        public override Expression<Func<UserGroup, bool>> IsExist()
        {
            return l => l.Name == Name;
        }

        public IDtoProperty<UserGroup>[] PagingProperty()
        {
            return new IDtoProperty<UserGroup>[]
          {
                    //new DtoProperty<UserGroup, long>(Id(() => Id), l => l.Id),
                    new DtoProperty<UserGroup, string>(Name(() => Name), l => l.Name)
          };
        }
    }
}
