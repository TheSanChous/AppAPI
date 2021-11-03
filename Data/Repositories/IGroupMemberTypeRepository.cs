using Models.Species;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using GroupMemberType = Data.Models.Species.GroupMemberType;
using GroupMemberTypes = Data.Models.Species.GroupMemberTypes;

namespace Data.Repositories
{
    public interface IGroupMemberTypeRepository
    {
        public GroupMemberType GetGroupMemberType(GroupMemberTypes groupMemberType);
    }
}
