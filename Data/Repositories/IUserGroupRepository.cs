using Models.Auth;
using Models.Species;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Data.Repositories
{
    public interface IUserGroupRepository : IEntityRepository<UserGroup>
    {
        public IEnumerable<User> GetUsers(Group group);

        public IEnumerable<Group> GetGroups(User user);
    }
}
