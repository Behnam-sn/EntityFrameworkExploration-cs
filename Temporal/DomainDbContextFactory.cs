using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Design;

namespace Temporal
{
    public class DomainDbContextFactory : IDesignTimeDbContextFactory<DomainDbContext>
    {
        private const string CONNECTION_STRING = "server=.\\sqlexpress;database=EntityFrameworkExplorationDB;MultipleActiveResultSets=true;trusted_connection=true;encrypt=yes;trustservercertificate=yes;";

        public DomainDbContext CreateDbContext(string[] args)
        {
            var optionsBuilder = new DbContextOptionsBuilder<DomainDbContext>();
            optionsBuilder.UseSqlServer(CONNECTION_STRING);
            return new DomainDbContext(optionsBuilder.Options);
        }
    }
}