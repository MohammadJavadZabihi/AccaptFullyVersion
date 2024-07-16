using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AccaptFullyVersion.DataLayer.Entites
{
    public class Wallet
    {
        public Wallet()
        {
            
        }

        [Key]
        public int WalletId { get; set; }

        public int Amount { get; set; }

        public int UserId { get; set; }


        public User User { get; set; }
    }
}
