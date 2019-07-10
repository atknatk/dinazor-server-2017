using System;
using System.Linq.Expressions;
using Dinazor.Core.Dto.Abstracts;
using Dinazor.Core.Interfaces.Databases;

namespace Dinazor.Core.Dto.Interfaces
{
    public interface ISelectWithJoinDto<TEntity, TDto>
        where TEntity : IEntity
        where TDto : BaseDto<TEntity, TDto>
    {
        Expression<Func<TEntity, TDto>> SelectWithJoin();
    }
}
