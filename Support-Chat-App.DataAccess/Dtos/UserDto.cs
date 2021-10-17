namespace Support_Chat_App.Data.Dtos
{
    public class UserDto : BaseEntityDto
    {
        public string Email { get; set; }
        public string Password { get; set; }
        public long UserTypeId { get; set; }
        public long? AgentSeniorityTypeId { get; set; }

        public UserTypeDto UserType { get; set; }
        public AgentSeniorityTypeDto AgentSeniorityType { get; set; }
    }
}
