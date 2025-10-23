using Microsoft.EntityFrameworkCore;
using StickyNotes.Domain.Entities;

namespace StickyNotes.Infrastructure.Persistence
{
    public class AppDbContext : DbContext
    {
        public AppDbContext(DbContextOptions<AppDbContext> options) : base(options) { }

        public DbSet<Note> Notes { get; set; }
    }
}