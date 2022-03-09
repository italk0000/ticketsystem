using System.ComponentModel.DataAnnotations;

namespace TicketSystem.Common.Models.Request
{
    public class LoginRequest
    {
        /// <summary>
        /// 帳號
        /// </summary>
        [Required]
        public string Username { get; set; }

        /// <summary>
        /// 密碼
        /// </summary>
        [Required]
        public string Password { get; set; }
    }
}
