using System.Collections.Generic;
using Models;
using Models.Auth;

namespace Data.Models.Auth
{
    public class Permission : BaseEntity
    {
        public PermissionType PermissionTypeId { get; set; }

        public string Title { get; set; }

        public ICollection<Role> Roles { get; set; }
    }
}