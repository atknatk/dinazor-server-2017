namespace Dinazor.Core.Database.Paging
{
    internal interface IPagingConstructorBuilder
    {
        IPagingInstanceBuilder WithConstructorArguments(object[] constructorArguments);

    }
}