using TicketSystem.Common.Enums;

namespace TicketSystem.Common.Models.Entity
{
    public class RoleIssueAction
    {
        public int RoleID { get; set; }
        public int IssueType { get; set; }
        public IssueActionEnum IssueAction { get; set; }
    }
}
