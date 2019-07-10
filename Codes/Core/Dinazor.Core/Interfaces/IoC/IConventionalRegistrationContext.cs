using System.Reflection;
using Dinazor.Core.IoC;

namespace Dinazor.Core.Interfaces.IoC
{
    public interface IConventionalRegistrationContext
    {
        Assembly Assembly { get; }

        IIocManager IocManager { get; }

        ConventionalRegistrationConfig Config { get; }
    }
}