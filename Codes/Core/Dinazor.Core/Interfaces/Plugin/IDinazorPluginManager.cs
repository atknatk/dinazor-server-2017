using Dinazor.Core.Plugin;

namespace Dinazor.Core.Interfaces.Plugin
{
    public interface IDinazorPluginManager
    {
        PluginSourceList PluginSources { get; }
    }
}
