using Microsoft.AspNetCore.Mvc;
using Support_Chat_App.Data.Enums;
using Support_Chat_App.Data.RequestModels;
using Support_Chat_App.Repositories.Authorization;
using Microsoft.Extensions.Logging;
using Support_Chat_App.Data.Dtos;
using Support_Chat_App.Managers.IManagers;
using Microsoft.AspNetCore.Http;
using System;

namespace Support_Chat_App.AuthenticationAPI.Controllers
{
    [Authorize]
    [ApiController]
    [Route("v1/[controller]")]
    public class UsersController : ControllerBase
    {
        private IUserManager _userManager;
        private readonly ILogger<UsersController> _logger;

        public UsersController(IUserManager userManager, 
            ILogger<UsersController> logger)
        {
            _userManager = userManager;
            _logger = logger;
        }

        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [AnonymousAttribute]
        [HttpPost("/login")]
        public IActionResult Login(LoginRequestModel loginRequestModel)
        {
            try
            {
                var response = _userManager.Login(loginRequestModel);
                return Ok(response);
            }
            catch(Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [Authorize(UserTypeEnum.Agent)]
        [HttpGet]
        public IActionResult GetAll()
        {
            try
            {
                var users = _userManager.GetAll();
                return Ok(users);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [Authorize(UserTypeEnum.Agent)]
        [HttpGet("{id:int}")]
        public IActionResult GetById(int id)
        {
            try
            {
                var user = _userManager.GetById(id);
                return Ok(user);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }
    }
}
