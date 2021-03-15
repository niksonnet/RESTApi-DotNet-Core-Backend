using API.Domain.Entity;
using Microsoft.EntityFrameworkCore;

namespace API.Infrastructure.DbContext
{
    public class UserDbContext : Microsoft.EntityFrameworkCore.DbContext
    {
        public UserDbContext(DbContextOptions<UserDbContext> options) : base(options) { }
        public DbSet<User> Users { get; set; }
        public DbSet<Discount> Discount { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<User>()
                .HasOne(s => s.Discount)
                .WithOne(ad => ad.User)
                .HasForeignKey<Discount>(ad => ad.UserID);
        }

        
    }
}
