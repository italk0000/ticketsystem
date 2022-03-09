using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc.Filters;

namespace TicketSystem.Common.Filters
{
    public class GlobalPermissionFilter : IAsyncAuthorizationFilter
    {
        private readonly IAuthorizationService _authorization;

        public GlobalPermissionFilter(IAuthorizationService authorization)
        {
            _authorization = authorization;
        }

        public async Task OnAuthorizationAsync(AuthorizationFilterContext context)
        {
            var hasAllowAnonymous = context.ActionDescriptor.EndpointMetadata?.Any(p => p is AllowAnonymousAttribute);
            if (hasAllowAnonymous == true)
            {
                return;
            }

            // 自訂驗證
            //var result = await this._authorization.AuthorizeAsync(context.HttpContext.User, null, new GlobalPermissionRequirement()
            //{
            //    ActionDescriptor = context.ActionDescriptor
            //});
        }
    }
}
