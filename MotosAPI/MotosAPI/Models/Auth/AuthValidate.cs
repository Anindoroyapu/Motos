using MotosAPI.Utils;

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

        public static ApiResponse ValidateRegistrationPost(AuthRegistration authRegistration)
        {
            var st = Validation.ValidateAll(new List<StatusObj>
            {
                Validation.IsValidGeneralString(authRegistration.FirstName, "First Name", 1),
                Validation.IsValidGeneralString(authRegistration.LastName, "Last Name", 1),
                Validation.IsValidEmailFormat(authRegistration.Email, "Email Address"),
                Validation.IsValidPhoneNumberFormat(authRegistration.Phone, "Phone Number"),
                Validation.IsValidPasswordFormat(authRegistration.Password, "Password"),
                Validation.IsValidPasswordFormat(authRegistration.ConfirmPassword, "Confirm Password"),
                Validation.IsTrue(authRegistration.Password == authRegistration.ConfirmPassword, "Passwords doesn't match"),
            });


            return new ApiResponse { Status = st.Status, Title = st.Title };
        }
    }
}
