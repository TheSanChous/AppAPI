using Data;
using Data.Repositories;
using Microsoft.EntityFrameworkCore;
using Models.Auth;
using Models.Species;
using Models.Species.DTO;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace AppAPI.Services.Special
{
    public class UsersGroupsService : IUsersGroupsService
    {
        private readonly IGroupRepository _groupRepository;
        private readonly IUserRepository _userRepository;
        private readonly IUserGroupRepository _userGroupRepository;
        private readonly IGroupMemberTypeRepository _groupMemberTypeRepository;
        private readonly IIdentifierGenerator _identifierGenerator;
        private readonly DatabaseContext _databaseContext;

        public UsersGroupsService(IUserRepository userRepository,
            IGroupRepository groupRepository,
            IUserGroupRepository userGroupRepository,
            IGroupMemberTypeRepository groupMemberTypeRepository,
            IIdentifierGenerator identifierGenerator,
            DatabaseContext databaseContext)
        {
            _userRepository = userRepository;
            _groupRepository = groupRepository;
            _userGroupRepository = userGroupRepository;
            _groupMemberTypeRepository = groupMemberTypeRepository;
            _identifierGenerator = identifierGenerator;
            _databaseContext = databaseContext;
        }

        public IEnumerable<Group> GetUserGroups(int UserId)
        {
            User user = _userRepository.Get(UserId);
            return _userGroupRepository.GetGroups(user);
        }

        public IEnumerable<User> GetGroupUsers(int GroupId)
        {
            var group = _groupRepository.Get(GroupId);
            return _userGroupRepository.GetUsers(group);
        }

        public void CreateGroup(GroupCreateDto group, User creator)
        {
            var newGroup = new Group
            {
                Identifier = _identifierGenerator.GenerateIdentifier(),
                Name = group.Name,
                Description = group.Description
            };

            _groupRepository.Add(newGroup);
            _databaseContext.SaveChanges();

            var userGroup = new UserGroup
            {
                Group = newGroup,
                User = creator,
                MemberType = _groupMemberTypeRepository.GetGroupMemberType(GroupMemberTypes.Administrator)
            };
            _userGroupRepository.Add(userGroup);
            _databaseContext.SaveChanges();
        }

        public void JoinUserToGroup(User user, int groupId, GroupMemberTypes type = GroupMemberTypes.Student)
        {
            var group = _groupRepository.Get(groupId);

            var userGroup = new UserGroup
            {
                Group = group,
                User = user,
                MemberType = _groupMemberTypeRepository.GetGroupMemberType(type)
            };

            _userGroupRepository.Add(userGroup);
            _databaseContext.SaveChanges();
        }
    }
}
