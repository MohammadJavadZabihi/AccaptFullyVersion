using AccaptFullyVersion.DataLayer.Entites;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Reflection.Metadata.Ecma335;
using System.Security.Principal;
using System.Text;
using System.Threading.Tasks;

namespace AccaptFullyVersion.Core.DTOs
{
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
