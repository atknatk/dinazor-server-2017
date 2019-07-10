using Dinazor.Core.Interfaces.Databases;
using Dinazor.Core.Interfaces.Databases.Paging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Threading.Tasks;
using LinqKit;

namespace Dinazor.Core.Database.Paging
{
    internal class PagingPropertySorter<TEntity, TProperty> : IPagingPropertySorter<TEntity>
       where TEntity : IEntity
    {
        #region Fields

        private readonly Expression<Func<TEntity, TProperty>> _sort;

        #endregion

        #region Constructors and Destructors

        public PagingPropertySorter(Expression<Func<TEntity, TProperty>> sort)
        {
            _sort = sort;
        }

        #endregion

        #region Public Methods and Operators

        public IOrderedQueryable<TEntity> SortAscending(IQueryable<TEntity> entities)
        {
            //return null;
            return entities.OrderBy(_sort.Expand());
        }

           public IOrderedQueryable<TEntity> SortDescending(IQueryable<TEntity> entities)
        {
           // return null;
            return entities.OrderByDescending(_sort.Expand());
        }

        #endregion
    }
}
