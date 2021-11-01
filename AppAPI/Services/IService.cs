namespace AppAPI.Services
{
    public interface IService
    {
        public IServiceActionResult Ok();

        public IServiceActionResult<TResult> Ok<TResult>(TResult value);

        public IServiceActionResult<TResult> Error<TResult>(string error, TResult value);
    }
}
