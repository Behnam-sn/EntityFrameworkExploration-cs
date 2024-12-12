using Microsoft.EntityFrameworkCore;
using TPH.Entities;

namespace TPH;

public sealed class ApplicationDbContext : DbContext
{
    public ApplicationDbContext(DbContextOptions options) : base(options)
    {
    }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<User>()
            .ToTable("Users")
            .HasDiscriminator<string>("user_type")
            .HasValue<Customer>("user_customer")
            .HasValue<Vendor>("user_vendor")
            .HasValue<Admin>("user_admin");
    }
}
