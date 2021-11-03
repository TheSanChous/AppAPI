using System.Collections.Generic;
using Models.Auth;

namespace Data.Models.Auth
{
    public class User : BaseEntity
    {
        public string FirstName { get; set; }

        public string LastName { get; set; }
        
        public string Email { get; set; }

        public string HashedPassword { get; set; }

        public byte[] Salt { get; set; }

        public ICollection<Role> Roles { get; set; }
    }
}
