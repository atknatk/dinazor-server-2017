using System; 
using System.Reflection;
using System.Runtime.Remoting.Messaging;

namespace Dinazor.Core.Interceptor.Attribute
{
    [AttributeUsage(AttributeTargets.Constructor | AttributeTargets.Method | AttributeTargets.Property, AllowMultiple = true)]
    public class InfoLoggerAttribute : System.Attribute
    {

        public void Log(MethodInfo methodInfo, IMethodCallMessage msg)
        {
            var namespacez = "";
            var method = "";
            if (msg.MethodBase.DeclaringType != null)
            {
                namespacez = msg.MethodBase.DeclaringType.FullName;
                method = msg.MethodBase.Name;
            }
            method = namespacez + "." + method;

            //construct the log and write it
        }
    }
}
