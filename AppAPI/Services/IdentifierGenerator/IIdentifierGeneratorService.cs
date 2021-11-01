namespace AppAPI.Services.IdentifierGenerator
{
    public interface IIdentifierGeneratorService
    {
        public IServiceActionResult<string> GenerateIdentifier();
    }
}
