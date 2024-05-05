using System.Security.Claims;

namespace SquashPointAPI.Extensions;

public static class ClaimsExtensions
{
    public static string GetUserEmail(this ClaimsPrincipal user)
    {
        return user.Claims.SingleOrDefault(x =>
            x.Type.Equals("http://schemas.xmlsoap.org/ws/2005/05/identity/claims/emailaddress")).Value;
    }
}