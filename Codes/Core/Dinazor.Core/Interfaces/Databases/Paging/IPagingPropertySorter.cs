using System.Linq;

namespace Dinazor.Core.Interfaces.Databases.Paging
{
    public interface IPagingPropertySorter<TEntity>
        where TEntity : IEntity
    {
   
        IOrderedQueryable<TEntity> SortAscending(IQueryable<TEntity> entities);
        IOrderedQueryable<TEntity> SortDescending(IQueryable<TEntity> entities);
        
    }
}
