using Common;
using Microsoft.EntityFrameworkCore;

namespace Temporal.Tests
{
    public class ApplicationDbContextFixture
    {
        public ApplicationDbContext Context { get; }

        public ApplicationDbContextFixture()
        {
            var optionsBuilder = new DbContextOptionsBuilder<ApplicationDbContext>();
            optionsBuilder.UseSqlServer(CommonConstants.CONNECTION_STRING);
            Context = new ApplicationDbContext(optionsBuilder.Options);
            Context.Database.EnsureDeleted();
            Context.Database.EnsureCreated();
        }
    }
}