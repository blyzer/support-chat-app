using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Support_Chat_App.DataAccess.Entities
{
    public class UserType : BaseEntity
    {
        [Column("name", TypeName = "varchar"), MaxLength(100)]
        public string Name { get; set; }

        public ICollection<User> Users { get; set; }
    }
}
