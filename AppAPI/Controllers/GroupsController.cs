using AppAPI.Services.Autorization;
using AppAPI.Services.Groups;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Models.Auth;
using Models.Species.DTO;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;

namespace AppAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class GroupsController : ControllerBase
    {
        private readonly IConfiguration _configuration;
        private readonly IUsersGroupsService _groupService;
        private readonly IUserAutorizationService _userAutorizationService;

        public GroupsController(IConfiguration configuration, IUsersGroupsService groupService, IUserAutorizationService userAutorizationService)
        {
            _configuration = configuration;
            _groupService = groupService;
            _userAutorizationService = userAutorizationService;
        }

        [HttpGet("all")]
        [Authorize]
        public IActionResult GetGroups()
        {
            var getUserIdResult = _userAutorizationService.GetUserId(User);
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
        public IActionResult CreateGroup([FromBody] GroupCreateDto group)
        {
            var getUserResult = _userAutorizationService.GetUser(User);
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
            var getUserResult = _userAutorizationService.GetUser(User);
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
