using System;

namespace AppAPI.Services.IdentifierGenerator
{
    public class IdentifierGeneratorService : ServiceBase, IIdentifierGeneratorService
    {
        public IServiceActionResult<string> GenerateIdentifier()
        {
            return Ok(Guid.NewGuid().ToString("N"));
        }
    }
}
