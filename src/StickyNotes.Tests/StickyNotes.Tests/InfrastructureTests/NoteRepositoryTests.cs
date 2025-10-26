using Microsoft.EntityFrameworkCore;
using StickyNotes.Domain.Entities;
using StickyNotes.Infrastructure.Persistence;
using StickyNotes.Infrastructure.Repositories;

namespace StickyNotes.Tests.InfrastructureTests
{
    [TestFixture]
    public class NoteRepositoryTests
    {
        private NoteRepository _repository;
        private AppDbContext _context;
        private Guid _userId;

        [SetUp]
        public void Setup()
        {
            var options = new DbContextOptionsBuilder<AppDbContext>()
                .UseInMemoryDatabase(databaseName: "TestDb")
                .Options;

            _context = new AppDbContext(options);
            _repository = new NoteRepository(_context);
            _userId = Guid.NewGuid();
        }

        [TearDown]
        public void TearDown()
        {
            _context.Database.EnsureDeleted();
            _context.Dispose();
        }

        [Test]
        public async Task AddAsync_ShouldAddNote()
        {
            var note = new Note("Title", "Content", _userId);

            await _repository.AddAsync(note);

            var savedNote = await _repository.GetByIdAsync(note.Id);
            Assert.That(savedNote, Is.Not.Null);
            Assert.That(savedNote.Title, Is.EqualTo("Title"));
        }

    }
}