using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Support_Chat_App.Data.Entities
{
    public class User : BaseEntity
    {
        [Column("email")]
        public string Email { get; set; }

        [Column("password", TypeName = "varchar"), MaxLength(100)]
        public string Password { get; set; }

        [ForeignKey("UserType")]
        [Column("user_type_id")]
        public long UserTypeId { get; set; }

        [ForeignKey("AgentSeniorityType")]
        [Column("agent_seniority_type_id")]
        public long? AgentSeniorityTypeId { get; set; }

        public UserType UserType { get; set; }
        public AgentSeniorityType AgentSeniorityType { get; set; }
    }
}
