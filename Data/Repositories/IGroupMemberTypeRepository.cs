using Data.Models.Species;

namespace Data.Repositories
{
    public interface IGroupMemberTypeRepository
    {
        public GroupMemberType GetGroupMemberType(GroupMemberTypes groupMemberType);
    }
}
