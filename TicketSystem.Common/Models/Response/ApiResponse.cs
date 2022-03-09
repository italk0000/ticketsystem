using System.Text.Json.Serialization;
using Microsoft.AspNetCore.Http;

namespace TicketSystem.Common.Models.Response
{
    public class ApiResponse
    {
        /// <summary>
        /// 建立 <see cref="ApiResponse"/> 的執行個體
        /// </summary>
        /// <param name="statusCode">HTTP 狀態碼</param>
        /// <param name="message">訊息</param>
        /// <param name="data">資料</param>
        public ApiResponse(int? statusCode = StatusCodes.Status200OK, string message = default)
        {
            StatusCode = statusCode ?? StatusCodes.Status200OK;
            Message = message ?? "Success";
        }

        /// <summary>
        /// HTTP 狀態碼
        /// </summary>
        [JsonPropertyName("status_code")]
        public int StatusCode { get; }

        /// <summary>
        /// 訊息
        /// </summary>
        [JsonPropertyName("message")]
        public string Message { get; }
    }


    public class ApiResponse<T> : ApiResponse
    {
        /// <summary>
        /// 建立 <see cref="ApiResponse"/> 的執行個體
        /// </summary>
        /// <param name="statusCode">HTTP 狀態碼</param>
        /// <param name="message">訊息</param>
        /// <param name="data">資料</param>
        public ApiResponse(int? statusCode = StatusCodes.Status200OK, string message = default, T data = default) : base(statusCode, message)
        {
            Data = data;
        }

        /// <summary>
        /// 資料
        /// </summary>
        public T Data { get; }
    }
}
