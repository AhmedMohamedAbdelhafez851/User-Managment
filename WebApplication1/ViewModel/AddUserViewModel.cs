using System.ComponentModel.DataAnnotations;

namespace WebApplication1.ViewModel
{
    public class AddUserViewModel
    {
        [Required(ErrorMessage = "Please enter your first name")]
        [StringLength(100, ErrorMessage = "First name must be between 3 and 100 characters", MinimumLength = 3)]
        [Display(Name = "First Name")]
        public string FirstName { get; set; }

        [Required(ErrorMessage = "Please enter your last name")]
        [StringLength(100, ErrorMessage = "Last name must be between 3 and 100 characters", MinimumLength = 3)]
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

        [Required]
        [StringLength(100, ErrorMessage = "The {0} must be at least {2} and at max {1} characters long.", MinimumLength = 6)]
        [DataType(DataType.Password)]
        [Display(Name = "Password")]
        public string Password { get; set; }

        [DataType(DataType.Password)]
        [Display(Name = "Confirm password")]
        [Compare("Password", ErrorMessage = "The password and confirmation password do not match.")]
        public string ConfirmPassword { get; set; }

        public List<RoleViewModel> Roles { get; set; } = new List<RoleViewModel>();
    }
}
