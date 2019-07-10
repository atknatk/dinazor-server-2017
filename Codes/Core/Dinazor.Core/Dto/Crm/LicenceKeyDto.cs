using System;
using System.Linq.Expressions;
using Dinazor.Core.Database.Entity.Crm;
using Dinazor.Core.Dto.Abstracts;

namespace Dinazor.Core.Dto.Crm
{
    public class LicenceKeyDto : UniqueDto<LicenceKey, LicenceKeyDto>
    {
        public string Key { get; set; }

        public override LicenceKey ToEntity()
        {
            return new LicenceKey()
            {
                Id = Id,
                Key = Key
            };
        }

        public override Expression<Func<LicenceKey, LicenceKeyDto>> Select()
        {
            return l => new LicenceKeyDto()
            {
                Id = l.Id,
                Key = l.Key
            };
        }

        public override Expression<Func<LicenceKey, bool>> IsExist()
        {
            throw new NotImplementedException();
        }
    }
}
