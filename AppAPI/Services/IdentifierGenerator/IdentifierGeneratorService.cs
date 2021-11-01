using System;

namespace AppAPI.Services.IdentifierGenerator
{
    public class IdentifierGeneratorService : IIdentifierGeneratorService
    {
        public IServiceActionResult<string> GenerateIdentifier()
        {
            return new ServiceActionResult<string>(Guid.NewGuid().ToString("N"));
        }
    }
}
