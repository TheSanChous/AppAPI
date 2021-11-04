using AppAPI.DTO;
using AppAPI.Services.IdentifierGenerator;
using Data;
using Data.Models.Auth;
using Data.Models.Species;
using Data.Repositories;
using System.Collections.Generic;
using AppAPI.Services.Groups.Exceptions;

namespace AppAPI.Services.Groups
{
    public class UsersGroupsService : IUsersGroupsService
    {
        private readonly IGroupRepository _groupRepository;
        private readonly IUserRepository _userRepository;
        private readonly IUserGroupRepository _userGroupRepository;
        private readonly IGroupMemberTypeRepository _groupMemberTypeRepository;
        private readonly DatabaseContext _databaseContext;

        private readonly IIdentifierGeneratorService _identifierGenerator;

        public UsersGroupsService(IUserRepository userRepository,
            IGroupRepository groupRepository,
            IUserGroupRepository userGroupRepository,
            IGroupMemberTypeRepository groupMemberTypeRepository,
            IIdentifierGeneratorService identifierGenerator,
            DatabaseContext databaseContext)
        {
            _userRepository = userRepository;
            _groupRepository = groupRepository;
            _userGroupRepository = userGroupRepository;
            _groupMemberTypeRepository = groupMemberTypeRepository;
            _identifierGenerator = identifierGenerator;
            _databaseContext = databaseContext;
        }

        public IEnumerable<Group> GetUserGroups(User user)
        {
            var groups = _userGroupRepository.GetGroups(user);
            return groups;
        }

        public IEnumerable<User> GetGroupUsers(int groupIdentifier)
        {
            var group = _groupRepository.Get(groupIdentifier);

            if (group is null)
            {
                throw new GroupNotFoundException();
            }

            var users = _userGroupRepository.GetUsers(group);

            return users;
        }

        public Group CreateGroup(GroupCreateModel group, User administrator)
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
                User = administrator,
                MemberType = _groupMemberTypeRepository.GetGroupMemberType(GroupMemberTypes.Administrator)
            };
            _userGroupRepository.Add(userGroup);
            _databaseContext.SaveChanges();

            return newGroup;
        }

        public void JoinUserToGroup(User user, string groupIdentifier, GroupMemberTypes type = GroupMemberTypes.Student)
        {
            var group = _groupRepository.Get(groupIdentifier);

            if(group is null)
            {
                throw new GroupNotFoundException();
            }

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
