using StickyNotes.Api.Controllers;

namespace StickyNotes.Tests.ApiTests
{

    [TestFixture]
    public class NotesControllerTests
    {
        private NotesController _controller;
        private FakeNoteService _service;
        private Guid _userId;

        [SetUp]
        public void Setup()
        {
            _service = new FakeNoteService();
            _controller = new NotesController(_service);
            _userId = Guid.NewGuid();
        }

        [Test]
        public async Task Create_ShouldReturnCreatedNote()
        {
            var request = new CreateNoteRequest("Title", "Content", _userId);
            var result = await _controller.Create(request);
            Assert.That(result, Is.Not.Null);
        }

        [Test]
        public async Task GetAll_ShouldReturnNotes()
        {
            await _service.CreateNoteAsync("T", "C", _userId);
            var response = await _controller.GetAll(_userId);
            Assert.That(response, Is.Not.Null);
        }

        [Test]
        public async Task Update_ShouldReturnUpdatedNote()
        {
            var note = await _service.CreateNoteAsync("Old Title", "Old Content", _userId);
            var request = new UpdateNoteRequest("New Title", "New Content");

            var result = await _controller.Update(note.Id, request);

            Assert.Multiple(() =>
            {
                Assert.That(result, Is.Not.Null);
                Assert.That(note.Title, Is.EqualTo("New Title"));
                Assert.That(note.Content, Is.EqualTo("New Content"));
            });
        }

        [Test]
        public async Task Delete_ShouldRemoveNote()
        {
            var note = await _service.CreateNoteAsync("Title", "Content", _userId);

            await _controller.Delete(note.Id);

            var allNotes = await _service.GetAllNotesAsync(_userId);
            Assert.That(allNotes.Any(n => n.Id == note.Id), Is.False);
        }

        [Test]
        public async Task Pin_ShouldSetNoteAsPinned()
        {
            var note = await _service.CreateNoteAsync("Note", "Text", _userId);

            await _controller.Pin(note.Id);

            var updated = await _service.GetNoteByIdAsync(note.Id);
            Assert.That(updated.Pinned, Is.True);
        }

        [Test]
        public async Task Unpin_ShouldSetNoteAsUnpinned()
        {
            var note = await _service.CreateNoteAsync("Note", "Text", _userId);
            await _controller.Pin(note.Id);

            await _controller.Unpin(note.Id);

            var updated = await _service.GetNoteByIdAsync(note.Id);
            Assert.That(updated.Pinned, Is.False);
        }

        [Test]
        public async Task Archive_ShouldSetNoteAsArchived()
        {
            var note = await _service.CreateNoteAsync("Test", "Archivable", _userId);

            await _controller.Archive(note.Id);

            var updated = await _service.GetNoteByIdAsync(note.Id);
            Assert.Multiple(() =>
            {
                Assert.That(updated.IsArchived, Is.True);
                Assert.That(updated.Pinned, Is.False);
            });
        }

        [Test]
        public async Task Restore_ShouldUnarchiveNote()
        {
            var note = await _service.CreateNoteAsync("Test", "ToRestore", _userId);
            await _controller.Archive(note.Id);

            await _controller.Restore(note.Id);

            var updated = await _service.GetNoteByIdAsync(note.Id);
            Assert.That(updated.IsArchived, Is.False);
        }
    }
}