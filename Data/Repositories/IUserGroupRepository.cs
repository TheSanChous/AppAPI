using Data.Models.Auth;
using System.Collections.Generic;
using Group = Data.Models.Species.Group;
using UserGroup = Data.Models.Species.UserGroup;

namespace Data.Repositories
{
    public interface IUserGroupRepository : IEntityRepository<UserGroup>
    {
        public IEnumerable<User> GetUsers(Group group);

        public IEnumerable<Group> GetGroups(User user);
    }
}
