using Microsoft.EntityFrameworkCore;
using StickyNotes.Domain.Entities;

namespace StickyNotes.Infrastructure.Persistence
{
    public class AppDbContext : DbContext
    {
        public AppDbContext(DbContextOptions<AppDbContext> options) : base(options) { }

        public DbSet<Note> Notes => Set<Note>();

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            modelBuilder.Entity<Note>(entity =>
            {
                entity.HasKey(n => n.Id);

                entity.Property(n => n.Title)
                      .IsRequired()
                      .HasMaxLength(200);

                entity.Property(n => n.Content)
                      .HasMaxLength(4000);

                entity.Property(n => n.Color)
                      .HasMaxLength(50);

                entity.Property(n => n.Tags)
                      .HasConversion(
                          v => string.Join(',', v ?? new List<string>()),
                          v => v.Split(',', StringSplitOptions.RemoveEmptyEntries).ToList()
                      );

                entity.Property(n => n.CreatedAt)
                      .IsRequired();

                entity.Property(n => n.UpdatedAt)
                      .IsRequired(false);
            });
        }
    }
}