using Microsoft.EntityFrameworkCore;
using Models;
using System.Collections.Generic;
using System.Linq;

namespace Data.Repositories
{
    public class UserRepository : IUserRepository
    {
        private readonly DatabaseContext _database;

        public UserRepository(DatabaseContext databaseContext)
        {
            _database = databaseContext;
        }

        public void Add(User user)
        {
            _database.Users.Add(user);
        }

        public IEnumerable<User> Get()
        {
            return _database.Users
                .Include(u => u.Roles)
                .ThenInclude(u => u.Permissions);
        }

        public User Get(int Id)
        {
            return Get()
                .Where(u => u.Id == Id)
                .SingleOrDefault();
        }
    }
}
