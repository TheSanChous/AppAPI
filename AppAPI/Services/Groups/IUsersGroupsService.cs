using AppAPI.DTO;
using Data.Models.Auth;
using Data.Models.Species;
using System.Collections.Generic;

namespace AppAPI.Services.Groups
{
    public interface IUsersGroupsService
    {
        public IServiceActionResult<IEnumerable<Group>> GetUserGroups(int userId);

        public IServiceActionResult<Group> CreateGroup(GroupCreateModel group, User creator);

        public IServiceActionResult JoinUserToGroup(User user, string groupId, GroupMemberTypes type = GroupMemberTypes.Student);
    }
}
