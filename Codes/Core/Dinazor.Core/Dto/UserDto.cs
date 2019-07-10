
using System;
using System.Linq.Expressions; 
using Dinazor.Core.Dto.Abstracts;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using Dinazor.Core.Database.Entity.User;
using Dinazor.Core.Dto.Interfaces; 

namespace Dinazor.Core.Dto
{
    public class UserDto : UniqueDto<User, UserDto>, IPagingDto<User>
    {
        public string Name { get; set; }
        public string Surname { get; set; }
        public UserGroupDto UserGroup { get; set; }
        [Required(ErrorMessage = "cannotBeNull")]
        public string Username { get; set; }
        public string Mail { get; set; }
        public string Password { get; set; }

        public override User ToEntity()
        {
            return new User()
            {
                Id = Id,
                Username = Username,
                Password = Password,
                Name = Name,
                Surname = Surname,
                Mail = Mail
            };
        }

        public override Expression<Func<User, UserDto>> Select()
        {
            return l => new UserDto()
            {
                Id = l.Id,
                Username = l.Username,
                Password = l.Password,
                Name = l.Name,
                Surname = l.Surname,
                Mail = l.Mail
            };
        }

        public override Expression<Func<User, bool>> IsExist()
        {
            return l => l.Username == Username && !l.IsDeleted;
        }

        public IDtoProperty<User>[] PagingProperty()
        {
            return new IDtoProperty<User>[]
           {
                    new DtoProperty<User, long>(Name(() => Id), l => l.Id),
                    new DtoProperty<User, string>(Name(() => Username), l => l.Username),
                    new DtoProperty<User, string>(Name(() => Password), l => l.Password),
                    new DtoProperty<User, string>(Name(() => Name), l => l.Name),
                    new DtoProperty<User, string>(Name(() => Surname), l => l.Surname),
                    new DtoProperty<User, string>(Name(() => Mail), l => l.Mail)
           };
        }
    }

    public class UserWithRolesDto : UserDto
    {
        public List<RoleDto> Roles { get; set; }
    }


}
