using MotosAPI.Data;
using MotosAPI.Utils;

namespace MotosAPI.Models.Auth
{
    public class AuthOperation
    {
        public static async Task<string> SavingToken(ApplicationDbContext _context, HttpRequest request, IConfiguration _config, UserProfile.UserProfile userInfo)
        {

            
            var jti = Guid.NewGuid().ToString();
            var validityHour = 2;
            var validityDateTime = DateTime.Now.AddHours(validityHour);

            var tokenStr = JwtOperation.MkJwtToken(_config, jti, userInfo.UserName, userInfo.PrimaryEmail, validityDateTime, "");

            userInfo.Token = tokenStr;
            

            return tokenStr;
        }
    }
}
