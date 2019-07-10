using System;
using Castle.Core.Logging;
using Castle.MicroKernel.Registration;
using Dinazor.Core.Common.Attributes;
using Dinazor.Core.Common.Helper;
using Dinazor.Core.Interfaces.IoC;
using Dinazor.Core.Plugin;

namespace Dinazor.Core.IoC.Module
{
    public class DinazorBootstrapper : IDisposable
    {
        public Type StartupModule { get; }

        public PluginSourceList PluginSources { get; }

        public IIocManager IocManager { get; }

        protected bool IsDisposed;

        private DinazorModuleManager _moduleManager;
        private ILogger _logger;

        private DinazorBootstrapper([Annotations.NotNullAttribute] Type startupModule)
            : this(startupModule, IoC.IocManager.Instance)
        {

        }

        private DinazorBootstrapper([Annotations.NotNullAttribute] Type startupModule, [Annotations.NotNullAttribute] IIocManager iocManager)
        {
            Check.NotNull(startupModule, nameof(startupModule));
            Check.NotNull(iocManager, nameof(iocManager));

            if (!typeof(DinazorModule).IsAssignableFrom(startupModule))
            {
                throw new ArgumentException($"{nameof(startupModule)} should be derived from {nameof(DinazorModule)}.");
            }

            StartupModule = startupModule;
            IocManager = iocManager;

            PluginSources = new PluginSourceList();
            _logger = NullLogger.Instance;
        }

        public static DinazorBootstrapper Create<TStartupModule>()
            where TStartupModule : DinazorModule
        {
            return new DinazorBootstrapper(typeof(TStartupModule));
        }

        public static DinazorBootstrapper Create<TStartupModule>([Annotations.NotNullAttribute] IIocManager iocManager)
            where TStartupModule : DinazorModule
        {
            return new DinazorBootstrapper(typeof(TStartupModule), iocManager);
        }

        public static DinazorBootstrapper Create([Annotations.NotNullAttribute] Type startupModule)
        {
            return new DinazorBootstrapper(startupModule);
        }

        public static DinazorBootstrapper Create([Annotations.NotNullAttribute] Type startupModule, [Annotations.NotNullAttribute] IIocManager iocManager)
        {
            return new DinazorBootstrapper(startupModule, iocManager);
        }

        public virtual void Initialize()
        {
            ResolveLogger();

            try
            {
                RegisterBootstrapper();
                IocManager.IocContainer.Install(new DinazorCoreInstaller());

                IocManager.Resolve<DinazorPluginManager>().PluginSources.AddRange(PluginSources);

                _moduleManager = IocManager.Resolve<DinazorModuleManager>();
                _moduleManager.Initialize(StartupModule);
                _moduleManager.StartModules();
            }
            catch (System.Exception ex)
            {
                _logger.Fatal(ex.ToString(), ex);
                throw;
            }
        }

        private void ResolveLogger()
        {
            if (IocManager.IsRegistered<ILoggerFactory>())
            {
                _logger = IocManager.Resolve<ILoggerFactory>().Create(typeof(DinazorBootstrapper));
            }
        }

        private void RegisterBootstrapper()
        {
            if (!IocManager.IsRegistered<DinazorBootstrapper>())
            {
                IocManager.IocContainer.Register(
                    Component.For<DinazorBootstrapper>().Instance(this)
                    );
            }
        }
        public virtual void Dispose()
        {
            if (IsDisposed)
            {
                return;
            }
            IsDisposed = true;
            _moduleManager?.ShutdownModules();
        }
    }
}
