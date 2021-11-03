using Microsoft.EntityFrameworkCore;
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
                .Where(gmt => gmt.MemberTypeId == groupMemberType)
                .SingleOrDefault();
        }
    }
}
