using System.Globalization;
using System.Web;
using Dinazor.Core.Common.Enum;
using Dinazor.Core.Common.Model;
using Dinazor.Core.Identity;
using Dinazor.Core.Interfaces.Configuration;
using Dinazor.Core.IoC;
using log4net;

namespace Dinazor.Core.Common.AppStart
{
    public static class SessionConfig
    {
        //private static string BaseUrl ;//= "/dinazor";
        //private static string ErrorUrl;// = BaseUrl + "/api/error";
        //private static string AuthorizationUrl;// = BaseUrl + "/api/authorization";

        private static readonly ILog Log = LogManager.GetLogger(System.Reflection.MethodBase.GetCurrentMethod().DeclaringType);

        public static void SessionExecute()
        { 
            string authorizationUrl = "";

            var configurationManager = IocManager.Instance.Resolve<IConfigurationManager>();
            var authorizationUrlResult = configurationManager.GetValue("AuthorizationUrl");
            if (authorizationUrlResult != null && authorizationUrlResult.Count > 0)
            {
                authorizationUrl = authorizationUrlResult[0];
                if (!string.IsNullOrEmpty(authorizationUrl))
                {
                    authorizationUrl = authorizationUrl.ToLower();
                }
            }
            else
            {
                Log.Error("No Authorization Url Found In DB");
            }

            var request = HttpContext.Current.Request;

            // authorization url control

            var url = request.Path.ToLower(new CultureInfo("en-US", false));
            url = url[url.Length - 1] == '/' ? url.Remove(url.Length - 1) : url;
            if (url.EndsWith(authorizationUrl)) return;

            //Control thr URL if it needs Token or not

            var urlNoNeedTokenList = configurationManager.GetValue("UrlNoNeedToken");

            if (urlNoNeedTokenList != null && urlNoNeedTokenList.Count > 0)
            {
                foreach (var item in urlNoNeedTokenList)
                {
                    var reqUrl = item.Split(':')[0].ToLower();
                    var verb = item.Split(':')[1];

                    if (url == reqUrl && request.HttpMethod.ToLower() == verb.ToLower())
                    {
                        return;
                    }
                }
            }

            var token = request.QueryString["token"];
            if (token == null)
            {
                RedirectDinazorResult.RedirectWithData(new DinazorResult()
                {
                    Status = ResultStatus.Unauthorized,
                    Message = "Token information is missing"
                });
                return;
            }

            DinazorPrincipal.AuthenticateUser(token);

            var dinazorPrincipal = (DinazorPrincipal)HttpContext.Current.User;

            if (dinazorPrincipal == null || !dinazorPrincipal.Identity.IsAuthenticated)
            {
                RedirectDinazorResult.RedirectWithData(new DinazorResult()
                {
                    Status = ResultStatus.SessionNotValid,
                    Message = $"Token information is wrong. token is {token}"
                });
            }
        }
    }
}