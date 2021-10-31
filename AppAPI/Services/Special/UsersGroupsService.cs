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
        private readonly DatabaseContext _databaseContext;

        public UsersGroupsService(IUserRepository userRepository,
            IGroupRepository groupRepository,
            IUserGroupRepository userGroupRepository,
            IGroupMemberTypeRepository groupMemberTypeRepository,
            DatabaseContext databaseContext)
        {
            _userRepository = userRepository;
            _groupRepository = groupRepository;
            _userGroupRepository = userGroupRepository;
            _groupMemberTypeRepository = groupMemberTypeRepository;
            _databaseContext = databaseContext;
        }

        public IEnumerable<Group> GetUserGroups(int UserId)
        {
            User user = _userRepository.Get(UserId);
            return _userGroupRepository.GetGroups(user);
        }

        public IEnumerable<User> GetGroupUsers(int GroupId)
        {
            Group group = _groupRepository.Get(GroupId);
            return _userGroupRepository.GetUsers(group);
        }

        public void CreateGroup(GroupCreateDto group, User creator)
        {
            Group newGroup = new Group
            {
                Name = group.Name,
                Description = group.Description
            };

            _groupRepository.Add(newGroup);
            _databaseContext.SaveChanges();

            UserGroup userGroup = new UserGroup
            {
                Group = newGroup,
                User = creator,
                MemberType = _groupMemberTypeRepository.GetGroupMemberType(GroupMemberTypes.Administrator)
            };
            _userGroupRepository.Add(userGroup);
            _databaseContext.SaveChanges();
        }
    }
}
