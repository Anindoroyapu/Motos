namespace MotosAPI.Models.Auth
{
    public class AuthRegistration
    {
        public string Organization { get; set; } = "";

        public string FirstName { get; set; } = "";

        public string LastName { get; set; } = "";

        public string Email { get; set; } = "";

        public string Password { get; set; } = "";

        public string ConfirmPassword { get; set; } = "";

        
    }
}
