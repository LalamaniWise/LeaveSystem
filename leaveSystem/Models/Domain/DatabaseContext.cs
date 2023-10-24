using leaveSystem.Models.DTO;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using System.Reflection.Emit;

namespace leaveSystem.Models.Domain
{
    public class DatabaseContext : IdentityDbContext<ApplicationUser>
    {
        public DbSet<Leave> Leave { get; set; }
       // public DbSet<Leave> Request { get; set; }
        public DatabaseContext(DbContextOptions<DatabaseContext>options):base(options) 
        {

        }

        protected override void OnModelCreating(ModelBuilder builder)
        {
           // builder.Entity<Leave>().HasNoKey();
            base.OnModelCreating(builder);

            // Define the foreign key relationship between ApplicationUser and Data
            builder.Entity<Leave>()
                .HasOne(d => d.User)
                .WithMany(u => u.LeaveList)
                .HasForeignKey(d => d.UserId)
                .OnDelete(DeleteBehavior.Cascade);
        }

        public DbSet<leaveSystem.Models.DTO.Request> Request { get; set; } = default!;

    }
}
