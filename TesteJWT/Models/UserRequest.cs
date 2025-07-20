namespace TesteJWT.Models
{
    public class UserRequest
    {
        public string? UserName { get; set; }

        public string? Email { get; set; }

        public string? PasswordHash { get; set; }
    }
}