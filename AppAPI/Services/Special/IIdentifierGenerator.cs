namespace AppAPI.Services.Special
{
    public interface IIdentifierGenerator
    {
        public IServiceActionResult<string> GenerateIdentifier();
    }
}
