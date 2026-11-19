namespace DmcMedSync.Models
{
    public class UserLogIn
    {
        public string Username { get; set; } = string.Empty;
        public string Password { get; set; }
        public string Role { get; set; } = string.Empty;
    }
}
