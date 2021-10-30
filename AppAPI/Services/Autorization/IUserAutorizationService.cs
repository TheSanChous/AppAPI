using Models.Auth;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;

namespace AppAPI.Services.Autorization
{
    public interface IUserAutorizationService
    {
        int GetUserId(ClaimsPrincipal User);
    }
}
