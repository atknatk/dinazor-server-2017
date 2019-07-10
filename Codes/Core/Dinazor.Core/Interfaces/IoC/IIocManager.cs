using System;
using Castle.Windsor;

namespace Dinazor.Core.Interfaces.IoC
{
    public interface IIocManager : IIocRegistrar, IIocResolver, IDisposable
    {
        IWindsorContainer IocContainer { get; }

        new bool IsRegistered(Type type);

        new bool IsRegistered<T>();
    }
}