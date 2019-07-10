using System;
using System.Threading.Tasks;
using Microsoft.Owin;
using Owin;

[assembly: OwinStartup(typeof(Dinazor.Module.NotificationManagement.Hub.Startup))]

namespace Dinazor.Module.NotificationManagement.Hub
{
    public class Startup
    {
        public void Configuration(IAppBuilder app)
        {
             app.MapSignalR();
        }
    }
}
