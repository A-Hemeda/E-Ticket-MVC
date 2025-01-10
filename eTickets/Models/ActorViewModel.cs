using System.ComponentModel.DataAnnotations;

namespace eTickets.PL.Models
{
    public class ActorViewModel
    {
        public int Id { get; set; }
        [Display(Name = "Profile Picture")]
        [Required(ErrorMessage = "Profile Picture is required")]
        public string ProfilePictureURL { get; set; }
        [Display(Name = "Full Name")]
        [Required(ErrorMessage = "Full Name is required")]
        [StringLength(50, MinimumLength = 3, ErrorMessage = "Full Name must be between 3 and 50 chars")]
        public string FullName { get; set; }
        [Display(Name = "Biography")]
        [Required(ErrorMessage = "Biography is required")]
        public string Bio { get; set; }
    }
}
