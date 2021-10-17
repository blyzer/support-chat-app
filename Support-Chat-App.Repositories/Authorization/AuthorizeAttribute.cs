using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;
using Support_Chat_App.Data.Enums;
using System;
using System.Collections.Generic;
using System.Linq;
using Support_Chat_App.Data.Dtos;

namespace Support_Chat_App.Repositories.Authorization
{
    [AttributeUsage(AttributeTargets.Class | AttributeTargets.Method)]
    public class AuthorizeAttribute : Attribute, IAuthorizationFilter
    {
        private readonly IList<UserTypeEnum> _roleList;

        public AuthorizeAttribute(params UserTypeEnum[] roles)
        {
            _roleList = roles ?? new UserTypeEnum[] { };
        }

        /// <summary>
        /// Validate Anonymouse and Authorized attributes
        /// </summary>
        /// <param name="context"></param>
        public void OnAuthorization(AuthorizationFilterContext context)
        {
            // skip authorization for anonymous attribute
            var skipAnonymous = context.ActionDescriptor.EndpointMetadata.OfType<AnonymousAttribute>().Any();
            if (skipAnonymous)
                return;

            // handle authorization for other attributes
            var user = (UserDto)context.HttpContext.Items["User"];
            if (user == null || (_roleList.Any() && !_roleList.Contains((UserTypeEnum)user.UserTypeId)))
            {
                context.Result = new JsonResult(new { message = "Unauthorized" }) { StatusCode = StatusCodes.Status401Unauthorized };
            }
        }
    }
}
