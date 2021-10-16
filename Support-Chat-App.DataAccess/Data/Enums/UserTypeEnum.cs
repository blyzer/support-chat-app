using System.ComponentModel;

namespace Support_Chat_App.DataAccess.Data.Enums
{
    enum UserTypeEnum
    {
        [Description("Client")]
        Client = 1,
        [Description("Agent")]
        Agent
    }
}
