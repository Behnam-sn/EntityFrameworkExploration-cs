using Microsoft.EntityFrameworkCore;

namespace Temporal.Tests
{
    public class DomainDbContextFixture
    {
        private const string CONNECTION_STRING = "server=.\\sqlexpress;database=EntityFrameworkExplorationDB;MultipleActiveResultSets=true;trusted_connection=true;encrypt=yes;trustservercertificate=yes;";

        public DomainDbContext DomainDbContext { get; }

        public DomainDbContextFixture()
        {
            var optionsBuilder = new DbContextOptionsBuilder<DomainDbContext>();
            optionsBuilder.UseSqlServer(CONNECTION_STRING);
            DomainDbContext = new DomainDbContext(optionsBuilder.Options);
            DomainDbContext.Database.EnsureDeleted();
            DomainDbContext.Database.EnsureCreated();
        }
    }
}