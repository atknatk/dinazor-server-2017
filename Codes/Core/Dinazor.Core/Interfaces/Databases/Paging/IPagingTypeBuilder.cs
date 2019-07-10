using System;

namespace Dinazor.Core.Database.Paging
{
    internal interface IPagingTypeBuilder
    {
        IPagingConstructorBuilder WithTypeArguments(Type[] typeArguments);
    }
}