namespace AppAPI.Services
{
    public interface IServiceActionResult
    {
        public bool IsOk { get; }

        public string Error { get; set; }
    }

    public interface IServiceActionResult<TResult> : IServiceActionResult
    {
        public TResult Value { get; set; }

    }
}
