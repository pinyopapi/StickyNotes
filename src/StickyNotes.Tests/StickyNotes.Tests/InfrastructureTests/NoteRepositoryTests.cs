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

        [Test]
        public async Task GetAllAsync_ShouldReturnAllNotesForUser()
        {
            await _repository.AddAsync(new Note("N1", "C1", _userId));
            await _repository.AddAsync(new Note("N2", "C2", _userId));

            var notes = await _repository.GetAllAsync(_userId);

            Assert.That(notes.Count(), Is.EqualTo(2));
        }

        [Test]
        public async Task UpdateAsync_ShouldPersistChanges()
        {
            var note = new Note("Old", "OldContent", _userId);
            await _repository.AddAsync(note);

            note.Update("New", "NewContent");
            await _repository.UpdateAsync(note);

            var updated = await _repository.GetByIdAsync(note.Id);
            Assert.Multiple(() =>
            {
                Assert.That(updated?.Title, Is.EqualTo("New"));
                Assert.That(updated?.Content, Is.EqualTo("NewContent"));
            });
        }

        [Test]
        public async Task DeleteAsync_ShouldRemoveNote()
        {
            var note = new Note("T", "C", _userId);
            await _repository.AddAsync(note);

            await _repository.DeleteAsync(note.Id);

            var deleted = await _repository.GetByIdAsync(note.Id);
            Assert.That(deleted, Is.Null);
        }

        [Test]
        public async Task DeleteAsync_ShouldNotThrow_WhenNoteDoesNotExist()
        {
            var invalidId = Guid.NewGuid();

            Assert.DoesNotThrowAsync(async () => await _repository.DeleteAsync(invalidId));
        }
    }
}