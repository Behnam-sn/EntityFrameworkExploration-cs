using Common;
using Microsoft.EntityFrameworkCore;

namespace Crud.Tests
{
    public class BloggingDbContextFixture : IAsyncDisposable
    {
        public BloggingDbContext Context { get; }

        public BloggingDbContextFixture()
        {
            var optionsBuilder = new DbContextOptionsBuilder<BloggingDbContext>();
            optionsBuilder.UseSqlServer(CommonConstants.CONNECTION_STRING);
            Context = new BloggingDbContext(optionsBuilder.Options);
            Context.Database.EnsureDeleted();
            Context.Database.EnsureCreated();
        }

        public async ValueTask DisposeAsync()
        {
            await Context.DisposeAsync();
        }
    }
}