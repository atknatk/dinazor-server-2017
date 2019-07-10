using System;
using System.Net;
using System.Net.Http;
using System.Text;
using System.Web.Http.Controllers;
using System.Web.Http.Filters;
using Dinazor.Core.Interfaces.Configuration;
using Dinazor.Core.IoC;
using log4net;

namespace Dinazor.Core.Common.Attributes
{
    public class DinazorBasicAuthenticationAttribute : AuthorizationFilterAttribute
    {
        private static readonly ILog Log = LogManager.GetLogger(System.Reflection.MethodBase.GetCurrentMethod().DeclaringType);

        public override void OnAuthorization(HttpActionContext actionContext)
        {
            if (actionContext.Request.Headers.Authorization == null)
            {
                actionContext.Response = actionContext.Request.CreateResponse(HttpStatusCode.Unauthorized);
            }
            else
            {
                string authenticationString = actionContext.Request.Headers.Authorization.Parameter;
                string originalString = Encoding.UTF8.GetString(Convert.FromBase64String(authenticationString));

                // Get All Usernames and Passwords

                var configurationManager = IocManager.Instance.Resolve<IConfigurationManager>();
                var result = configurationManager.GetValue("BasicAuthenticationUsernamePassword");

                var isAuthenticated = false;

                if (result!=null)
                {
                    foreach (var item in result)
                    {
                        if (item == originalString)
                        {
                            isAuthenticated = true;
                            break;
                        }
                    }

                    if (!isAuthenticated)
                    {
                        Log.Warn("Basic Authentication Username or Password is wrong");
                        actionContext.Response = actionContext.Request.CreateResponse(HttpStatusCode.Unauthorized);
                    }

                }
                else
                {
                    Log.Error("No Basic Authentication Username And Password In Database");
                    actionContext.Response = actionContext.Request.CreateResponse(HttpStatusCode.Unauthorized);
                }
            }
            base.OnAuthorization(actionContext);
        }
    }
}
