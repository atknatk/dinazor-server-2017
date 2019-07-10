

using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Reflection;
using System.Runtime.Remoting.Messaging;
using Dinazor.Core.Common.Enum;
using Dinazor.Core.Common.Model;
using Dinazor.Core.Localization;
using log4net;

namespace Dinazor.Core.Interceptor.Attribute
{
    [AttributeUsage(AttributeTargets.Method)]
    public class DinazorRequiredAttribute : System.Attribute
    {
        private readonly ILog _log = LogManager.GetLogger(System.Reflection.MethodBase.GetCurrentMethod().DeclaringType);

        public DinazorResult CheckRequiredField(MethodInfo methodInfo, IMethodCallMessage msg)
        {
            DinazorResult result = new DinazorResult { Status = ResultStatus.Success };

            //try to find required attributes
            var requiredParameters =
                methodInfo.GetParameters()
                    .Where(
                        l =>
                            l.GetCustomAttributes(false).FirstOrDefault(m => m is RequiredAttribute) != null).ToList();

            foreach (var parameterMessage in requiredParameters)
            {
                var parameter = msg.GetInArg(parameterMessage.Position);
                if (parameter == null)
                {
                    result.Message = parameterMessage.Name;
                    return result;
                }
                if (parameter.GetType().IsPrimitive)
                {
                    continue;
                }

                Type type = parameter.GetType();
                var requiredSubParameters = type.GetProperties().Where(l => l.GetCustomAttributes(false).FirstOrDefault(z => z.GetType() == typeof(RequiredAttribute)) != null).ToList();
                foreach (var propertyInfo in requiredSubParameters)
                {
                    var value = propertyInfo.GetValue(parameter);
                    if (value == null || value.ToString() == "")
                    {
                        string columnName = propertyInfo.Name;
                        string errorMessage = propertyInfo.GetCustomAttributes(typeof(RequiredAttribute), false).Cast<RequiredAttribute>().Single().ErrorMessage;
                        result.Message = LocaleManager.GetMessage(errorMessage, new string[1] { columnName });
                        result.Status =ResultStatus.MissingRequiredParamater;
                        _log.Error("Missing Required Parameter : " +columnName);
                        return result;
                    }
                }
            }
            return result;
        }
    }
}
