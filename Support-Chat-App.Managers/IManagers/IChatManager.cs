using Support_Chat_App.Data.Dtos;
using Support_Chat_App.Data.RequestModels;
using Support_Chat_App.Data.ResponseModels;
using System.Collections.Generic;

namespace Support_Chat_App.Managers.IManagers
{
    public interface IChatManager
    {
        void CreateChat(long userId);
        void CreateChatSession(ChatSessionCreateDto chatSession);
    }
}
