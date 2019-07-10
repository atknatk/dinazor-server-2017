using Dinazor.Core.Database.Helper;
using Dinazor.Core.Dto.Interfaces;
using Dinazor.Core.Interfaces.Databases;
using Dinazor.Core.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Threading.Tasks;
using LinqKit;

namespace Dinazor.Core.Database.Paging
{

    internal class PagingFilter<TEntity>
        where TEntity : IEntity
    {
        #region Fields

        private readonly PagingRequest _param;
        private readonly IDtoProperty<TEntity>[] _properties;

        #endregion

        #region Constructors and Destructors

        public PagingFilter(PagingRequest param, IDtoProperty<TEntity>[] properties)
        {
            _param = param;
            _properties = properties;
        }

        #endregion

        #region Public Methods and Operators

        public IQueryable<TEntity> Filter(IQueryable<TEntity> entities)
        {
            if (_param.search == null||string.IsNullOrEmpty(_param.search.value))
            {
                return entities;
            }

            ConstantExpression escapedExpression = Expression.Constant(_param.search.value.Escape());

            MethodCallExpression searchExpr = Expression.Call(
                escapedExpression, typeof(string).GetMethod("ToLower", new Type[0]));

            var expressions = new List<Expression<Func<TEntity, bool>>>();

            foreach (var property in _properties)
            {
                MethodCallExpression stringToLowerExpression = Expression.Call(
                    property.ToSqlString.Body, typeof(string).GetMethod("ToLower", new Type[0]));
                MethodCallExpression containsExpression = Expression.Call(
                    stringToLowerExpression, typeof(string).GetMethod("Contains"), new Expression[] { searchExpr });
                LambdaExpression lambdaExpression = Expression.Lambda(
                    containsExpression, property.ToSqlString.Parameters);
                expressions.Add(lambdaExpression as Expression<Func<TEntity, bool>>);
            }

            Expression<Func<TEntity, bool>> resultLambda = expressions.Aggregate((expr1, expr2) => expr1.Or(expr2));
            return entities.Where(resultLambda.Expand());
        }

        #endregion
    }
}
