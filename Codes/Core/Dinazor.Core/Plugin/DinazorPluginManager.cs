using Dinazor.Core.Interfaces.Plugin;

namespace Dinazor.Core.Plugin
{
    public class DinazorPluginManager : IDinazorPluginManager
    {
        public PluginSourceList PluginSources { get; }

        public DinazorPluginManager()
        {
            PluginSources = new PluginSourceList();
        }
    }
}
