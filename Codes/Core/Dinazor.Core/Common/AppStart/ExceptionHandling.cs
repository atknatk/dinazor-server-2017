using System;
using System.Web;
using Dinazor.Core.Common.Enum;
using Dinazor.Core.Common.Model;
using Dinazor.Core.Exceptions;
using log4net;

namespace Dinazor.Core.Common.AppStart
{
    public class ExceptionHandling
    {

        private static readonly ILog Log = LogManager.GetLogger(System.Reflection.MethodBase.GetCurrentMethod().DeclaringType);

        public static void Execute(HttpServerUtility server, HttpResponse response)
        {
            Exception exc = server.GetLastError();

            if (exc.GetType() == typeof(HttpException))
            {

                if (exc.Message.Contains("NoCatch") || exc.Message.Contains("maxUrlLength"))
                    return;

                server.Transfer("HttpErrorPage.aspx");
            }
            else if (exc.GetType() == typeof(DinazorAuthorizationException))
            {
                RedirectDinazorResult.RedirectWithData(new DinazorResult()
                {
                    Status = ResultStatus.Unauthorized,
                    Message = exc.Message
                });
            }
             
            Log.Error(exc); 
            server.ClearError();
        }
    }
}