using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;
using Models;
using Models.Auth;
using System;
using System.Linq;

namespace AuthAPI.Attributes
{
    [AttributeUsage(AttributeTargets.Method | AttributeTargets.Class)]
    public class RequirePermissionAttribute : TypeFilterAttribute
    {
        public RequirePermissionAttribute(params PermissionType[] permissions) : base(typeof(PermissionFilter))
        {
            Arguments = new object[] { permissions };
        }
    }

    public class PermissionFilter : IAuthorizationFilter
    {
        private readonly PermissionType[] _permissions;

        public PermissionFilter(PermissionType[] permissions)
        {
            _permissions = permissions;
        }

        public void OnAuthorization(AuthorizationFilterContext context)
        {
            var user = context.HttpContext.User;

            var hasExtendedPermissions =
                _permissions.All(p =>
                    user.HasClaim("permission", p.ToString()));

            if (!hasExtendedPermissions)
            {
                context.Result = new ForbidResult();
            }
        }
    }
}
