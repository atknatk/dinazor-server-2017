using System.Web;
using Dinazor.Core.Common.Model;
using Newtonsoft.Json;

namespace Dinazor.Core.Common.AppStart
{
    public class RedirectDinazorResult
    { 
        public static void RedirectWithData(DinazorResult result)
        {
            var data = result != null ? JsonConvert.SerializeObject(result) : string.Empty;
            HttpContext.Current.Response.Clear();
            HttpContext.Current.Response.Buffer = true;
            HttpContext.Current.Response.Charset = "utf-8";
            HttpContext.Current.Response.ContentEncoding = System.Text.Encoding.UTF8;
            HttpContext.Current.Response.ContentType = "application/json";
            HttpContext.Current.Response.AddHeader("content-length", data.Length.ToString());
            HttpContext.Current.Response.Write(data);
            HttpContext.Current.Response.End();
        }
    }
}