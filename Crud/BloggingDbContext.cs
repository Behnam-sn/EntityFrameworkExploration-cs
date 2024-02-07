using Microsoft.EntityFrameworkCore;

namespace Crud
{
    public class BloggingDbContext : DbContext
    {
        public DbSet<Blog> Blogs { get; set; }
        public DbSet<Post> Posts { get; set; }

        public BloggingDbContext(DbContextOptions options) : base(options)
        {
        }
    }
}