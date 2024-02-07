using Common;
using Microsoft.EntityFrameworkCore;

namespace Crud.Tests
{
    public class BloggingContextFixture : IAsyncDisposable
    {
        public BloggingContext Context { get; }

        public BloggingContextFixture()
        {
            var optionsBuilder = new DbContextOptionsBuilder<BloggingContext>();
            optionsBuilder.UseSqlServer(CommonConstants.CONNECTION_STRING);
            Context = new BloggingContext(optionsBuilder.Options);
            Context.Database.EnsureDeleted();
            Context.Database.EnsureCreated();
        }

        public async ValueTask DisposeAsync()
        {
            await Context.DisposeAsync();
        }
    }
}