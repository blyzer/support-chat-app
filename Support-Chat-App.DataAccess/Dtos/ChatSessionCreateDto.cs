using System;
using System.Collections.Generic;
using System.Text;

namespace Support_Chat_App.Data.Dtos
{
    public class ChatSessionCreateDto
    {
        public long UserId { get; set; }
        public DateTime CreatedDateTime { get; set; }
        public long AgentId { get; set; }
    }
}
