using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc.Abstractions;

namespace TicketSystem.Common.Authorizations
{
    public class GlobalPermissionRequirement : IAuthorizationRequirement
    {
        public ActionDescriptor ActionDescriptor { get; set; }
    }
}
