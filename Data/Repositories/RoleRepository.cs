using Microsoft.EntityFrameworkCore;
using Models.Auth;
using System.Collections.Generic;
using System.Linq;

namespace Data.Repositories
{
    public class RoleRepository : IRoleRepository
    {
        private readonly DatabaseContext _database;

        public RoleRepository(DatabaseContext databaseContext)
        {
            _database = databaseContext;
        }

        public Role Get(string title)
        {
            return Get()
                .Where(r => r.Title == title)
                .SingleOrDefault();
        }

        public IEnumerable<Role> Get()
        {
            return _database.Roles
                .Include(r => r.Permissions);
        }
    }
}
