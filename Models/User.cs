using System.Collections.Generic;

namespace Models
{
    public class User
    {
        public int Id { get; set; }

        public string FirstName { get; set; }

        public string LastName { get; set; }
        
        public string Email { get; set; }

        public string HashedPassword { get; set; }

        public byte[] Salt { get; set; }

        public ICollection<Role> Roles { get; set; }
    }
}
