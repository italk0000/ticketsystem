using System.Security.Claims;
using IdentityModel;

namespace TicketSystem.Common.Extensions
{
    public static class PrincipalExtension
    {
        public static (string idStr, int? id) GetSubject(this ClaimsPrincipal principal)
        {
            if (principal == null || principal.Claims == null || principal.Claims.Count() == 0)
                return (null, null);

            var subjectClaim = principal.Claims
                .Where(a => (a.Type == ClaimTypes.NameIdentifier || a.Type == JwtClaimTypes.Subject) && !string.IsNullOrEmpty(a.Value))
                .FirstOrDefault();

            if (subjectClaim == null)
                return (null, null);

            var idStr = subjectClaim.Value;
            if (int.TryParse(idStr, out int id))
            {
                return (idStr, id);
            }
            else
            {
                return (idStr, null);
            }
        }

        public static List<int> GetRoles(this ClaimsPrincipal principal)
        {
            if (principal == null || principal.Claims == null || principal.Claims.Count() == 0)
                return null;

            var roles = principal.Claims
                .Where(a => (a.Type == ClaimTypes.Role || a.Type == JwtClaimTypes.Role) && !string.IsNullOrEmpty(a.Value))
                .Select(p => p.Value)
                .ToList();

            if (roles == null || roles.Count == 0)
                return null;

            var result = new List<int>();
            foreach (var item in roles)
            {
                if (int.TryParse(item, out int id))
                {
                    result.Add(id);
                }
            }
            return result;
        }

        public static string GetName(this ClaimsPrincipal principal)
        {
            if (principal == null || principal.Claims == null || principal.Claims.Count() == 0)
                return null;

            if (principal.Identity == null || !principal.Identity.IsAuthenticated)
                return null;

            if (!string.IsNullOrEmpty(principal.Identity.Name))
                return principal.Identity.Name;

            var nameClaim = principal.Claims
                .Where(a => (a.Type == ClaimTypes.Name || a.Type == JwtClaimTypes.Name) && !string.IsNullOrEmpty(a.Value))
                .FirstOrDefault();

            if (nameClaim == null)
                return null;

            return nameClaim.Value;
        }
    }
}
