using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Support_Chat_App.Data.Entities
{
    public class TeamType : BaseEntity
    {
        [Column("name", TypeName = "varchar"), MaxLength(100)]
        public string Name { get; set; }

        [Column("shift_type_id")]
        [ForeignKey("ShiftType")]
        public long ShiftTypeId { get; set; }

        public ShiftType ShiftType { get; set; }
    }
}
