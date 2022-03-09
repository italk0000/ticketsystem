using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;

namespace TicketSystem.Common.Authorizations
{
    public class PermissionAuthorizationHandler : AuthorizationHandler<GlobalPermissionRequirement>
    {
        private readonly IHttpContextAccessor _accessor;

        public PermissionAuthorizationHandler(IHttpContextAccessor accessor)
        {
            _accessor = accessor;
        }

        protected override async Task HandleRequirementAsync(AuthorizationHandlerContext context, GlobalPermissionRequirement requirement)
        {
            // TODO 一般功能的驗證, ex: User的CRUD, Role的CRUD
            context.Succeed(requirement);
            return;
        }
    }
}
