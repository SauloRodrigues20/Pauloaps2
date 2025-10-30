using Library.Infrastructure.Data;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Design;

namespace Library.Infrastructure.Factory
{
    /// <summary>
    /// Factory for creating LibraryDbContext instances at design time (for migrations)
    /// </summary>
    public class LibraryDbContextFactory : IDesignTimeDbContextFactory<LibraryDbContext>
    {
        public LibraryDbContext CreateDbContext(string[] args)
        {
            var optionsBuilder = new DbContextOptionsBuilder<LibraryDbContext>();
            optionsBuilder.UseSqlite("Data Source=library.db");

            return new LibraryDbContext(optionsBuilder.Options);
        }
    }
}
