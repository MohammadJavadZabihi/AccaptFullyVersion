using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AccaptFullyVersion.Core.DTOs
{
    public class AddProductViewModel
    {
        [Required]
        [MaxLength(150)]
        public string ProductName { get; set; } = string.Empty;

        [Required]
        [MaxLength(150)]
        public string Color { get; set; } = string.Empty;

        public decimal ProductPrice { get; set; }

        [Required]
        [MaxLength(150)]
        public string Description { get; set; } = string.Empty ;

        public int PrdoctCount { get; set; }
    }
}
