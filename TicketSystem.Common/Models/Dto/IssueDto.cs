using TicketSystem.Common.Enums;

namespace TicketSystem.Common.Models.Dto
{
    public class IssueDto
    {
        public int IssueID { get; set; }
        public int IssueType { get; set; }
        public IssueStatusEnum IssueStatus { get; set; }
        public string Summary { get; set; }
        public string Description { get; set; }
        public bool IsDeleted { get; set; }
        public string CreatedUser { get; set; }
        public DateTime CreatedTime { get; set; }
        public string UpdatedUser { get; set; }
        public DateTime UpdatedTime { get; set; }
    }
}
