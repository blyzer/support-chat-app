namespace Support_Chat_App.Data.Dtos
{
    public class UserWithSeniority
    {
        public long UserId { get; set; }
        public double Seniority { get; set; }
        public int ChatCount { get; set; }
        public long AgentSeniorityTypeId { get; set; }
        public bool? IsOverflowAgent { get; set; }
    }
}
