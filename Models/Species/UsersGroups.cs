using Models.Auth;

namespace Models.Species
{
    public class UsersGroups
    {
        public int Id { get; set; }

        public User User { get; set; }

        public Group Group { get; set; }

        public GroupMemberType MemberType { get; set; }
    }
}
