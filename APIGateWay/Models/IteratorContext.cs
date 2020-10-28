using Microsoft.EntityFrameworkCore;

namespace APIGateWay.Models
{
    public class IteratorContext : DbContext
    {
        public IteratorContext(DbContextOptions<IteratorContext> options)
    : base(options)
        {
        }

        public DbSet<Iterator> Iterator { get; set; }
    }
}
