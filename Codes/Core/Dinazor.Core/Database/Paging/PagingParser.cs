using Dinazor.Core.Dto.Interfaces;
using Dinazor.Core.Interfaces.Databases;
using Dinazor.Core.Model;
using System; 
using System.Data.Entity;
using System.Linq;
using System.Linq.Expressions;
using System.Threading.Tasks;

namespace Dinazor.Core.Database.Paging
{
    public class PagingParser<TEntity, TDto>
        where TEntity : IEntity
        where TDto : IDto
    {
        #region Fields

        private readonly IDtoProperty<TEntity>[] _properties;

        private IQueryable<TEntity> _entities;

        #endregion

        #region Constructors and Destructors

        public PagingParser(IQueryable<TEntity> entities, IDtoProperty<TEntity>[] properties)
        {
            _entities = entities;
            _properties = properties;
        }
        #endregion

        #region Public Methods and Operators

        public async Task<PagingReply<TDto>> ParseAsync(PagingRequest param, Expression<Func<TEntity, TDto>> selectFn)
        {
            int totalRecords = _entities.Count();
            _entities = Sort(param);
            _entities = FilterGlobal(param);
            _entities = FilterSpecific(param);
            int displayRecords = _entities.Count();
            _entities = _entities.Skip(param.start);
            _entities = _entities.Take(param.length);
            var data = await _entities.Select(selectFn).ToListAsync();

            PagingReply<TDto> reply = new PagingReply<TDto>()
            {
                data = data,
                draw = param.draw,
                recordsTotal = totalRecords,
                recordsFiltered = displayRecords
            };

            return reply;
        }


        #endregion

        #region Methods

        private IQueryable<TEntity> FilterGlobal(PagingRequest param)
        {
            var filter = new PagingFilter<TEntity>(param, _properties);
            return filter.Filter(_entities);
        }

        private IQueryable<TEntity> FilterSpecific(PagingRequest param)
        {
            var filter = new PagingPropertyFilter<TEntity>(param, _properties);
            return filter.Filter(_entities);
        }

        private IQueryable<TEntity> Sort(PagingRequest param)
        {
            var sorter = new PagingSorter<TEntity>(param, _properties);
            return sorter.Sort(_entities);
        }
        #endregion

    }
}
