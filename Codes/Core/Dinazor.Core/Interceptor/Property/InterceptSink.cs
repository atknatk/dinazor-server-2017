using System;
using System.Linq;
using System.Reflection;
using System.Runtime.Remoting.Messaging;
using System.Security;
using System.Threading.Tasks;
using Dinazor.Core.Common.Enum;
using Dinazor.Core.Common.Model;
using Dinazor.Core.Exceptions;
using Dinazor.Core.Interceptor.Attribute;
using log4net;

namespace Dinazor.Core.Interceptor.Property
{
    public class InterceptSink : IMessageSink
    {
        protected readonly ILog Log = LogManager.GetLogger(System.Reflection.MethodBase.GetCurrentMethod().DeclaringType);
        private readonly MarshalByRefObject _obj;

        public InterceptSink(IMessageSink nextSink, MarshalByRefObject obj)
        {
            NextSink = nextSink;
            _obj = obj;
        }

        #region IMessageSink Members
        public IMessageSink NextSink { get; }

        [SecurityCritical]
        public IMessage SyncProcessMessage(IMessage msg)
        {
            var watch = System.Diagnostics.Stopwatch.StartNew();

            var mcm = (IMethodCallMessage)msg;
            var method = ((MethodInfo)mcm.MethodBase);

            var methodName = method.Name;
            var className = string.Empty;
            if (method.DeclaringType != null)
            {
                className = method.DeclaringType.Name;
            }
            Log.DebugFormat("[{0}] Execution for {1}", className, methodName);

            var result = PreProcess(ref mcm);
            if (!result.IsSuccess)
            {
                Type returnType = method.ReturnType;

                var genericReturnType = returnType.GenericTypeArguments[0];
                DinazorResult instance = (DinazorResult)Activator.CreateInstance(genericReturnType);
                instance.Message = result.Message;
                instance.Status = result.Status;

                var task = Task.Factory.StartNew(() => instance);
                var task2 = Convert.ChangeType(task, returnType);

                return new ReturnMessage(task2, null, 0, null, mcm);
            }

            var rtnMsg = NextSink.SyncProcessMessage(msg);
            var mrm = (rtnMsg as IMethodReturnMessage);

            PostProcess((IMethodCallMessage)msg, ref mrm);

            watch.Stop();
            Log.DebugFormat("[{0}] Executed Method for {1} time : {2} ms", className, methodName, watch.ElapsedMilliseconds);
            return mrm;
        }

        [SecurityCritical]
        public IMessageCtrl AsyncProcessMessage(IMessage msg, IMessageSink replySink)
        {
            var rtnMsgCtrl = NextSink.AsyncProcessMessage(msg, replySink);
            return rtnMsgCtrl;
        }

        #endregion

        private DinazorResult PreProcess(ref IMethodCallMessage msg)
        {
            Type type = Type.GetType(_obj.GetType().FullName);
            foreach (var asm in AppDomain.CurrentDomain.GetAssemblies())
            {
                type = asm.GetType(_obj.GetType().FullName);
                if (type != null)
                {
                    break;
                }
            }

            ParameterInfo[] paramInfoArr = msg.MethodBase.GetParameters();
            Type[] typeArr = new Type[paramInfoArr.Length];
            int ii = 0;
            foreach (var item in paramInfoArr)
            {
                typeArr[ii] = item.ParameterType;
                ii++;
            }

            MethodInfo methodInfo = null;
            if (type != null)
            {
                methodInfo = type.GetMethod(msg.MethodName, typeArr);
            }
            var result = new DinazorResult().Success();

            // Authorization
            if (methodInfo == null)
            {
                result.Status = ResultStatus.UnknownError;
                return result;
            }
            var authorizations = (AuthorizationAttribute[])methodInfo.GetCustomAttributes(typeof(AuthorizationAttribute), true);
            if (authorizations.Select(t => t.IsAuthorizated()).Any(isSuccess => !isSuccess))
            {
                throw new DinazorAuthorizationException(new DinazorResult()
                {
                    Status = ResultStatus.Unauthorized,
                    Message = "unauthorized"
                });
            }

            //required
            DinazorRequiredAttribute[] required = (DinazorRequiredAttribute[])methodInfo.GetCustomAttributes(typeof(DinazorRequiredAttribute), true);
            for (int i = 0; i < required.Length; i++)
            {
                result = required[i].CheckRequiredField(methodInfo, msg);
                if (!result.IsSuccess)
                {
                    return result;
                }
            }

            //info logger
            var info = (InfoLoggerAttribute[])methodInfo.GetCustomAttributes(typeof(InfoLoggerAttribute), true);
            foreach (var t in info)
            {
                t.Log(methodInfo, msg);
            }

            //Pre Process
            /*    var attrs = (PreProcessAttribute[])msg.MethodBase.GetCustomAttributes(typeof(PreProcessAttribute), true);
                foreach (var t in attrs)
                {
                    result = t.Processor.Process(ref msg);
                    if (!result.IsSuccess)
                    {
                        return result;
                    }
                }*/
            return result;
        }

        private static void PostProcess(IMethodCallMessage callMsg, ref IMethodReturnMessage rtnMsg)
        {
            /* var attrs = (PostProcessAttribute[])callMsg.MethodBase.GetCustomAttributes(typeof(PostProcessAttribute), true);
             foreach (var t in attrs)
             {
                 t.Processor.Process(callMsg, ref rtnMsg);
             }*/
        }
    }
}
