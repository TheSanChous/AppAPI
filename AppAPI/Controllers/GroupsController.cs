using AppAPI.DTO;
using AppAPI.Services.Authorization;
using AppAPI.Services.Groups;
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
            var getUserIdResult = _userAuthorizationService.GetUserId(User);
            if (!getUserIdResult.IsOk)
            {
                return BadRequest(getUserIdResult.Error);
            }

            var userId = getUserIdResult.Value;

            var result = _groupService.GetUserGroups(userId);

            if (result.IsOk)
            {
                return Ok(result.Value);
            }

            return BadRequest(result.Error);
        }

        [HttpPost("create")]
        [Authorize]
        public IActionResult CreateGroup([FromBody] GroupCreateModel group)
        {
            var getUserResult = _userAuthorizationService.GetUser(User);
            if (!getUserResult.IsOk)
            {
                return BadRequest(getUserResult.Error);
            }
            var user = getUserResult.Value;

            var result = _groupService.CreateGroup(group, user);
            
            if (result.IsOk)
            {
                return Ok();
            }

            return BadRequest(result.Error);
        }

        [HttpPost("join")]
        [Authorize]
        public IActionResult JoinUserToGroup([FromBody] string groupId)
        {
            var getUserResult = _userAuthorizationService.GetUser(User);
            if (!getUserResult.IsOk)
            {
                return BadRequest(getUserResult.Error);
            }
            var user = getUserResult.Value;

            var result = _groupService.JoinUserToGroup(user, groupId);

            if (result.IsOk)
            {
                return Ok();
            }

            return BadRequest(result.Error);
        }
    }
}
