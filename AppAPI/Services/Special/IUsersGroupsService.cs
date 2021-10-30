using Models.Auth;
using Models.Species;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace AppAPI.Services.Special
{
    public interface IUsersGroupsService
    {
        public IEnumerable<Group> GetUserGroups(int UserId);
    }
}
