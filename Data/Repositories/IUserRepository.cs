using Models;

namespace Data.Repositories
{
    public interface IUserRepository : IEntityRepository<User>
    {
        public void Add(User user);

        public User Get(int Id);
    }
}
