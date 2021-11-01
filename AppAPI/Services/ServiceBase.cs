namespace AppAPI.Services
{
    public class ServiceBase
    {
        protected IServiceActionResult Ok()
        {
            return new ServiceActionResult();
        }

        protected IServiceActionResult<TResult> Ok<TResult>(TResult value)
        {
            return new ServiceActionResult<TResult>(value);
        }

        protected IServiceActionResult Error(string error)
        {
            return new ServiceActionResult(error);
        }

        protected IServiceActionResult<TResult> Error<TResult>(string error, TResult value)
        {
            return new ServiceActionResult<TResult>(value, error);
        }
    }
}