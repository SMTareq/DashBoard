using GAMEPORTALCMS.Models.Entity;
using Microsoft.EntityFrameworkCore;

namespace GAMEPORTALCMS.Data
{
    public class LoginDBContext: DbContext
    {
        public LoginDBContext(DbContextOptions<LoginDBContext> options) : base(options)
        {

        }
        public DbSet<DWUser> DWUsers { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);
   
            modelBuilder.Entity<DWUser>().HasKey(u => u.uid);

        }

    }
}
