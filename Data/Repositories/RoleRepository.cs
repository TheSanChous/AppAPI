using AppAPI.Models;
using Microsoft.EntityFrameworkCore;
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

        public void Add(Role entity)
        {
            throw new System.NotImplementedException();
        }

        public void Delete(Role entity)
        {
            throw new System.NotImplementedException();
        }

        public void Delete(int id)
        {
            throw new System.NotImplementedException();
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

        public Role Get(int id)
        {
            throw new System.NotImplementedException();
        }

        public void Update(Role entity)
        {
            throw new System.NotImplementedException();
        }
    }
}
