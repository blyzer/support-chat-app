using System.Collections.Generic;

namespace Support_Chat_App.Data.Dtos
{
    public class UserTypeDto : BaseEntityDto
    {
        public string Name { get; set; }

        public ICollection<UserDto> Users { get; set; }
    }
}
