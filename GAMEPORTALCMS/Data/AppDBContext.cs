using GAMEPORTALCMS.Models.Entity;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Reflection.Emit;

namespace GAMEPORTALCMS.Data
{
    public class AppDBContext : DbContext
    {
        public AppDBContext(DbContextOptions options) : base(options)
        {
        }

        public DbSet<User> Users { get; set; }
        public DbSet<Billboard> Billboards { get; set; }
        public DbSet<DownloadableGame> DownloadableGames { get; set; }
        public DbSet<GameCategory> GameCategorys { get; set; }
        public DbSet<GameType> GameTypes { get; set; }
        public DbSet<OnlineGames> OnlineGamess { get; set; }
        public DbSet<GamePatch> GamePatchs { get; set; }
        public DbSet<PatchGame> PatchGames { get; set; }
        public DbSet<Promotion> Promotions { get; set; }
        public DbSet<CMSUser> CMSUsers { get; set; }

        public DbSet<GamePortalClient> GamePortalClients { get; set; }
        public DbSet<EBL_Migration> EBL_Migrations { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            modelBuilder.Entity<User>()
                .HasKey(u => u.Id);

            modelBuilder.Entity<Billboard>().HasKey(u => u.Id);
            modelBuilder.Entity<DownloadableGame>().HasKey(u => u.Id);
            modelBuilder.Entity<GameCategory>().HasKey(u => u.Id);
            modelBuilder.Entity<GameType>().HasKey(u => u.Id);
            modelBuilder.Entity<OnlineGames>().HasKey(u => u.Id);
            modelBuilder.Entity<GamePatch>().HasKey(u => u.Id);
            modelBuilder.Entity<PatchGame>().HasKey(u => u.Id);
            modelBuilder.Entity<Promotion>().HasKey(u => u.Id);
            modelBuilder.Entity<CMSUser>().HasKey(u => u.Id);
            modelBuilder.Entity<GamePortalClient>().HasKey(u => u.Id);
            modelBuilder.Entity<EBL_Migration>().HasKey(u => u.DWDOCID);

        }
    }
}
