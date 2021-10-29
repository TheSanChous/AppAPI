using System.Collections.Generic;

namespace Data.Repositories
{
    public interface IEntityRepository<Entity>
    {
        public IEnumerable<Entity> Get();
    }
}
