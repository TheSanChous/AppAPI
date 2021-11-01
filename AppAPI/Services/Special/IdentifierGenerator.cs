using System;

namespace AppAPI.Services.Special
{
    public class IdentifierGenerator : IIdentifierGenerator
    {
        public string GenerateIdentifier()
        {
            return Guid.NewGuid().ToString("N");
        }
    }
}
