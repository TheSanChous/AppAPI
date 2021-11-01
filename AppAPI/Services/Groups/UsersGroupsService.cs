using AppAPI.Services.IdentifierGenerator;
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

namespace AppAPI.Services.Groups
{
    public class UsersGroupsService : ServiceBase, IUsersGroupsService
    {
        private readonly IGroupRepository _groupRepository;
        private readonly IUserRepository _userRepository;
        private readonly IUserGroupRepository _userGroupRepository;
        private readonly IGroupMemberTypeRepository _groupMemberTypeRepository;
        private readonly IIdentifierGeneratorService _identifierGenerator;
        private readonly DatabaseContext _databaseContext;

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

        public IServiceActionResult<IEnumerable<Group>> GetUserGroups(int UserId)
        {
            var user = _userRepository.Get(UserId);
            if(user is null)
            {
                return Error<IEnumerable<Group>>("User not found", null);
            }

            var groups = _userGroupRepository.GetGroups(user);
            return Ok(groups);
        }

        public IServiceActionResult<IEnumerable<User>> GetGroupUsers(int GroupId)
        {
            var group = _groupRepository.Get(GroupId);
            if (group is null)
            {
                return Error<IEnumerable<User>>("Group not found", null);
            }

            var users = _userGroupRepository.GetUsers(group);
            return Ok(users);
        }

        public IServiceActionResult<Group> CreateGroup(GroupCreateDto group, User creator)
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
                User = creator,
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
