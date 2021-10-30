using AppAPI.Services.Autorization;
using AppAPI.Services.Special;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
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
            int UserId = _userAutorizationService.GetUserId(User);
            var groups = _groupService.GetUserGroups(UserId);
            return Ok(groups);
        }
    }
}
