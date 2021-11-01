using Models.Auth;
using Models.Species;
using Models.Species.DTO;
using System.Collections.Generic;

namespace AppAPI.Services.Groups
{
    public interface IUsersGroupsService
    {
        public IServiceActionResult<IEnumerable<Group>> GetUserGroups(int userId);

        public IServiceActionResult<Group> CreateGroup(GroupCreateDto group, User creator);

        public IServiceActionResult JoinUserToGroup(User user, string groupId, GroupMemberTypes type = GroupMemberTypes.Student);
    }
}
