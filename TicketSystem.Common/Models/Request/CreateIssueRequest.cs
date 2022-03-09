using System.ComponentModel.DataAnnotations;
using TicketSystem.Common.Enums;

namespace TicketSystem.Common.Models.Request
{
    public class CreateIssueRequest
    {
        [Required]
        public int IssueType { get; set; }

        [Required]
        public IssueStatusEnum IssueStatus { get; set; } = IssueStatusEnum.New;

        [Required]
        public string Summary { get; set; }

        [Required]
        public string Description { get; set; }
    }
}
