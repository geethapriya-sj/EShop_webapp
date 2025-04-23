using System.ComponentModel.DataAnnotations;

namespace Eshop_Webapp.Models
{
    public class SignUpModel
    {
        public long UserID { get; set; } //Auto incremented primary key

        [Required(ErrorMessage="First Name is required")]
        [StringLength(50,ErrorMessage ="First name cannot be longer than 50 charecters")]
        public string Name { get; set; }

        [Required(ErrorMessage = " Phone Number is required")]
        [Phone(ErrorMessage = "Invalid phone number")]
        public string phoneNumber { get; set; }

        [Required(ErrorMessage = "Email is required")]
        [EmailAddress(ErrorMessage = "Invalid email address")]
        public string Email { get; set; }

        [Required(ErrorMessage = "Password is required")]
        [StringLength(100, ErrorMessage = "Password must be at least {2} characters long", MinimumLength = 6)]
        public string Password { get; set; }

        [Required(ErrorMessage = "Role is required")]
        [StringLength(50, ErrorMessage = "Role cannot be longer than 50 characters")]
        public string Role { get; set; }

    }
}
