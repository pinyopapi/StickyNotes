using Microsoft.AspNetCore.Mvc;
using StickyNotes.Api.Controllers;
using StickyNotes.Application.Services;
using StickyNotes.Domain.Entities;

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

        [Test]
        public async Task ChangeColor_ShouldUpdateNoteColor()
        {
            var note = await _service.CreateNoteAsync("Colored", "Note", _userId);
            var request = new ChangeColorRequest("#FF5733");

            await _controller.ChangeColor(note.Id, request);

            var updated = await _service.GetNoteByIdAsync(note.Id);
            Assert.That(updated.Color, Is.EqualTo("#FF5733"));
        }

        [Test]
        public async Task SetPosition_ShouldUpdateCoordinates()
        {
            var note = await _service.CreateNoteAsync("MoveMe", "Note", _userId);
            var request = new PositionRequest(42.5f, 88.1f);

            await _controller.SetPosition(note.Id, request);

            var updated = await _service.GetNoteByIdAsync(note.Id);
            Assert.Multiple(() =>
            {
                Assert.That(updated.PositionX, Is.EqualTo(42.5f));
                Assert.That(updated.PositionY, Is.EqualTo(88.1f));
            });
        }

        [Test]
        public async Task AddTag_ShouldAddTagToNote()
        {
            var note = await _service.CreateNoteAsync("Tagged", "Note", _userId);
            var request = new TagRequest("important");

            await _controller.AddTag(note.Id, request);

            var updated = await _service.GetNoteByIdAsync(note.Id);
            Assert.That(updated.Tags, Does.Contain("important"));
        }

        [Test]
        public void AddTag_ShouldNotAddTag_WhenTagIsEmptyOrWhitespace()
        {
            var note = new Note("Title", "Content", Guid.NewGuid());

            note.AddTag("");
            note.AddTag("   ");

            Assert.That(note.Tags, Is.Empty);
        }

        [Test]
        public void AddTag_ShouldNotAddDuplicateTag()
        {
            var note = new Note("Title", "Content", Guid.NewGuid());
            note.AddTag("Tag1");

            note.AddTag("Tag1");

            Assert.That(note.Tags, Has.Count.EqualTo(1));
        }


        [Test]
        public async Task RemoveTag_ShouldRemoveTagFromNote()
        {
            var note = await _service.CreateNoteAsync("Tagged", "Note", _userId);
            note.AddTag("todo");
            var request = new TagRequest("todo");

            await _controller.RemoveTag(note.Id, request);

            var updated = await _service.GetNoteByIdAsync(note.Id);
            Assert.That(updated.Tags, Does.Not.Contain("todo"));
        }

        [Test]
        public async Task GetById_ShouldReturnNote_WhenExists()
        {
            var note = await _service.CreateNoteAsync("Title", "Content", _userId);

            var result = await _controller.GetById(note.Id) as OkObjectResult;

            Assert.That(result, Is.Not.Null);
            var returnedNote = result.Value as Note;
            Assert.That(returnedNote?.Id, Is.EqualTo(note.Id));
        }

        [Test]
        public async Task GetById_ShouldReturnNotFound_WhenNoteDoesNotExist()
        {
            var invalidId = Guid.NewGuid();

            var result = await _controller.GetById(invalidId) as NotFoundObjectResult;

            Assert.That(result, Is.Not.Null);
            Assert.That(result.StatusCode, Is.EqualTo(404));
        }

        [Test]
        public async Task GetById_ShouldReturnBadRequest_WhenIdIsEmpty()
        {
            var emptyId = Guid.Empty;

            var result = await _controller.GetById(emptyId);

            Assert.That(result, Is.TypeOf<BadRequestObjectResult>());
        }

        [Test]
        public async Task Create_ShouldReturnBadRequest_WhenModelStateIsInvalid()
        {
            _controller.ModelState.AddModelError("Title", "Required");
            var request = new CreateNoteRequest("", "Content", _userId);

            var result = await _controller.Create(request);

            Assert.That(result, Is.TypeOf<BadRequestObjectResult>());
        }

        [Test]
        public void Constructor_ShouldThrowArgumentNullException_WhenRepositoryIsNull()
        {
            var ex = Assert.Throws<ArgumentNullException>(() => new NoteService(null));
            Assert.That(ex.ParamName, Is.EqualTo("repository"));
        }


    }
}