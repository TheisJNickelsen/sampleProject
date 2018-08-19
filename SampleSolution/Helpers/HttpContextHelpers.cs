using System.Security.Claims;

namespace SampleSolution.Helpers
{
    public static class HttpContextHelpers
    {
        public static string GetCurrentUserEmail(ClaimsPrincipal user)
        {
            return user.FindFirst(ClaimTypes.NameIdentifier).Value;
        }
    }
}
