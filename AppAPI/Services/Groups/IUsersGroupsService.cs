using AppAPI.DTO;
using Data.Models.Auth;
using Data.Models.Species;
using System.Collections.Generic;

namespace AppAPI.Services.Groups
{
    public interface IUsersGroupsService
    {
        public IEnumerable<Group> GetUserGroups(User user);

        public Group CreateGroup(GroupCreateModel group, User creator);

        public void JoinUserToGroup(User user, string groupIdentifier, GroupMemberTypes type = GroupMemberTypes.Student);
    }
}
