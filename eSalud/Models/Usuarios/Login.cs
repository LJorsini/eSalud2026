namespace eSalud.Models
{
    public class Login
    {
        public string Email {get; set;}
        public string Password {get; set;}
    }

    public class RefreshTokenRequest
    {
        public string Email {get; set;}
        public string RefreshToken {get; set;}
    }

    public class LogoutRequest
    {
        public string Email {get; set;}
    }
}