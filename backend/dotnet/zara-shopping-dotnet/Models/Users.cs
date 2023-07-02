namespace ZaraShopping.Models
{

    public class Users : Address
    {
        public new int Id { get; set; }
        public string Name { get; set; } = string.Empty;
        public string Email { get; set; } = string.Empty;
        public string Password { get; set; } = string.Empty;
        public string ProfilePicture { get; set; } = string.Empty;
        public DateTime DateOfBirth { get; set; }
        public string Gender { get; set; } = string.Empty;
        public string RoleName { get; set; } = string.Empty;
        public bool IsDeleted { get; set; }

    }

}
