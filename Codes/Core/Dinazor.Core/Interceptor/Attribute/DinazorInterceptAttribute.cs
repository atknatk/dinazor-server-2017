
using System;
using System.Runtime.Remoting.Contexts;
using Dinazor.Core.Interceptor.Property;

namespace Dinazor.Core.Interceptor.Attribute
{
    [AttributeUsage(AttributeTargets.Class, Inherited = true)]
    public class DinazorInterceptAttribute : ContextAttribute
    {
        public DinazorInterceptAttribute() : base("DinazorIntercept")
        {
        }

        public override void GetPropertiesForNewContext(System.Runtime.Remoting.Activation.IConstructionCallMessage ctorMsg)
        {
            ctorMsg.ContextProperties.Add(new InterceptProperty());
        }

        public override bool IsContextOK(Context ctx, System.Runtime.Remoting.Activation.IConstructionCallMessage ctorMsg)
        {
            var ip = ctx.GetProperty("InterceptProperty") as InterceptProperty;
            return ip != null;
        }

        public override bool IsNewContextOK(Context newCtx)
        {
            var ip = newCtx.GetProperty("InterceptProperty") as InterceptProperty;
            return ip != null;
        }

    }
}
