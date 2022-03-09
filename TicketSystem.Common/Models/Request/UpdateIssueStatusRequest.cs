using System.ComponentModel.DataAnnotations;
using TicketSystem.Common.Enums;

namespace TicketSystem.Common.Models.Request
{
    public class UpdateIssueStatusRequest
    {
        [Required]
        public int IssueID { get; set; }

        [Required]
        public IssueStatusEnum IssueStatus { get; set; }
    }
}
