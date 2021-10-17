using System;
using System.Collections.Generic;
using System.Text;

namespace Support_Chat_App.Data.Dtos
{
    public class AgentChatDto
    {
        public long Id{ get; set; }
        public long AgentId { get; set; }
        public long ChatId { get; set; }
        public DateTime? CreatedOn { get; set; }
    }
}
