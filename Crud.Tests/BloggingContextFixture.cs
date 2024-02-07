﻿using Microsoft.EntityFrameworkCore;

namespace Crud.Tests
{
    public class BloggingContextFixture : IAsyncDisposable
    {
        private const string CONNECTION_STRING = "server=.\\sqlexpress;database=EntityFrameworkExplorationDB;MultipleActiveResultSets=true;trusted_connection=true;encrypt=yes;trustservercertificate=yes;";
        public BloggingContext Context { get; }

        public BloggingContextFixture()
        {
            var optionsBuilder = new DbContextOptionsBuilder<BloggingContext>();
            optionsBuilder.UseSqlServer(CONNECTION_STRING);
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