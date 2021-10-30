using Models;
using System.Collections.Generic;

namespace Data.Repositories
{
    public interface IEntityRepository<T> where T : BaseEntity
    {
        public IEnumerable<T> Get();

        public T Get(int id);

        public void Add(T entity);

        public void Update(T entity);

        public void Delete(T entity);

        public void Delete(int id);
    }
}
