using Dinazor.Core.Common.Model;

namespace Dinazor.Core.Exceptions
{
    public class DinazorAuthorizationException : DinazorException
    {
        public DinazorAuthorizationException()
        {

        }

        public DinazorAuthorizationException(string message) : base(message)
        {
        }

        public DinazorAuthorizationException(DinazorResult result) : base(result.Message)
        {
            Result = result;
        }
        public DinazorAuthorizationException(string message, System.Exception innerException) : base(message,innerException)
        {
        }
    }
}
