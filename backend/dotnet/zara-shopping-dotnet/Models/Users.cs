namespace ZaraShopping.Models
{

    public class Users
    {
        public int Id { get; set; }
        public string UserName { get; set; } = string.Empty;
        public string FirstName { get; set; } = string.Empty;
        public string MiddleName { get; set; }
        public string LastName { get; set; } = string.Empty;
        public string Email { get; set; } = string.Empty;
        public string Password { get; set; } = string.Empty;
        public string ProfilePicture { get; set; } = string.Empty;
        public DateTime DateRegistered { get; set; }
        public int? Age { get; set; }
        public int RoleId { get; set; }
        public bool IsDeleted { get; set; }
        

    }

}
