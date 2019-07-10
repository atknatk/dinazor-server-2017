using System;
using System.Linq.Expressions;
using Dinazor.Core.Interfaces.Databases;

namespace Dinazor.Core.GenericOperation.Interfaces
{
    public interface IDinazorDeleteControl<TEntity> where TEntity : IEntity
    {
        Expression<Func<TEntity, bool>> IsDeletable();
    }
}
