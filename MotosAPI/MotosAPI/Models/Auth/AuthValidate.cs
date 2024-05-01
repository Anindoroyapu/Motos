using MotosAPI.Utils;
using MotosAPI.Models;

namespace MotosAPI.Models.Auth
{
    public class AuthValidate
    {
        public static ApiResponse ValidateLoginPost(AuthLogin authLogin)
        {
            var st = Validation.ValidateAll(new List<StatusObj>
            {
                Validation.IsValidEmailFormat(authLogin.Username,"Username/Email Address"),
                Validation.IsValidGeneralLongString(authLogin.Password, "Password", 1),
            });


            return new ApiResponse { Status = st.Status, Title = st.Title };
        }

        public static ApiResponse ValidateRegistrationPost(UserProfile.UserProfile userProfile)
        {
            var st = Validation.ValidateAll(new List<StatusObj>
            {
                Validation.IsValidGeneralString(userProfile.FirstName, "First Name", 1),
                Validation.IsValidGeneralString(userProfile.LastName, "Last Name", 1),
                Validation.IsValidEmailFormat(userProfile.PrimaryEmail, "Email Address"),
                Validation.IsValidPhoneNumberFormat(userProfile.PrimaryPhone, "Phone Number"),
                Validation.IsValidPasswordFormat(userProfile.Password, "Password"),
            });


            return new ApiResponse { Status = st.Status, Title = st.Title };
        }
    }
}
