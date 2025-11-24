using ApplicationCore.Entities.Identity;
using ApplicationCore.Entities.Models;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace Infrastructure.Database
{
    public class ApplicationDbContext : IdentityDbContext<ApplicationUser, IdentityRole<Guid>, Guid>
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options) : base(options)
        {

        }

        public DbSet<Car> Cars { get; set; }
        public DbSet<CustomerOrder> Orders { get; set; }
        public DbSet<RefreshToken> RefreshTokens { get; set; }

        protected override void OnModelCreating(ModelBuilder builder)
        {
            builder.Entity<RefreshToken>(e => e.HasIndex(r => r.Token).IsUnique());
            builder.Entity<RefreshToken>(e => e.HasOne(r => r.User).WithMany().HasForeignKey(r => r.UserId));

            base.OnModelCreating(builder);
        }
    }
}
