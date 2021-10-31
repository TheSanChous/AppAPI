using Models.Auth;
using Models.Species;
using Models.Species.DTO;
using System.Collections.Generic;

namespace AppAPI.Services.Special
{
    public interface IUsersGroupsService
    {
        public IEnumerable<Group> GetUserGroups(int UserId);

        public void CreateGroup(GroupCreateDto group, User creator);
    }
}
