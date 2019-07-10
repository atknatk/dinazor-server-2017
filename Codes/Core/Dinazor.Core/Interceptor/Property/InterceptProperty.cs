using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Remoting.Contexts;
using System.Runtime.Remoting.Messaging;
using System.Threading.Tasks;

namespace Dinazor.Core.Interceptor.Property
{
    public class InterceptProperty : IContextProperty, IContributeObjectSink
    {
        public string Name => "InterceptProperty";
        public bool IsNewContextOK(Context newCtx)
        {
            var p = newCtx.GetProperty("InterceptProperty") as InterceptProperty;
            return p != null;
        }

        public void Freeze(Context newContext)
        {
        }
        public IMessageSink GetObjectSink(MarshalByRefObject obj, IMessageSink nextSink)
        {
            return new InterceptSink(nextSink, obj);
        }
    }
}
