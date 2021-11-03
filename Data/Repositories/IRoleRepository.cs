using AppAPI.Models;

namespace Data.Repositories
{
    public interface IRoleRepository : IEntityRepository<Role>
    {
        Role Get(string title);
    }
}
