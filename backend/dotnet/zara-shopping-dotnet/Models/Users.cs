namespace TestToSQL.Models
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
        public Address? Address { get; set; }
        public string RoleName { get; set; } = string.Empty;
        public int CreatedBy { get; set; }
        public int? ModifiedBy { get; set; }
        public DateTime DateCreated { get; set; }
        public DateTime DateModified { get; set; }
        public bool? IsDeleted { get; set; }
    }
}
