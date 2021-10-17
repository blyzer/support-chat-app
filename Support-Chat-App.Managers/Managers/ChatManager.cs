using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Logging;
using Support_Chat_App.Data.Dtos;
using Support_Chat_App.Data.Enums;
using Support_Chat_App.Managers.IManagers;
using Support_Chat_App.Repositories.Authorization;
using Support_Chat_App.Repositories.IRepositories;
using System;
using System.Collections.Generic;
using System.Linq;
using upport_Chat_App.Managers.ChatSessionSender;

namespace Support_Chat_App.Managers.Managers
{
    public class ChatManager : IChatManager
    {
        private IChatRepository _chatRepository;
        private ITokenController _tokenController;
        private readonly ILogger<UserManager> _logger;
        private readonly IChatSessionCreationSender _chatSessionCreationSender;
        private IUserRepository _userRepository;
        private IHttpContextAccessor _httpContextAccessor;
        private IAgentChatRepository _agentChatRepository;

        private const int juniorCapacity = 4;
        private const int midCapacity = 6;
        private const int seniorCapacity = 8;
        private const int leadCapacity = 5;

        public ChatManager(IChatRepository chatRepository,
            ITokenController tokenController,
            ILogger<UserManager> logger,
            IChatSessionCreationSender chatSessionCreationSender,
            IUserRepository userRepository,
            IHttpContextAccessor httpContextAccessor,
            IAgentChatRepository agentChatRepository
            )
        {
            _chatRepository = chatRepository;
            _tokenController = tokenController;
            _logger = logger;
            _chatSessionCreationSender = chatSessionCreationSender;
            _userRepository = userRepository;
            _httpContextAccessor = httpContextAccessor;
            _agentChatRepository = agentChatRepository;
        }

        /// <summary>
        /// Add chat session into the queue
        /// </summary>
        /// <param name="userId"></param>
        public void CreateChat(long userId)
        {
            var chatSession = new
            {
                UserId = userId,
                CreatedDateTime = DateTime.UtcNow,
            };
            _chatSessionCreationSender.SendChatSession(chatSession);
        }

        /// <summary>
        /// Create chat session with client and agent
        /// </summary>
        /// <param name="chatSession"></param>
        public void CreateChatSession(ChatSessionCreateDto chatSession)
        {
            var agentList = _userRepository.GetByTeamType(SelectTeamByTimeSpan());
            var user = (UserDto)_httpContextAccessor.HttpContext.User.Claims;
            var SelectedAgentId = GetAgent(agentList);

            //create new chat - add record to the db 
            var chat = new ChatDto
            {
                AgentId = SelectedAgentId,
                ClientId = user.Id
            };
            _chatRepository.Add(chat);

            //create new agent chat - add record to the db 
            var agentChat = new AgentChatDto
            {
                AgentId = SelectedAgentId,
                ChatId = chat.Id,
                CreatedOn = DateTime.UtcNow
            };
            _agentChatRepository.Add(agentChat);
        }

        /// <summary>
        /// Get the selected Agent
        /// </summary>
        /// <param name="agentList"></param>
        /// <returns></returns>
        private long GetAgent(List<UserWithSeniority> agentList)
        {
            if (agentList.Any(x => x.AgentSeniorityTypeId == (long)AgentSeniorityTypeEnum.Junior && x.ChatCount < juniorCapacity))
            {
                return SelectAgentFromList(agentList, (long)AgentSeniorityTypeEnum.Junior, juniorCapacity);
            }
            else if (agentList.Any(x => x.AgentSeniorityTypeId == (long)AgentSeniorityTypeEnum.MidLevel && x.ChatCount < midCapacity))
            {
                return SelectAgentFromList(agentList, (long)AgentSeniorityTypeEnum.MidLevel, midCapacity);
            }
            else if (agentList.Any(x => x.AgentSeniorityTypeId == (long)AgentSeniorityTypeEnum.Senior && x.ChatCount < seniorCapacity))
            {
                return SelectAgentFromList(agentList, (long)AgentSeniorityTypeEnum.Senior, seniorCapacity);
            }
            else if (agentList.Any(x => x.AgentSeniorityTypeId == (long)AgentSeniorityTypeEnum.TeamLead && x.ChatCount < leadCapacity))
            {
                return SelectAgentFromList(agentList, (long)AgentSeniorityTypeEnum.TeamLead, leadCapacity);
            }
            else
            {
                return SelectAgentFromList(agentList, isOverflow: true);
            }
        }

        /// <summary>
        /// Select agent from the list
        /// </summary>
        /// <param name="agentList"></param>
        /// <param name="userTypeId"></param>
        /// <param name="capacity"></param>
        /// <param name="isOverflow"></param>
        /// <returns></returns>
        private long SelectAgentFromList(List<UserWithSeniority> agentList, long? userTypeId = null, int? capacity = null, bool isOverflow = false)
        {
            if (isOverflow)
            {
                return agentList.FirstOrDefault(x => x.AgentSeniorityTypeId == userTypeId && x.ChatCount < capacity).UserId;
            }
            else
            {
                return agentList.FirstOrDefault(x => x.IsOverflowAgent == true && x.ChatCount < capacity).UserId;
            }
        }

        /// <summary>
        /// Select team by the timespan
        /// </summary>
        /// <returns></returns>
        private int SelectTeamByTimeSpan()
        {
            TimeSpan time = DateTime.Now.TimeOfDay;

            //Morning Shift
            if (time > new TimeSpan(06, 00, 01)
             && time < new TimeSpan(14, 00, 00))
            {
                return (int)TeamEnum.A;
            }
            //Evening Shift
            else if (time > new TimeSpan(14, 00, 01)
             && time < new TimeSpan(22, 00, 00))
            {
                return (int)TeamEnum.B;
            }
            //Night Shift
            else
            {
                return (int)TeamEnum.C;
            }
        }
    }
}
