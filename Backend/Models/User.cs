using System.ComponentModel.DataAnnotations;

namespace BlogApplication.Models
{
    public enum UserRole
    {
        User,
        Admin
    }

    public class User
    {
        public int UserID { get; set; }

        [Required]
        public string Username { get; set; }

        [Required]
        [EmailAddress]
        public string Email { get; set; }

        [Required]
        public string Password { get; set; }

        [Required]
        public string FullName { get; set; }

        [Required]
        public UserRole Role { get; set; } // Property for Role
    }
}
