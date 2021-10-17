using System.ComponentModel.DataAnnotations.Schema;

namespace Support_Chat_App.Data.Entities
{
    public class Chat : BaseEntity
    {
        [Column("client_id")]
        public long ClientId { get; set; }

        [Column("agent_id")]
        public long AgentId { get; set; }
    }
}
