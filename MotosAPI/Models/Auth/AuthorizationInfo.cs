namespace MotosAPI.Models.Auth
{
    public class AuthorizationInfo
    {
        public int Status { get; set; }

        public string Title { get; set; } = "";

        public UserProfile.UserProfile UserProfile { get; set; } = new UserProfile.UserProfile();

        public string ReferenceName { get; set; } = "AuthorizationRequired";
    }
}
