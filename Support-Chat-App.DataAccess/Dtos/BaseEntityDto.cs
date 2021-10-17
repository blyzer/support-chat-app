using System;

namespace Support_Chat_App.Data.Dtos
{
    public class BaseEntityDto
    {
        public long Id { get; set; }
        public long? CreatedBy { get; set; }
        public DateTime? CreatedOn { get; set; }
        public long? UpdatedBy { get; set; }
        public DateTime? UpdatedOn { get; set; }
        public bool? IsDeleted { get; set; }
    }
}
