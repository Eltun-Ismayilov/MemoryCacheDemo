using Microsoft.EntityFrameworkCore;

namespace MemoryCacheDemo.Data
{
    public class TestDataContext:DbContext
    {
        public TestDataContext(DbContextOptions options) : base(options)
        {
        }
        public DbSet<Demo> Demos { get; set; }
    }
}
