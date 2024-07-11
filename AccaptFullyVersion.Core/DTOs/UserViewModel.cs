using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Reflection.Metadata.Ecma335;
using System.Text;
using System.Threading.Tasks;

namespace AccaptFullyVersion.Core.DTOs
{
    public class UserRegisterViewModel
    {
        [Display(Name = "UserName")]
        [Required(ErrorMessage = "Pleas Fill the {0}")]
        [MaxLength(150, ErrorMessage = "Invalid UserName Input")]
        public string UserName { get; set; } = string.Empty;

        [Display(Name = "Email")]
        [Required(ErrorMessage = "Pleas Fill the {0}")]
        [MaxLength(150, ErrorMessage = "Invalid Email Input")]
        [EmailAddress(ErrorMessage = "Email Most be like (example@gmail.com)")]
        public string Email { get; set; } = string.Empty;

        [Display(Name = "Password")]
        [Required(ErrorMessage = "Pleas Fill the {0}")]
        [MaxLength(200, ErrorMessage = "Invalid Password Input")]
        public string Password { get; set; } = string.Empty;

        [Display(Name = "RePassword")]
        [Required(ErrorMessage = "Pleas Fill the {0}")]
        [MaxLength(200, ErrorMessage = "Invalid RePassword Input")]
        [Compare("Password")]
        public string RePassword { get; set; } = string.Empty;
    }

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

    public class UserUpdateAccountViewModel
    {
        [Display(Name = "UserName")]
        [MaxLength(150, ErrorMessage = "Invalid UserName Input")]
        public string UserName { get; set; } = string.Empty;

        [Display(Name = "Email")]
        [MaxLength(150, ErrorMessage = "Invalid Email Input")]
        //[EmailAddress(ErrorMessage = "Email Most be like (example@gmail.com)")]
        public string Email { get; set; } = string.Empty;
    }

    public class InformationUserViewModel
    {
        [Display(Name = "UserName")]
        [Required(ErrorMessage = "Pleas Fill the {0}")]
        [MaxLength(150, ErrorMessage = "Invalid UserName Input")]
        public string UserName { get; set; } = string.Empty;

        [Display(Name = "Email")]
        [Required(ErrorMessage = "Pleas Fill the {0}")]
        [MaxLength(150, ErrorMessage = "Invalid Email Input")]
        [EmailAddress(ErrorMessage = "Email Most be like (example@gmail.com)")]
        public string Email { get; set; } = string.Empty;

        public int Wallet { get; set; }
    }

}
