using System.ComponentModel.DataAnnotations;

namespace ASM2.Models
{
    public class User
    {
        [Required(ErrorMessage = "Email is required")]
        [RegularExpression(@"^[a-zA-Z0-9._%+-]+@gmail\.com$", ErrorMessage = "Please enter a valid Gmail address (e.g., user@gmail.com)")]
        public string Email { get; set; }

        [Required(ErrorMessage = "Password is required")]
        [DataType(DataType.Password)]
        public string Pass { get; set; }
    }
}