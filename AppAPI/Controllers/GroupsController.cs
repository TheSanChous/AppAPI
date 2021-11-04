using AppAPI.DTO;
using AppAPI.Services.Authorization;
using AppAPI.Services.Groups;
using Data.Models.Auth;
using Data.Models.Species;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;

namespace AppAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class GroupsController : ControllerBase
    {
        private readonly IConfiguration _configuration;
        private readonly IUsersGroupsService _groupService;
        private readonly IUserAuthorizationService _userAuthorizationService;

        public GroupsController(IConfiguration configuration, IUsersGroupsService groupService, IUserAuthorizationService userAuthorizationService)
        {
            _configuration = configuration;
            _groupService = groupService;
            _userAuthorizationService = userAuthorizationService;
        }

        [HttpGet("all")]
        [Authorize]
        public IActionResult GetGroups()
        {
            User user = _userAuthorizationService.GetUser(User);
            
            var groups = _groupService.GetUserGroups(user);

            return Ok(groups);
        }

        [HttpPost("create")]
        [Authorize]
        public IActionResult CreateGroup([FromBody] GroupCreateModel groupCreateModel)
        {
            User user = _userAuthorizationService.GetUser(User);
           
            _groupService.CreateGroup(groupCreateModel, user);

            return Ok();
        }

        [HttpPost("join")]
        [Authorize]
        public IActionResult JoinUserToGroup([FromBody] string groupId)
        {
            User user = _userAuthorizationService.GetUser(User);
            
            _groupService.JoinUserToGroup(user, groupId);

            return Ok();
        }
    }
}
