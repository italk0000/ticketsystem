using TicketSystem.Common.Enums;

namespace TicketSystem.Common.Models.Entity
{
    public class RoleIssueStatus
    {
        public int RoleID { get; set; }
        public int IssueType { get; set; }
        public IssueStatusEnum IssueStatus { get; set; }
    }
}
