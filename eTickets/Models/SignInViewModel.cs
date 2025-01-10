using System.ComponentModel.DataAnnotations;

namespace eTickets.PL.Models
{
    public class SignInViewModel
    {
        [Required]
        [EmailAddress(ErrorMessage = "Invalid Email")]
        public string Email { get; set; }
        [Required]
        [MinLength(6, ErrorMessage = "Minimum Length is 6 chars")]
        public string Password { get; set; }
        public bool RememberMe { get; set; }
    }
}
