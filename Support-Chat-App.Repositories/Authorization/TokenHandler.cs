using Microsoft.AspNetCore.Http;
using Support_Chat_App.Repositories.IRepositories;
using System.Linq;
using System.Threading.Tasks;

namespace Support_Chat_App.Repositories.Authorization
{
    public class TokenMiddleware
    {
        private readonly RequestDelegate _next;

        public TokenMiddleware(RequestDelegate next)
        {
            _next = next;
        }

        /// <summary>
        /// To store the user object in the HTTP Context
        /// </summary>
        /// <param name="context"></param>
        /// <param name="userService"></param>
        /// <param name="tokenController"></param>
        /// <returns></returns>
        public async Task Invoke(HttpContext context, IUserRepository userService, ITokenController tokenController)
        {
            var token = context.Request.Headers["Authorization"].FirstOrDefault()?.Split(" ").Last();
            var userId = tokenController.ValidateToken(token);
            if (userId != null)
            {
                context.Items["User"] = userService.GetById(userId.Value);
            }

            await _next(context);
        }
    }
}
