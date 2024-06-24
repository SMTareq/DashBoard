using GAMEPORTALCMS.Models.Entity;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Reflection.Emit;

namespace GAMEPORTALCMS.Data
{
    public class AppDBContext : DbContext
    {
        public AppDBContext(DbContextOptions<AppDBContext> options) : base(options)
        {

        }
    
        public DbSet<User> Users { get; set; }
        public DbSet<GameCategory> GameCategorys { get; set; }
      
        public DbSet<EBL_Migration> EBL_Migrations { get; set; }
        public DbSet<_EBL_POC> _EBL_POCs { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            modelBuilder.Entity<User>()
                .HasKey(u => u.Id);

            modelBuilder.Entity<GameCategory>().HasKey(u => u.Id);
 
            modelBuilder.Entity<EBL_Migration>().HasKey(u => u.DWDOCID);
            modelBuilder.Entity<_EBL_POC>().HasKey(u=>u.DWDOCID);

        }
    }
}
