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
    }
}