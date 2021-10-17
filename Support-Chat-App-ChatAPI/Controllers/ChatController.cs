using Microsoft.AspNetCore.Mvc;
using Support_Chat_App.Data.Enums;
using Support_Chat_App.Repositories.Authorization;
using Microsoft.Extensions.Logging;
using Support_Chat_App.Managers.IManagers;
using Microsoft.AspNetCore.Http;
using System;
using Support_Chat_App.Data.Dtos;

namespace Support_Chat_App.ChatAPI.Controllers
{
    [Authorize]
    [ApiController]
    [Route("v1/[controller]")]
    public class ChatsController : ControllerBase
    {
        private IChatManager _chatManager;
        private readonly ILogger<ChatsController> _logger;

        public ChatsController(IChatManager chatManager,
            ILogger<ChatsController> logger)
        {
            _chatManager = chatManager;
            _logger = logger;
        }

        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [Authorize(UserTypeEnum.Client)]
        [HttpPost]
        public IActionResult CreateChat()
        {
            try
            {
                var user = (UserDto)HttpContext.Items["User"];
                if (user != null)
                    _chatManager.CreateChat(user.Id);
                else
                    return BadRequest("Please log into the system again");

                return Ok();
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }
    }
}
