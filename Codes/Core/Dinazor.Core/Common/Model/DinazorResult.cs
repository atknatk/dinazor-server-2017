using System.Collections.Generic;
using Dinazor.Core.Common.Enum;

namespace Dinazor.Core.Common.Model
{
    public class DinazorResult
    {
        public ResultStatus Status { get; set; }

        public string Message { get; set; }

        public long ObjectId { get; set; }

        public string Exception { get; set; }
        //TODO
        public List<string> ExceptionMessages { get; set; }

        public Dictionary<string,DinazorResult> PersistErrorList { get; set; }


        public DinazorResult()
        {
            Status = ResultStatus.UnknownError;
            ExceptionMessages = new List<string>();
            PersistErrorList = new Dictionary<string, DinazorResult>();
        }

        public DinazorResult Success()
        {
            Status = ResultStatus.Success;
            return this;
        }

        public DinazorResult Success(string message)
        {
            Status = ResultStatus.Success;
            Message = message;
            return this;
        }

        public void AddToPersistErrorList(string identifier,DinazorResult result)
        {
            PersistErrorList.Add(identifier,result);
        }

        public bool IsSuccess => Status == ResultStatus.Success;

    }

    public class DinazorResult<T> : DinazorResult
    {
        public T Data { get; set; }
        public DinazorResult<T> SetData(T data, ResultStatus status = ResultStatus.Success)
        {
            Data = data;
            Status = status;
            return this;
        }
    }
}
