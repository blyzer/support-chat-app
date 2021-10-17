using Support_Chat_App.Data.Dtos;
using Support_Chat_App.Data.Enums;

namespace Support_Chat_App.Data.ResponseModels
{
    public class LoginResponseModel
    {
        public long Id { get; set; }
        public string Email { get; set; }
        public long UserRoleId { get; set; }
        public UserTypeEnum UserRole { get; set; }
        public string Token { get; set; }

        public LoginResponseModel(UserDto user, string token)
        {
            Id = user.Id;
            Email = user.Email;
            UserRoleId = user.UserTypeId;
            UserRole = (UserTypeEnum)user.UserTypeId;
            Token = token;
        }
    }
}
