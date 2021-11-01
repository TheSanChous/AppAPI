namespace AppAPI.Services
{
    public class ServiceBase : IService
    {
        public IServiceActionResult Ok()
        {
            return new ServiceActionResult();
        }

        public IServiceActionResult<TResult> Ok<TResult>(TResult value)
        {
            return new ServiceActionResult<TResult>(value);
        }

        public IServiceActionResult Error(string error)
        {
            return new ServiceActionResult(error);
        }

        public IServiceActionResult<TResult> Error<TResult>(string error, TResult value)
        {
            return new ServiceActionResult<TResult>(value, error);
        }
    }
}
