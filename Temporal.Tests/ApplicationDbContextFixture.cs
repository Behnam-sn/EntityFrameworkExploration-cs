using Microsoft.EntityFrameworkCore;

namespace Temporal.Tests
{
    public class ApplicationDbContextFixture
    {
        private const string CONNECTION_STRING = "server=.\\sqlexpress;database=EntityFrameworkExplorationDB;MultipleActiveResultSets=true;trusted_connection=true;encrypt=yes;trustservercertificate=yes;";

        public ApplicationDbContext Context { get; }

        public ApplicationDbContextFixture()
        {
            var optionsBuilder = new DbContextOptionsBuilder<ApplicationDbContext>();
            optionsBuilder.UseSqlServer(CONNECTION_STRING);
            Context = new ApplicationDbContext(optionsBuilder.Options);
            Context.Database.EnsureDeleted();
            Context.Database.EnsureCreated();
        }
    }
}