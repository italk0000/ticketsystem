using Dapper;
using TicketSystem.Common.Enums;

namespace TicketSystem.Common.Models.Entity
{
    [Table("Issue")]
    public class Issue
    {
        [Key]
        public int IssueID { get; set; }
        public int TicketType { get; set; }
        public IssueStatusEnum IssueStatus { get; set; }
        public string Summary { get; set; }
        public string Description { get; set; }
        public bool IsDeleted { get; set; }
        public string CreatedUser { get; set; }
        public DateTime CreatedTime { get; set; }
        [Editable(false)]
        public string UpdatedUser { get; set; }
        [Editable(false)]
        public DateTime UpdatedTime { get; set; }
    }
}
