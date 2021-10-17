using AutoMapper;
using Microsoft.EntityFrameworkCore;
using Support_Chat_App.Data;
using Support_Chat_App.Data.Dtos;
using Support_Chat_App.Data.Enums;
using Support_Chat_App.Repositories.IRepositories;
using System;
using System.Collections.Generic;
using System.Linq;

namespace Support_Chat_App.Repositories.Repositories
{
    public class UserRepository : IUserRepository
    {
        private SupportChatContext _context;
        private IMapper _mapper;

        public UserRepository(SupportChatContext context,
            IMapper mapper)
        {
            _context = context;
            _mapper = mapper;
        }

        /// <summary>
        /// Get all users
        /// </summary>
        /// <returns>user list</returns>
        public IEnumerable<UserDto> GetAll()
        {
            try
            {
                return _mapper.Map<List<UserDto>>(_context.Users);
            }
            catch (Exception ex)
            {
                throw new Exception($"Couldn't retrieve users: {ex.Message}");
            }
        }

        /// <summary>
        /// Get User by user id
        /// </summary>
        /// <param name="id"></param>
        /// <returns>Requested User</returns>
        public UserDto GetById(long id)
        {
            try
            {
                var user = _mapper.Map<UserDto>(_context.Users.Find(id));
                if (user == null)
                    throw new KeyNotFoundException("User is not exist");
                return user;
            }
            catch (Exception ex)
            {
                throw new Exception($"Couldn't retrieve user: {ex.Message}");
            }

        }

        /// <summary>
        /// Validate user email for login/authentication
        /// </summary>
        /// <param name="email"></param>
        /// <returns>Returns the user object for given email address</returns>
        public UserDto GetByEmail(string email)
        {
            try
            {
                return _mapper.Map<UserDto>(_context.Users.SingleOrDefault(x => x.Email == email));
            }
            catch (Exception ex)
            {
                throw new Exception($"Couldn't find the email: {ex.Message}");
            }
        }

        /// <summary>
        /// Get User with the seniority
        /// </summary>
        /// <param name="teamTypeId"></param>
        /// <returns>Requested User</returns>
        public List<UserWithSeniority> GetByTeamType(long teamTypeId)
        {
            try
            {
                return _context.Users
                            .Include(x => x.AgentSeniorityType)
                            .Include(x => x.AgentChats)
                            .Where(x => x.TeamTypeId == teamTypeId || 
                                    x.TeamTypeId == (long)TeamEnum.Overflow)
                            .OrderBy(x => x.AgentChats.Count)
                            .Select(x => new UserWithSeniority()
                            {
                                UserId = x.Id,
                                Seniority = x.AgentSeniorityType.SeniorityMultiplier,
                                ChatCount = x.AgentChats.Count(x => x.CreatedOn.Value.Date == DateTime.UtcNow.Date),
                                AgentSeniorityTypeId = x.AgentSeniorityTypeId.Value,
                                IsOverflowAgent = x.TeamTypeId == (long)TeamEnum.Overflow
                            }).ToList();
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }

        }
    }
}
