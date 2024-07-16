using AccaptFullyVersion.DataLayer.Entites;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AccaptFullyVersion.DataLayer.Context
{
    public class AccaptContext : DbContext
    {
        public AccaptContext(DbContextOptions<AccaptContext> options) : base(options)
        {
            
        }

        #region User Table

        public DbSet<User> Users { get; set; }
        public DbSet<Wallet> Wallet { get; set; }
        public DbSet<Product> Product { get; set; }

        #endregion
    }
}
