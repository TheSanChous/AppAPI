using System.Collections.Generic;

namespace Models.Auth
{
    public class Role : BaseEntity
    {
        public string Title { get; set; }

        public ICollection<Permission> Permissions { get; set; }

        public ICollection<User> Users { get; set; }
    }
}