using AutoMapper;
using Microsoft.AspNetCore.Http;
using TicketSystem.Common.Enums;
using TicketSystem.Common.Exceptions;
using TicketSystem.Common.Extensions;
using TicketSystem.Common.Models.Dto;
using TicketSystem.Common.Models.Entity;
using TicketSystem.Common.Models.Request;
using TicketSystem.Common.Models.Response;
using TicketSystem.Repository.Interfaces;
using TicketSystem.Service.Interfaces;

namespace TicketSystem.Service.Account
{
    public class IssueService : IIssueService
    {
        private readonly IIssueRepository _issueRepository;
        private readonly IHttpContextAccessor _accessor;
        private readonly IMapper _mapper;

        public IssueService(IIssueRepository issueRepository, IMapper mapper, IHttpContextAccessor accessor)
        {
            _issueRepository = issueRepository;
            _mapper = mapper;
            _accessor = accessor;
        }

        public async Task<ApiResponse> Create(CreateIssueRequest request)
        {
            var roleTicketActions = await _issueRepository.GetRoleIssueAction();
            var roles = _accessor?.HttpContext?.User.GetRoles();

            if (roleTicketActions.Where(x => roles.Contains(x.RoleID) &&
                                        x.IssueType == request.IssueType &&
                                        x.IssueAction == IssueActionEnum.Create).Any() == false)
            {
                throw new HttpCodeException(StatusCodes.Status403Forbidden);
            }

            var issue = _mapper.Map<Issue>(request);
            issue.IssueStatus = IssueStatusEnum.New;
            issue.IsDeleted = false;
            issue.UpdatedUser = _accessor?.HttpContext?.User?.GetName();
            issue.UpdatedTime = DateTime.Now;
            issue.CreatedUser = _accessor?.HttpContext?.User?.GetName();
            issue.CreatedTime = DateTime.Now;

            await _issueRepository.Update(issue);

            return new ApiResponse();
        }

        public async Task<ApiResponse> Delete(int issueID)
        {
            var issue = await TryGetByTicketAction(issueID, IssueActionEnum.Delete);
            issue.IsDeleted = true;
            issue.UpdatedUser = _accessor?.HttpContext?.User?.GetName();
            issue.UpdatedTime = DateTime.Now;

            await _issueRepository.Update(issue);

            return new ApiResponse();
        }

        public async Task<ApiResponse<IssueDto>> Query(int issueID)
        {
            var issue = await TryGetByTicketAction(issueID, IssueActionEnum.Read);

            return new ApiResponse<IssueDto>(data: _mapper.Map<IssueDto>(issue));
        }

        public async Task<ApiResponse> Update(UpdateIssueRequest request)
        {
            await TryGetByTicketAction(request.IssueID, IssueActionEnum.Update);

            var issue = _mapper.Map<Issue>(request);
            issue.UpdatedUser = _accessor?.HttpContext?.User?.GetName();
            issue.UpdatedTime = DateTime.Now;

            await _issueRepository.Update(issue);

            return new ApiResponse();
        }

        public async Task<ApiResponse> UpdateStatus(UpdateIssueStatusRequest request)
        {
            var issue = await _issueRepository.Query(request.IssueID);
            if (issue == null)
            {
                throw new HttpCodeException(StatusCodes.Status404NotFound);
            }

            // 更新狀態不檢查 roleticketaction, 但要檢查 roleticketstatus
            var roleTicketStatus = await _issueRepository.GetRoleIssueStatus();
            var roles = _accessor?.HttpContext?.User.GetRoles();

            if (roleTicketStatus.Where(x => roles.Contains(x.RoleID) &&
                                       x.IssueType == issue.TicketType &&
                                       x.IssueStatus == request.IssueStatus).Any() == false)
            {
                throw new HttpCodeException(StatusCodes.Status403Forbidden);
            }

            issue.IssueStatus = request.IssueStatus;
            issue.UpdatedUser = _accessor?.HttpContext?.User?.GetName();
            issue.UpdatedTime = DateTime.Now;

            await _issueRepository.Update(issue);

            return new ApiResponse();
        }

        private async Task<Issue> TryGetByTicketAction(int issueID, IssueActionEnum issueAction)
        {
            var roleIDs = _accessor?.HttpContext?.User?.GetRoles();
            var issue = await _issueRepository.GetByRoleAndAction(issueID, roleIDs, issueAction);

            if (issue == null)
            {
                throw new HttpCodeException(StatusCodes.Status403Forbidden);
            }

            return issue;
        }
    }
}
