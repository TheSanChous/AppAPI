using Data.Models.Species;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Linq;
using Data.Models.Auth;

namespace Data.Repositories
{
    public class UserGroupRepository : IUserGroupRepository
    {
        private readonly DatabaseContext _databaseContext;

        public UserGroupRepository(DatabaseContext databaseContext)
        {
            _databaseContext = databaseContext;
        }

        public void Add(UserGroup userGroup)
        {
            _databaseContext.UsersGroups.Add(userGroup);
        }

        public void Delete(UserGroup userGroup)
        {
            _databaseContext.UsersGroups.Remove(userGroup);
        }

        public void Delete(int id)
        {
            _databaseContext.UsersGroups.Remove(Get(id));
        }

        public IEnumerable<UserGroup> Get()
        {
            return _databaseContext.UsersGroups
                .Include(ug => ug.User)
                .Include(ug => ug.Group)
                .Include(ug => ug.MemberType);
        }

        public UserGroup Get(int id)
        {
            return Get()
                .SingleOrDefault(ug => ug.Id == id);
        }

        public IEnumerable<Group> GetGroups(User user)
        {
            return Get()
                .Where(ug => ug.User == user)
                .Select(ug => ug.Group);
        }

        public IEnumerable<User> GetUsers(Group group)
        {
            return Get()
                .Where(ug => ug.Group == group)
                .Select(ug => ug.User);
        }

        public void Update(UserGroup userGroup)
        {
            _databaseContext.UsersGroups.Attach(userGroup);
            _databaseContext.Entry(userGroup).State = EntityState.Modified;
        }
    }
}
