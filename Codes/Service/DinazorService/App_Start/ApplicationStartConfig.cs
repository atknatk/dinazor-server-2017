using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Web.Http;
using Dinazor.Core.Common.Extensions;
using Dinazor.Core.Interceptor.Attribute;
using Dinazor.Core.IoC.Module;

namespace DinazorService
{
    public class ApplicationStartConfig
    {
        public static void Register()
        {
            GlobalConfiguration.Configure(WebApiConfig.Register);
            GlobalConfiguration.Configuration.Filters.Add(new ExceptionHandlerAttribute());

            var bootstrapper = DinazorBootstrapper.Create<DinazorWebModule>();

            var loadedModules = AppDomain.CurrentDomain.GetAssemblies().Where(l => l.GetName().Name.StartsWith("Dinazor.Module"))
                .SelectMany(GetDinazorModules).ToArray();

            bootstrapper.PluginSources.AddTypeList(loadedModules);
            bootstrapper.Initialize();
        }

        private static List<Type> GetDinazorModules(Assembly moduleType)
        {
            var list = new List<Type>();

            list.AddRange(moduleType.GetTypes().Where(l => l.BaseType == typeof(DinazorModule)));
            return list;
        }
    }
}