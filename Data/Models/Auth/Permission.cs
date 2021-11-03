using AppAPI.Models;
using Models.Auth;
using System.Collections.Generic;

namespace Data.Models.Auth
{
    public class Permission : BaseEntity
    {
        public PermissionType PermissionTypeId { get; set; }

        public string Title { get; set; }

        public ICollection<Role> Roles { get; set; }
    }
}