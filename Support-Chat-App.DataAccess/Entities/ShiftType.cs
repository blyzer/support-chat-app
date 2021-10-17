using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Support_Chat_App.Data.Entities
{
    public class ShiftType : BaseEntity
    {
        [Column("name", TypeName = "varchar"), MaxLength(100)]
        public string Name { get; set; }

        public ICollection<TeamType> TeamTypes { get; set; }
    }
}
