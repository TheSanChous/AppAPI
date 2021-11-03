using Data.Models.Auth;

namespace Data.Models.Species
{
    public class UserGroup : BaseEntity
    {
        public User User { get; set; }

        public Group Group { get; set; }

        public GroupMemberType MemberType { get; set; }
    }
}
