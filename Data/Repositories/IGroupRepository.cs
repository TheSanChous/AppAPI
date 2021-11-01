using Models.Species;

namespace Data.Repositories
{
    public interface IGroupRepository : IEntityRepository<Group>
    {
        public Group Get(string id);
    }
}
