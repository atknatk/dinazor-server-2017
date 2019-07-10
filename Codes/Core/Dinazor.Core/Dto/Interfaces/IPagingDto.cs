using Dinazor.Core.Interfaces.Databases;

namespace Dinazor.Core.Dto.Interfaces
{
    public interface IPagingDto<TEntity> where TEntity : IEntity
    {
        IDtoProperty<TEntity>[] PagingProperty();
    }
}
