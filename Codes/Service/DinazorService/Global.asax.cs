using System;
using System.Web.Http.Filters;
using Dinazor.Core.Common.AppStart;
using log4net.Config;
using System.Web.Routing;

namespace DinazorService
{
    public class WebApiApplication : System.Web.HttpApplication
    {
        protected void Application_Start()
        {
            XmlConfigurator.Configure();
            ApplicationStartConfig.Register();
        }

        void Application_Error(object sender, EventArgs e)
        {
            ExceptionHandling.Execute(Server, Response);
        }

        protected void Application_AuthenticateRequest(object sender, EventArgs e)
        {
            SessionConfig.SessionExecute();
        }
         
    } 
}
