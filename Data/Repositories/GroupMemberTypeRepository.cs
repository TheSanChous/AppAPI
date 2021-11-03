using Data.Models.Species;
using System.Linq;

namespace Data.Repositories
{
    public class GroupMemberTypeRepository : IGroupMemberTypeRepository
    {
        private readonly DatabaseContext _databaseContext;

        public GroupMemberTypeRepository(DatabaseContext databaseContext)
        {
            _databaseContext = databaseContext;
        }

        public GroupMemberType GetGroupMemberType(GroupMemberTypes groupMemberType)
        {
            return _databaseContext.GroupMemberTypes
                .SingleOrDefault(gmt => gmt.MemberTypeId == groupMemberType);
        }
    }
}
