
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using Dinazor.Core.Database.Entity;
using Dinazor.Core.Dto.Abstracts;
using LinqKit;

namespace Dinazor.Core.Dto
{
    public class CityDto : UniqueDto<City, CityDto>
    {
        public string Name { get; set; }

        public ICollection<TownDto> TownList { get; set; }

        public override City ToEntity()
        {
            return new City()
             {
                 Id = Id,
                 Name = Name,
                 TownList = TownList?.Select(l=>l.ToEntity()).ToList()
               /*  TownList = TownList.Select(m=> new Town()
                 {
                     Name = m.Name
                 }).ToList()*/
             };
        }

        public override Expression<Func<City, CityDto>> Select()
        {
            var select = new TownDto().Select().Expand();
            return l => new CityDto()
            {
                Id = l.Id,
                Name = l.Name,
                TownList =  l.TownList.Select(m=> select.Invoke(m)).ToList()
            };
        }

        public override Expression<Func<City, bool>> IsExist()
        {
            return l => l.Name == Name;
        }


    }
}
