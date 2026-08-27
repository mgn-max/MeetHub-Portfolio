namespace MeetHub.API.Models.RequestsAuth
{
    public class LoginRequest
    {
        public string LoginOrEmail { get; set; } = string.Empty;

        public string Password { get; set; } = string.Empty;
    }
}
