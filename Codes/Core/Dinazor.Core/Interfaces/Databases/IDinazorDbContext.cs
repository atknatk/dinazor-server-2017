using System.Data.Entity;
using Dinazor.Core.Common.Enum;

namespace Dinazor.Core.Interfaces.Databases
{
    public interface IDinazorDbContext
    {
        void GetDbEntityEntry<TEntity>(TEntity entity, DinazorEntityState state,out bool doRollback) where TEntity : class;
    }
}
