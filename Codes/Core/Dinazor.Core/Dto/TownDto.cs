using System;
using System.Linq.Expressions;
using Dinazor.Core.Database.Entity;
using Dinazor.Core.Dto.Abstracts;
using Dinazor.Core.GenericOperation.Interfaces;

namespace Dinazor.Core.Dto
{
    public class TownDto : UniqueDto<Town, TownDto>, IDinazorDeleteControl<Town>
    {
        public Expression<Func<Town, bool>> IsDeletable()
        {
            return l => true;
        }

        public string Name { get; set; }
        public CityDto City { get; set; }


        public override Town ToEntity()
        {
            return new Town()
            {
                Id = Id,
                Name = Name,
                IdCity = City?.Id ?? 0
            };
        }

        public override Expression<Func<Town, TownDto>> Select()
        {
            return l => new TownDto()
            {
                Id = l.Id,
                Name = l.Name
            };
        }

        public override Expression<Func<Town, bool>> IsExist()
        {
            return l => l.Name == Name;
        }
    }
}
