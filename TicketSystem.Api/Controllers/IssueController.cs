using Microsoft.AspNetCore.Mvc;
using TicketSystem.Common.Models.Dto;
using TicketSystem.Common.Models.Request;
using TicketSystem.Common.Models.Response;
using TicketSystem.Service.Interfaces;

namespace TicketSystem.Api.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class IssueController : ControllerBase
    {
        private readonly ILogger<IssueController> _logger;
        private readonly IIssueService _service;

        public IssueController(ILogger<IssueController> logger, IIssueService service)
        {
            _logger = logger;
            _service = service;
        }

        /// <summary>
        /// 以ID查詢
        /// </summary>
        /// <param name="issueID"></param>
        /// <returns></returns>
        [HttpGet("{issueID}")]
        public async Task<ApiResponse<IssueDto>> Get(int issueID)
        {
            return await _service.Query(issueID);
        }

        /// <summary>
        /// 開單
        /// </summary>
        /// <returns></returns>
        [HttpPost]
        public async Task<ApiResponse> Post(CreateIssueRequest request)
        {
            return await _service.Create(request);
        }

        /// <summary>
        /// 修改
        /// </summary>
        /// <returns></returns>
        [HttpPut]
        public async Task<ApiResponse> Put(UpdateIssueRequest request)
        {
            return await _service.Update(request);
        }

        /// <summary>
        /// 刪除
        /// </summary>
        /// <param name="issueID"></param>
        /// <returns></returns>
        [HttpDelete("{issueID}")]
        public async Task<ApiResponse> Delete(int issueID)
        {
            return await _service.Delete(issueID);
        }

        /// <summary>
        /// 更新狀態
        /// </summary>
        /// <param name="request"></param>
        /// <returns></returns>
        [HttpPut("Status")]
        public async Task<ApiResponse> UpdateStatus(UpdateIssueStatusRequest request)
        {
            return await _service.UpdateStatus(request);
        }
    }
}
