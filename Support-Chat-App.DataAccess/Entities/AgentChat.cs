using System.ComponentModel.DataAnnotations.Schema;

namespace Support_Chat_App.Data.Entities
{
    public class AgentChat : BaseEntity
    {
        [ForeignKey("Agent")]
        [Column("agent_id")]
        public long AgentId { get; set; }

        [ForeignKey("Chat")]
        [Column("chat_id")]
        public long ChatId { get; set; }

        public User Agent { get; set; }
        public Chat Chat { get; set; }
    }
}
