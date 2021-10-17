using Support_Chat_App.Data.Dtos;
using System.Collections.Generic;

namespace Support_Chat_App.Repositories.IRepositories
{
    public interface IChatRepository
    {
        ChatDto Add(ChatDto chat);
    }
}
