using Microsoft.EntityFrameworkCore;
using Models.Species;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Group = Data.Models.Species.Group;

namespace Data.Repositories
{
    public class GroupRepository : IGroupRepository
    {
        private readonly DatabaseContext _databaseContext;

        public GroupRepository(DatabaseContext databaseContext)
        {
            _databaseContext = databaseContext;
        }

        public void Add(Group entity)
        {
            _databaseContext.Groups.Add(entity);
        }

        public void Delete(Group entity)
        {
            _databaseContext.Groups.Remove(entity);
        }

        public void Delete(int id)
        {
            _databaseContext.Groups
                .Remove(Get(id));
        }

        public Group Get(string id)
        {
            return _databaseContext.Groups
                .FirstOrDefault(g => g.Identifier == id);
        }

        public IEnumerable<Group> Get()
        {
            return _databaseContext.Groups
                .Include(g => g.Subjects)
                .ThenInclude(s => s.Homeworks);
        }

        public Group Get(int id)
        {
            return Get()
                .SingleOrDefault(g => g.Id == id);
        }

        public void Update(Group entity)
        {
            _databaseContext.Groups.Attach(entity);
            _databaseContext.Entry(entity).State = EntityState.Modified;
        }
    }
}
