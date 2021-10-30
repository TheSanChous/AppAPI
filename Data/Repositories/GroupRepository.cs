using Microsoft.EntityFrameworkCore;
using Models.Species;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

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
            _databaseContext.Groups.Remove(Get(id));
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
                .Where(g => g.Id == id)
                .SingleOrDefault();
        }

        public void Update(Group entity)
        {
            _databaseContext.Groups.Attach(entity);
            _databaseContext.Entry(entity).State = EntityState.Modified;
        }
    }
}
