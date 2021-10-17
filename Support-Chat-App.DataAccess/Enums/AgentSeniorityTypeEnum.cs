using System.ComponentModel;

namespace Support_Chat_App.Data.Enums
{
    public enum AgentSeniorityTypeEnum
    {
        [Description("Junior")]
        Junior = 1,
        [Description("Mid-Level")]
        MidLevel,
        [Description("Senior")]
        Senior,
        [Description("Team Lead")]
        TeamLead
    }
}
