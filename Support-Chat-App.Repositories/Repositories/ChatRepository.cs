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
    public class ChatRepository : IChatRepository
    {
        private SupportChatContext _context;
        private IMapper _mapper;

        public ChatRepository(SupportChatContext context, 
            IMapper mapper)
        {
            _context = context;
            _mapper = mapper;
        }

        public ChatDto Add(ChatDto chat)
        {
            try
            {
                var record = _mapper.Map<Chat>(chat);
                _context.Chats.Add(record);
                _context.SaveChanges();
                return _mapper.Map<ChatDto>(record);
            }
            catch(Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }
    }
}
