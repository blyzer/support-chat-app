using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Support_Chat_App.Data.Entities
{
    public class AgentSeniorityType : BaseEntity
    {
        [Column("name", TypeName = "varchar"), MaxLength(100)]
        public string Name { get; set; }

        [Column("seniority_multiplier")]
        public double SeniorityMultiplier { get; set; }

        public ICollection<User> Enrollments { get; set; }
    }
}
