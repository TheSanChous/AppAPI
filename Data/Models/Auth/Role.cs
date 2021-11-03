using System.Collections.Generic;

namespace Models.Auth
{
    public class Role : BaseEntity
    {
        public string Title { get; set; }

        public ICollection<Data.Models.Auth.Permission> Permissions { get; set; }

        public ICollection<User> Users { get; set; }
    }
}