using System.IdentityModel.Tokens.Jwt;
using System.Linq;

namespace TemplateWithIdentity.Helper
{

    public static class JwtHelper
    {
        public static List<string> GetRolesFromToken(string? token)
        {
            var handler = new JwtSecurityTokenHandler();
            var jwtToken = handler.ReadJwtToken(token);

            // استخراج جميع الأدوار من المطالبة "role"
            var roleClaims = jwtToken.Claims.Where(claim => claim.Type == "role").Select(claim => claim.Value).ToList();
            return roleClaims;
        }

    }

}
