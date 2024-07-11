using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using MotosAPI.Data;
using MotosAPI.Models.Auth;
using MotosAPI.Models.UserProfile;
using MotosAPI.Utils;

namespace MotosAPI.Controllers
{
    [Route("api/auth")]
    [ApiController]
    public class AuthController : Controller
    {
        private readonly ApplicationDbContext _context;
        private readonly IConfiguration _config;
        public AuthController(ApplicationDbContext context, IConfiguration config)
        {
            _context = context;
            _config = config;
        }

        [HttpPost("login")]
        public async Task<ActionResult<IEnumerable<Auth>>> AuthLogin(AuthLogin authLogin)
        {
            //Validation
            var validate = AuthValidate.ValidateLoginPost(authLogin);

            if (validate.Status != 0)
            {
                return Ok(ApiResponseHandler.Error(validate.Title));
            }

            //Check user on DB
            var userInfo = await _context.UserProfile.FirstOrDefaultAsync(u => (u.PrimaryPhone == authLogin.Username || u.PrimaryEmail == authLogin.Username));

            if (userInfo == null)
            {
                return Ok(ApiResponseHandler.Error("User Not Found"));
            }

            //--Match Password
            var passwordMatch = BCrypt.Net.BCrypt.Verify(authLogin.Password, userInfo.Password);
            if (!passwordMatch)
            {
                return Ok(ApiResponseHandler.Error("Password Doesn't Match"));
            }

            var tokenStm = await AuthOperation.SavingToken(_context, Request, _config, userInfo);
            if (tokenStm == "")
            {
                return Ok(ApiResponseHandler.Error("Token Not Generated"));
            }

            //--Return
            return Ok(ApiResponseHandler.Success("Login Successfully", new
            {
                Token = tokenStm,
            }));
        }


        //api/auth/registration
        [HttpPost("create-account")]
        public async Task<ActionResult<IEnumerable<Auth>>> AuthCreateAccount(UserProfile userProfile)
        {
            //--Validate
            var validate = AuthValidate.ValidateRegistrationPost(userProfile);
            if (validate.Status != 0)
            {
                return Ok(ApiResponseHandler.Error(validate.Title));
            }

            //--Check If PrimaryEmail Already Exist
            var emailExist = await _context.UserProfile.FirstOrDefaultAsync(u => u.UserName == userProfile.PrimaryEmail || u.PrimaryEmail == userProfile.PrimaryEmail);
            if (emailExist != null)
            {
                return Ok(ApiResponseHandler.Error("Email Already Exist", "Email"));
            }

            //--Check If Primary Phone Already exist
            var phoneExist = await _context.UserProfile.FirstOrDefaultAsync(x => x.UserName == userProfile.PrimaryPhone || x.PrimaryPhone == userProfile.PrimaryPhone);
            if (phoneExist != null)
            {
                return Ok(ApiResponseHandler.Error("Phone Number Already Exist"));
            }

            //--Creating User
            var user = new UserProfile
            {
                FirstName = userProfile.FirstName,
                LastName = userProfile.LastName,
                PrimaryEmail = userProfile.PrimaryEmail,
                PrimaryPhone = userProfile.PrimaryPhone,
                UserName = "",
                DateOfBirth = 0,
                Gender = "",
                Password = BCrypt.Net.BCrypt.HashPassword(userProfile.Password),
                
            };

            _context.UserProfile.Add(user);
            await _context.SaveChangesAsync();

            return Ok(ApiResponseHandler.Success("Account Created Successfully"));
        }

    }
}
