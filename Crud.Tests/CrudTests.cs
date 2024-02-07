namespace Crud.Tests
{
    public class CrudTests : IClassFixture<BloggingDbContextFixture>
    {
        private readonly BloggingDbContext context;

        public CrudTests(BloggingDbContextFixture bloggingDbContextFixture)
        {
            context = bloggingDbContextFixture.Context;
        }

        [Fact]
        public async Task Create_Test()
        {
            // Arrange
            var blog = new Blog
            {
                Url = "http://blogs.msdn.com/adonet"
            };
            // Act
            await context.Blogs.AddAsync(blog);
            await context.SaveChangesAsync();
            // Assert
            var actual = context.Blogs.First(b => b.Id == blog.Id);
            actual.Should().BeEquivalentTo(new
            {
                Url = "http://blogs.msdn.com/adonet"
            });
        }

        [Fact]
        public async Task Update_Test()
        {
            // Arrange
            var blog = new Blog
            {
                Url = "http://blogs.msdn.com/adonet"
            };
            await context.Blogs.AddAsync(blog);
            await context.SaveChangesAsync();
            // Act
            blog.Url = "https://devblogs.microsoft.com/dotnet";
            blog.Posts.Add(
                new Post
                {
                    Title = "Hello World",
                    Content = "I wrote an app using EF Core!"
                });
            await context.SaveChangesAsync();
            // Assert
            var actual = context.Blogs.First(b => b.Id == blog.Id);
            actual.Should().BeEquivalentTo(new
            {
                Url = "https://devblogs.microsoft.com/dotnet",
                Posts = new[]
                {
                    new
                    {
                        Title = "Hello World",
                        Content = "I wrote an app using EF Core!"
                    }
                }
            });
        }

        [Fact]
        public async Task Delete_Test()
        {
            // Arrange
            var blog = new Blog
            {
                Url = "http://blogs.msdn.com/adonet"
            };
            await context.Blogs.AddAsync(blog);
            await context.SaveChangesAsync();
            // Act
            context.Blogs.Remove(blog);
            await context.SaveChangesAsync();
            // Assert
            context.Blogs.Should().BeEmpty();
        }
    }
}