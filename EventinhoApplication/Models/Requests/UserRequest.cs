namespace EventinhoApplication.Models.Requests
{
    public class UserRequest
    {
        public string? UserName { get; set; }

        public string? Email { get; set; }

        public string? PasswordHash { get; set; }
    }
}