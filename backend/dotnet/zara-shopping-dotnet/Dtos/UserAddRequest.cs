namespace ZaraShopping.Dtos
{
    public class UserAddRequest
    {
        public string UserName { get; set; } = string.Empty;
        public string FirstName { get; set; } = string.Empty;
        public string MiddleName { get; set; } = string.Empty;
        public string LastName { get; set; } = string.Empty;
        public string Email { get; set; } = string.Empty;  
        public string Password { get; set; } = string.Empty;
        public string ProfilePicture { get; set;} = string.Empty;
        public int? Age { get; set; }
        public int RoleId { get; set; }

    }
}
