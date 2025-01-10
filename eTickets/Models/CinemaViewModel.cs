using System.ComponentModel.DataAnnotations;

namespace eTickets.PL.Models
{
    public class CinemaViewModel
    {
        public int Id { get; set; }
        [Display(Name = "Cinema Logo")]
        [Required(ErrorMessage = "Cinema Logo is required")]
        public string Logo { get; set; }

        [Display(Name = "Cinema Name")]
        [Required(ErrorMessage = "Cinema Name is required")]
        [StringLength(50, MinimumLength = 3, ErrorMessage = "Full Name must be between 3 and 50 chars")]
        public string Name { get; set; }

        [Display(Name = "Description")]
        [Required(ErrorMessage = "Cinema Description is required")]
        public string Description { get; set; }
    }
}
