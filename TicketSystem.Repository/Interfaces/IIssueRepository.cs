using TicketSystem.Common.Enums;
using TicketSystem.Common.Models.Entity;

namespace TicketSystem.Repository.Interfaces
{
    public interface IIssueRepository
    {
        Task<Issue> GetByRoleAndAction(int issueID, List<int> roleIDs, IssueActionEnum issueAction);
        Task<IEnumerable<RoleIssueAction>> GetRoleIssueAction();
        Task<Issue> Query(int issueID);
        Task Update(Issue issue);
        Task Delete(int issueID);
        Task<IEnumerable<RoleIssueStatus>> GetRoleIssueStatus();
    }
}
