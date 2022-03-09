using TicketSystem.Common.Models.Dto;
using TicketSystem.Common.Models.Request;
using TicketSystem.Common.Models.Response;

namespace TicketSystem.Service.Interfaces
{
    public interface IIssueService
    {
        Task<ApiResponse> Create(CreateIssueRequest request);
        Task<ApiResponse<IssueDto>> Query(int issueID);
        Task<ApiResponse> Update(UpdateIssueRequest request);
        Task<ApiResponse> Delete(int issueID);
        Task<ApiResponse> UpdateStatus(UpdateIssueStatusRequest request);
    }
}
