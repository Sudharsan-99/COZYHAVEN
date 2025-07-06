using System.ComponentModel.DataAnnotations;

namespace CozyHaven.MVC.Models
{
    public class UserViewModel
    {
        public int Id { get; set; }

        [Required]
        [MaxLength(100)]
        public string FullName { get; set; }

        [Required]
        [EmailAddress]
        [MaxLength(100)]
        public string Email { get; set; }

        [Required]
        [MaxLength(255)]
        [DataType(DataType.Password)]
        public string PasswordHash { get; set; }

        [Required]
        [MaxLength(20)]
        public string Role { get; set; } // User, HotelOwner, Admin

        [MaxLength(10)]
        public string Gender { get; set; }

        [Phone]
        [MaxLength(15)]
        public string ContactNumber { get; set; }

        [MaxLength(200)]
        public string Address { get; set; }
    }
}
