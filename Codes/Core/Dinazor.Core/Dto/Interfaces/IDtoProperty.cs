using Dinazor.Core.Interfaces.Databases;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Threading.Tasks;

namespace Dinazor.Core.Dto.Interfaces
{
    public interface IDtoProperty<TEntity> where TEntity : IEntity
    {
        #region Public Properties

        string ColumnHeader { get; set; }

        Expression<Func<TEntity, string>> Display { get; set; }

        LambdaExpression SortBy { get; set; }

        Expression<Func<TEntity, string>> ToSqlString { get; set; }

        #endregion
    }
}
