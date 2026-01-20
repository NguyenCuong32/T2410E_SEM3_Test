using System.ComponentModel.DataAnnotations;

namespace ComicSystem.DTOs
{
    public class CustomerRegisterDto
    {
        [Required(ErrorMessage = "Full name is required")]
        [StringLength(255)]
        [Display(Name = "Full Name")]
        public string FullName { get; set; } = string.Empty;

        [Required(ErrorMessage = "Phone number is required")]
        [StringLength(15)]
        [Phone(ErrorMessage = "Invalid phone number")]
        [Display(Name = "Phone Number")]
        public string PhoneNumber { get; set; } = string.Empty;
    }
}
