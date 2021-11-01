using System;

namespace AppAPI.Services.Special
{
    public class IdentifierGenerator : IIdentifierGenerator
    {
        public IServiceActionResult<string> GenerateIdentifier()
        {
            return new ServiceActionResult<string>(Guid.NewGuid().ToString("N"));
        }
    }
}
