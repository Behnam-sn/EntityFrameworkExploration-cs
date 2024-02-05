using Domain;
using Microsoft.EntityFrameworkCore;

namespace Temporal
{
    public class DomainDbContext : DbContext
    {
        public DbSet<Document> Documents { get; set; }
        public DbSet<ParameterValue> ParameterValues { get; set; }

        public DomainDbContext(DbContextOptions options) : base(options)
        {
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Document>().ToTable("Documents", b => b.IsTemporal());
            modelBuilder.Entity<ParameterValue>().ToTable("ParameterValues", b => b.IsTemporal());
        }
    }
}