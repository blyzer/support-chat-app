using Support_Chat_App.Data.Dtos;

namespace Support_Chat_App.Repositories.Authorization
{
    public interface ITokenController
    {
        public string GenerateToken(UserDto user);
        public int? ValidateToken(string token);
    }
}
