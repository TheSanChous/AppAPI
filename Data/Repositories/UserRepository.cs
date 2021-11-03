using Data.Models.Auth;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Linq;

namespace Data.Repositories
{
    public class UserRepository : IUserRepository
    {
        private readonly DatabaseContext _databaseContext;

        public UserRepository(DatabaseContext databaseContext)
        {
            _databaseContext = databaseContext;
        }

        public void Add(User user)
        {
            _databaseContext.Users.Add(user);
        }

        public void Delete(User entity)
        {
            _databaseContext.Users.Remove(entity);
        }

        public void Delete(int id)
        {
            _databaseContext.Users.Remove(Get(id));
        }

        public IEnumerable<User> Get()
        {
            return _databaseContext.Users
                .Include(u => u.Roles)
                .ThenInclude(u => u.Permissions);
        }

        public User Get(int Id)
        {
            return Get()
                .SingleOrDefault(u => u.Id == Id);
        }

        public User Get(string Email)
        {
            return Get()
                .SingleOrDefault(u => u.Email == Email);
        }

        public void Update(User entity)
        {
            _databaseContext.Users.Attach(entity);
            _databaseContext.Entry(entity).State = EntityState.Modified;
        }
    }
}
