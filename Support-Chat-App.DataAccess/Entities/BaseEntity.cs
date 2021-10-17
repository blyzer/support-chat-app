using System;
using System.ComponentModel.DataAnnotations.Schema;

namespace Support_Chat_App.Data.Entities
{
    public class BaseEntity
    {
        [Column("id")]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public long Id { get; set; }
        
        [Column("created_by")]
        public long? CreatedBy { get; set; }

        [Column("created_on")]
        public DateTime? CreatedOn { get; set; }

        [Column("updated_by")]
        public long? UpdatedBy { get; set; }

        [Column("updated_on")]
        public DateTime? UpdatedOn { get; set; }

        [Column("is_deleted")]
        public bool? IsDeleted { get; set; }
    }
}
