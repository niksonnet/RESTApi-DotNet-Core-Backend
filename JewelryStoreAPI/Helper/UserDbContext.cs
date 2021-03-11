using JewelryStoreAPI.Entity;
using Microsoft.EntityFrameworkCore;

namespace JewelryStoreAPI.Helper
{
    public class UserDbContext : DbContext
    {
        public UserDbContext(DbContextOptions<UserDbContext> options) : base(options) { }
        public DbSet<User> Users { get; set; }
        public DbSet<Discount> Discount { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<User>()
                .HasOne<Discount>(s => s.Discount)
                .WithOne(ad => ad.User)
                .HasForeignKey<Discount>(ad => ad.UserID);
        }
    }
}
