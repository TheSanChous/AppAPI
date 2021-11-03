using AppAPI.DTO;
using AppAPI.Services.IdentifierGenerator;
using Data;
using Data.Models.Auth;
using Data.Models.Species;
using Data.Repositories;
using System.Collections.Generic;

namespace AppAPI.Services.Groups
{
    public class UsersGroupsService : ServiceBase, IUsersGroupsService
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

        public IServiceActionResult<IEnumerable<Group>> GetUserGroups(int userId)
        {
            var user = _userRepository.Get(userId);
            if(user is null)
            {
                return Error<IEnumerable<Group>>("User not found", null);
            }

            var groups = _userGroupRepository.GetGroups(user);
            return Ok(groups);
        }

        public IServiceActionResult<IEnumerable<User>> GetGroupUsers(int groupId)
        {
            var group = _groupRepository.Get(groupId);
            if (group is null)
            {
                return Error<IEnumerable<User>>("Group not found", null);
            }

            var users = _userGroupRepository.GetUsers(group);
            return Ok(users);
        }

        public IServiceActionResult<Group> CreateGroup(GroupCreateModel group, User administrator)
        {
            var newGroup = new Group
            {
                Identifier = _identifierGenerator.GenerateIdentifier().Value,
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

            return Ok(newGroup);
        }

        public IServiceActionResult JoinUserToGroup(User user, string groupId, GroupMemberTypes type = GroupMemberTypes.Student)
        {
            var group = _groupRepository.Get(groupId);

            if(group is null)
            {
                return Error("Group not found");
            }

            var userGroup = new UserGroup
            {
                Group = group,
                User = user,
                MemberType = _groupMemberTypeRepository.GetGroupMemberType(type)
            };

            _userGroupRepository.Add(userGroup);
            _databaseContext.SaveChanges();

            return Ok();
        }
    }
}
