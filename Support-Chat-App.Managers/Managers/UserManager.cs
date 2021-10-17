using AutoMapper;
using Microsoft.Extensions.Logging;
using Support_Chat_App.Data;
using Support_Chat_App.Data.Dtos;
using Support_Chat_App.Data.RequestModels;
using Support_Chat_App.Data.ResponseModels;
using Support_Chat_App.Managers.IManagers;
using Support_Chat_App.Repositories.Authorization;
using Support_Chat_App.Repositories.IRepositories;
using System;
using System.Collections.Generic;

namespace Support_Chat_App.Managers.Managers
{
    public class UserManager : IUserManager
    {
        private IUserRepository _userRepository;
        private ITokenController _tokenController;
        private readonly ILogger<UserManager> _logger;

        public UserManager(IUserRepository userRepository,
            ITokenController tokenController,
            ILogger<UserManager> logger)
        {
            _userRepository = userRepository;
            _tokenController = tokenController;
            _logger = logger;
        }

        /// <summary>
        /// Get all users
        /// </summary>
        /// <returns>User list</returns>
        public IEnumerable<UserDto> GetAll()
        {
            return _userRepository.GetAll();
        }

        /// <summary>
        /// Get user by given user id
        /// </summary>
        /// <param name="id"></param>
        /// <returns>User</returns>
        public UserDto GetById(long id)
        {
            return _userRepository.GetById(id); ;
        }

        /// <summary>
        /// Handle login by username and password
        /// </summary>
        /// <param name="loginRequestModel"></param>
        /// <returns>Login Response Model</returns>
        public LoginResponseModel Login(LoginRequestModel loginRequestModel)
        {
            var user = _userRepository.GetByEmail(loginRequestModel.Username);

            if (user == null || !(loginRequestModel.Password == user.Password))
                throw new Exception("Username or password is incorrect");

            var jwtToken = _tokenController.GenerateToken(user);

            return new LoginResponseModel(user, jwtToken);
        }
    }
}
