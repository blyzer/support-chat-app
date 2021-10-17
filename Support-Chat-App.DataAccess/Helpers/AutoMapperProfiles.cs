using AutoMapper;
using Support_Chat_App.Data.Dtos;
using Support_Chat_App.Data.Entities;

namespace Support_Chat_App.Data.Helpers 
{ 
    public class AutoMapperProfiles : Profile
    {
        public AutoMapperProfiles()
        {
            CreateMap<User, UserDto>().ReverseMap();
            CreateMap<UserType, UserTypeDto>().ReverseMap();
            CreateMap<AgentSeniorityType, AgentSeniorityTypeDto>().ReverseMap();
            CreateMap<Chat, ChatDto>().ReverseMap();
            CreateMap<AgentChat, AgentChatDto>().ReverseMap(); 
        }
    }
}
