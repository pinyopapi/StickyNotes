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



    }
}