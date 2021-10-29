using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Models.Species
{
    public class GroupMemberType
    {
        public int Id { get; set; }

        public GroupMemberTypes MemberTypeId { get; set; }

        public string Title { get; set; }
    }
}
