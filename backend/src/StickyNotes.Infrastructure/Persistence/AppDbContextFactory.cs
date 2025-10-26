using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Design;
using System.Diagnostics.CodeAnalysis;

namespace StickyNotes.Infrastructure.Persistence;
[ExcludeFromCodeCoverage]
public class AppDbContextFactory : IDesignTimeDbContextFactory<AppDbContext>
{
    public AppDbContext CreateDbContext(string[] args)
    {
        var optionsBuilder = new DbContextOptionsBuilder<AppDbContext>();
        optionsBuilder.UseSqlite("Data Source=stickyNotes.db");

        return new AppDbContext(optionsBuilder.Options);
    }
}