using System;
using System.Linq.Expressions;
using Dinazor.Core.Database.Entity.Crm;
using Dinazor.Core.Dto.Abstracts;

namespace Dinazor.Core.Dto.Crm
{
    public class OrganizationDto : UniqueDto<Organization, OrganizationDto>
    {
        public string Name { get; set; }
        public string Vdn { get; set; }

        public override Organization ToEntity()
        {
            return new Organization()
            {
                Id = Id,
                Name = Name,
                Vdn = Vdn
            };
        }

        public override Expression<Func<Organization, OrganizationDto>> Select()
        {
            return l => new OrganizationDto()
            {
                Id = l.Id,
                Name = l.Name,
                Vdn = Vdn
            };
        }

        public override Expression<Func<Organization, bool>> IsExist()
        {
            throw new NotImplementedException();
        }
    }
}
