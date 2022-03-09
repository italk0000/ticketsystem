using Dapper;
using TicketSystem.Common.DataAccess;
using TicketSystem.Common.Enums;
using TicketSystem.Common.Models.Entity;
using TicketSystem.Repository.Interfaces;

namespace TicketSystem.Repository.Account
{
    public class IssueRepository : IIssueRepository
    {
        private readonly IUnitOfWork _unitOfWork;

        public IssueRepository(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        public async Task<IEnumerable<RoleIssueAction>> GetRoleIssueAction()
        {
            return await _unitOfWork.DbConnection.GetListAsync<RoleIssueAction>();
        }

        public async Task<IEnumerable<RoleIssueStatus>> GetRoleIssueStatus()
        {
            return await _unitOfWork.DbConnection.GetListAsync<RoleIssueStatus>();
        }

        public async Task<Issue> GetByRoleAndAction(int issueID, List<int> roleIDs, IssueActionEnum issueAction)
        {
            var sql = @"SELECT t.*
                        FROM issue t
                        INNER JOIN issuetype tt ON t.issueType = tt.ID
                        INNER JOIN roleissueaction rtt ON rtt.issueType = tt.ID
                        WHERE t.issueID = @issueID 
                        AND rtt.roleID IN @roleIDs 
                        AND rtt.issueAction = @issueAction
                        AND t.isDeleted = 0";

            var issue = await _unitOfWork.DbConnection.QuerySingleOrDefaultAsync<Issue>(sql, new { issueID, roleIDs, issueAction });
            return issue;
        }

        public async Task<Issue> Query(int issueID)
        {
            return await _unitOfWork.DbConnection.GetAsync<Issue>(new { issueID, isDeleted = false });
        }

        public async Task Update(Issue issue)
        {
            await _unitOfWork.DbConnection.UpdateAsync(issue);
        }

        public async Task Delete(int issueID)
        {
            await _unitOfWork.DbConnection.DeleteAsync(issueID);
        }
    }
}
