using Data;
using Data.Repositories;
using Microsoft.EntityFrameworkCore;
using Models.Auth;
using Models.Species;
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
        private readonly DatabaseContext _databaseContext;

        public UsersGroupsService(IUserRepository userRepository,
            IGroupRepository groupRepository,
            IUserGroupRepository userGroupRepository,
            DatabaseContext databaseContext)
        {
            _userRepository = userRepository;
            _groupRepository = groupRepository;
            _userGroupRepository = userGroupRepository;
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
    }
}
