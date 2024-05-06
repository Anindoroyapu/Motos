using MotosAPI.Data;
using Microsoft.EntityFrameworkCore;
using MotosAPI.Utils;

namespace MotosAPI.Models.Auth
{
    public class AuthorizationCustomer
    {
        public static async Task<AuthorizationInfo> Verify(ApplicationDbContext _context, HttpRequest request, List<string>? permissions = null)
        {
            try
            {
                //--Verify Auth Data
                var headers = request.Headers;
                var token = headers["Authorization"];

                var jti = JwtOperation.GetJti(token);
                if (jti == null)
                {
                    return new AuthorizationInfo
                    {
                        Status = 1,
                        Title = "Login Required",
                    };
                }

                return await GetAuthLog(_context, jti, permissions);
            }
            catch // (Exception e)
            {
                return new AuthorizationInfo
                {
                    Status = 1,
                    Title = "Login Required",
                };
            }
        }

        public static async Task<AuthorizationInfo> GetAuthLog(ApplicationDbContext _context, string jti, List<string>? permissions)
        {
            //--Check JTI on DB
            var authInfo = await _context.UserAuthLog.FirstOrDefaultAsync(u => u.LoginKey == jti);
            if (authInfo == null)
            {
                return new AuthorizationInfo
                {
                    Status = 1,
                    Title = "JTI Not Found",
                };
            }

            if (authInfo.TimeExpire < TimeOperation.GetUnixTime())
            {
                return new AuthorizationInfo
                {
                    Status = 1,
                    Title = "Token Expired",
                };
            }

            return await GetUserProfile(_context, authInfo);
        }

        public static async Task<AuthorizationInfo> GetUserProfile(ApplicationDbContext _context, UserAuthLog.UserAuthLog userAuthLog, List<string>? permissions)
        {
            

            //--Collect User Info form UserProfile
            var userProfile = await _context.UserProfile.FirstOrDefaultAsync(u => u.Id == userAuthLog.UserId && u.TimeDeleted == 0);
            if (userProfile == null)
            {
                return new AuthorizationInfo
                {
                    Status = 1,
                    Title = "User Not Found",
                };
            }

            if (userProfile.Status != "active")
            {
                return new AuthorizationInfo
                {
                    Status = 1,
                    Title = "User is not in active status",
                };
            }

            if (userProfile.Role == "")
            {
                return new AuthorizationInfo
                {
                    Status = 1,
                    Title = "User Role Not Found",
                };
            }

            return new AuthorizationInfo
            {
                Status = 0,
                Title = "Success",
                UserProfile = userProfile,
                ReferenceName = "",
            };
        }
    }
}
