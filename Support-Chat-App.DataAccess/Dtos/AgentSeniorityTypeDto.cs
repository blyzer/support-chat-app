using System.Collections.Generic;

namespace Support_Chat_App.Data.Dtos
{
    public class AgentSeniorityTypeDto : BaseEntityDto
    {
        public string Name { get; set; }
        public double SeniorityMultiplier { get; set; }

        public ICollection<UserDto> Enrollments { get; set; }
    }
}
