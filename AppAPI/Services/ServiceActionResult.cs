namespace AppAPI.Services
{
    public class ServiceActionResult<TResult> : IServiceActionResult<TResult>
    {
        protected string error;

        public TResult Value { get; set; }

        public bool IsOk { get; protected set; }

        public string Error {
            get
            {
                return error;
            }
            set
            {
                error = value;
                IsOk = error is null ? true : false;
            }
        }

        public ServiceActionResult(TResult value, string error = null)
        {
            Value = value;
            IsOk = true;
            Error = error;
        }
    }

    public class ServiceActionResult : IServiceActionResult
    {
        protected string error;

        public bool IsOk { get; protected set; }

        public string Error
        {
            get
            {
                return error;
            }
            set
            {
                error = value;
                IsOk = error is null ? true : false;
            }
        }

        public ServiceActionResult(string error = null)
        {
            IsOk = true;
            Error = error;
        }
    }
}
