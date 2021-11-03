using Models;

namespace Data.Models.Species
{
    public class GroupMemberType : BaseEntity
    {
        public GroupMemberTypes MemberTypeId { get; set; }

        public string Title { get; set; }
    }
}
