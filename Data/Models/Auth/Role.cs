using System.Collections.Generic;
using Data.Models;
using Data.Models.Auth;

namespace AppAPI.Models
{
    public class Role : BaseEntity
    {
        public string Title { get; set; }

        public ICollection<Data.Models.Auth.Permission> Permissions { get; set; }

        public ICollection<User> Users { get; set; }
    }
}