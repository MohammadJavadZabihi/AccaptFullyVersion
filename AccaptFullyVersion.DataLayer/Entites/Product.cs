using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AccaptFullyVersion.DataLayer.Entites
{
    public class Product
    {
        [Key]
        public int ProductId { get; set; }

        [Required]
        [MaxLength(150)]
        public string ProductName { get; set; }

        [Required]
        [MaxLength(150)]
        public string Color { get; set; }

        public decimal ProductPrice { get; set; }

        [Required]
        [MaxLength(150)]
        public string Description { get; set; }

        public int PrdoctCount { get; set; }
    }
}
