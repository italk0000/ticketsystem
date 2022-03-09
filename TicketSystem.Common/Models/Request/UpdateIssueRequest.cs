using System.ComponentModel.DataAnnotations;

namespace TicketSystem.Common.Models.Request
{
    public class UpdateIssueRequest
    {
        [Required]
        public int IssueID { get; set; }
        public string Summary { get; set; }
        public string Description { get; set; }
    }
}
