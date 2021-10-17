using System.ComponentModel.DataAnnotations;

namespace Support_Chat_App.Data.RequestModels
{
    public class LoginRequestModel
    {
        [Required]
        public string Username { get; set; }

        [Required]
        public string Password { get; set; }
    }
}
