using System;

namespace AppAPI.Services.IdentifierGenerator
{
    public class IdentifierGeneratorService : IIdentifierGeneratorService
    {
        public string GenerateIdentifier()
        {
            return Guid.NewGuid().ToString("N");
        }
    }
}
