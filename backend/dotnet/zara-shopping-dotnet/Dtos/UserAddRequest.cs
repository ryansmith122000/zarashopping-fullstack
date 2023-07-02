namespace ZaraShopping.Dtos
{
    public class UserAddRequest
    {
        public string Name { get; set; } = string.Empty;
        public string Email { get; set; } = string.Empty;
        public string Password { get; set; } = string.Empty;
        public string ProfilePicture { get; set; } = string.Empty;
        public string DateOfBirth { get; set; } = string.Empty;
        public string Gender { get; set; } = string.Empty;
        public string Street { get; set; } = string.Empty;
        public string LineTwo { get; set; } = string.Empty;
        public string City { get; set; } = string.Empty;
        public string State { get; set; } = string.Empty;
        public int ZipCode { get; set; }
        public string Country { get; set;} = string.Empty;
        public int RoleId { get; set; } = 1;
        public int CreatedBy { get; set; }
    }
}
