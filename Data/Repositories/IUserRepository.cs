using Models.Auth;

namespace Data.Repositories
{
    public interface IUserRepository : IEntityRepository<User>
    {
        public User Get(string Email);
    }
}
