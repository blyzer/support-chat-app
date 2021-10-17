using AutoMapper;
using Support_Chat_App.Data;
using Support_Chat_App.Data.Dtos;
using Support_Chat_App.Data.Entities;
using Support_Chat_App.Repositories.IRepositories;
using System;
using System.Collections.Generic;
using System.Linq;

namespace Support_Chat_App.Repositories.Repositories
{
    public class AgentChatRepository : IAgentChatRepository
    {
        private SupportChatContext _context;
        private IMapper _mapper;

        public AgentChatRepository(SupportChatContext context, 
            IMapper mapper)
        {
            _context = context;
            _mapper = mapper;
        }

        /// <summary>
        /// Create a new Agent Chat record
        /// </summary>
        /// <param name="agentChat"></param>
        /// <returns></returns>
        public AgentChatDto Add(AgentChatDto agentChat)
        {
            try
            {
                var record = _mapper.Map<AgentChat>(agentChat);
                _context.AgentChats.Add(record);
                _context.SaveChanges();
                return _mapper.Map<AgentChatDto>(record);
            }
            catch(Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }
    }
}
