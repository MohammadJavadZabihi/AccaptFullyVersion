using System.ComponentModel.DataAnnotations;

namespace AccaptFullyVersion.Core.DTOs
{
    public class UserLoginViewModel
    {
        [Display(Name = "UserName")]
        [Required(ErrorMessage = "Pleas Fill the {0}")]
        [MaxLength(150, ErrorMessage = "Invalid UserName Input")]
        public string UserName { get; set; } = string.Empty;

        [Display(Name = "Password")]
        [Required(ErrorMessage = "Pleas Fill the {0}")]
        [MaxLength(200, ErrorMessage = "Invalid Password Input")]
        public string Password { get; set; } = string.Empty;

        public bool RememberMe { get; set; }
    }

}
