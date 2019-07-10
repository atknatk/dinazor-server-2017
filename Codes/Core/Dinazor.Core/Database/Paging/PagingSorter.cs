using Dinazor.Core.Dto.Interfaces;
using Dinazor.Core.Interfaces.Databases;
using Dinazor.Core.Interfaces.Databases.Paging;
using Dinazor.Core.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Threading.Tasks;

namespace Dinazor.Core.Database.Paging
{
    public class PagingSorter<TEntity>
        where TEntity : IEntity
    {

        #region Constants

        private const string Ascending = "asc";

        private const string Descending = "desc";

        #endregion

        #region Fields

        private readonly PagingRequest _param;

        private readonly IDtoProperty<TEntity>[] _properties;

        #endregion

        #region Constructors and Destructors

        public PagingSorter(PagingRequest param, IDtoProperty<TEntity>[] properties)
        {
            _param = param;
            _properties = properties;
        }

        #endregion

        #region Public Methods and Operators

        public IQueryable<TEntity> Sort(IQueryable<TEntity> entities)
        {
            IOrderedQueryable<TEntity> orderedEntities = null;
            if (_param.order == null)
            {
                return entities;
            }
            for (int i = 0; i < _param.order.Length; i++)
            {
                var order = _param.order[i];
                int sortingColumn = order.column;
                string sortingDirection = order.dir;
                IDtoProperty<TEntity> property = _properties[sortingColumn];
                Type keyType = property.SortBy.ReturnType;

                var typeArguments = new[] { typeof(TEntity), keyType };
                IPagingPropertySorter<TEntity> sorter = MakePropertySorter(typeArguments, property.SortBy);
                if (string.IsNullOrEmpty(sortingDirection) || sortingDirection.Equals(Ascending))
                {
                    orderedEntities = orderedEntities != null
                                          ? sorter.SortAscending(orderedEntities)
                                          : sorter.SortAscending(entities);
                }
                else if (sortingDirection.Equals(Descending))
                {
                    orderedEntities = orderedEntities != null
                                          ? sorter.SortDescending(orderedEntities)
                                          : sorter.SortDescending(entities);
                }
            }

            return orderedEntities ?? entities;
        }


        #endregion

        #region Methods

         private static IPagingPropertySorter<TEntity> MakePropertySorter(
            Type[] typeArguments, LambdaExpression sortBy)
        {
            return
                (IPagingPropertySorter<TEntity>)
                PagingGenericTypeHelper.Create(typeof(PagingPropertySorter<,>))
                                          .WithTypeArguments(typeArguments)
                                          .WithConstructorArguments(new object[] { sortBy })
                                          .CreateInstance();
        }

        #endregion

    }
}
