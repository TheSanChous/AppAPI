using Models.Species;
using Group = Data.Models.Species.Group;

namespace Data.Repositories
{
    public interface IGroupRepository : IEntityRepository<Group>
    {
        public Group Get(string id);
    }
}
