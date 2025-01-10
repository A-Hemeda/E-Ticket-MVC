using System.ComponentModel.DataAnnotations;

namespace eTickets.PL.Models
{
    public class RegisterViewModel
    {
        [Required]
        [EmailAddress(ErrorMessage = "Invalid Email")]
        public string Email { get; set; }
        [Required]
        [StringLength(6,MinimumLength =6)]
        public string Password { get; set; }
        [Required]
        [StringLength(6, MinimumLength = 6)]
        [Compare(nameof(Password), ErrorMessage = "Mismatch Password")]
        
        public string ConfirmPassword { get; set; }
        [Display(Name = "Full name")]
        public string FullName { get; set; }
    }
}
