using Models;

namespace Data.Repositories
{
    public interface IUserRepository : IEntityRepository<User>
    {
        public void Add(User user);

        public User Get(int Id);

        public User Get(string Email);
    }
}
