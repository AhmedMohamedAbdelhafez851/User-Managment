using NuGet.Protocol;
using System.ComponentModel.DataAnnotations;

namespace WebApplication1.ViewModel
{
    public class ProfileFormViewModel
    {
        public string Id { get; set; }
        [Required(ErrorMessage = "Please enter your first name")]
        [StringLength(100, ErrorMessage = "First name must be between 6 and 100 characters", MinimumLength = 3)]
        [Display(Name = "First Name")]
        public string FirstName { get; set; }

        [Required(ErrorMessage = "Please enter your last name")]
        [StringLength(100, ErrorMessage = "Last name must be between 6 and 100 characters", MinimumLength = 3)]
        [Display(Name = "Last Name")]
        public string LastName { get; set; }
        [Required]
        [EmailAddress]
        [Display(Name = "Email")]
        public string Email { get; set; }

        [Required(ErrorMessage = "Please enter your user name")]
        [Display(Name = "Username")]
        [StringLength(100)]
        public string UserName { get; set; }


    }
}
