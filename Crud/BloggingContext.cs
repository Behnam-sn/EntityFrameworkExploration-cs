using Microsoft.EntityFrameworkCore;

namespace Crud
{
    public class BloggingContext : DbContext
    {
        public DbSet<Blog> Blogs { get; set; }
        public DbSet<Post> Posts { get; set; }

        public BloggingContext(DbContextOptions options) : base(options)
        {
        }
    }
}