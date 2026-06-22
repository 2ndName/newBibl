using Microsoft.EntityFrameworkCore;

using newBibl.Models;

namespace newBibl.Data
{
    public class ApplicationDbContext : DbContext
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options)
            : base(options)
        {
        }

        public DbSet<Book> Books { get; set; }
        public DbSet<Author> Authors { get; set; }
        public DbSet<Edition> Editions { get; set; }
    }
}
