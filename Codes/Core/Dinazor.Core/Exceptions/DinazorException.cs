using System;
using Dinazor.Core.Common.Model;

namespace Dinazor.Core.Exceptions
{
    [Serializable]
    public class DinazorException : Exception
    {
        public DinazorResult Result { get; set; }

        public DinazorException()
        {
            
        }

        public DinazorException(string message) : base(message)
        {
        }

        public DinazorException(DinazorResult result) : base(result.Message)
        {
            Result = result;
        }

        public DinazorException(string message, System.Exception innerException) : base(message,innerException)
        {
        }
    }
}
