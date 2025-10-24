using StickyNotes.Application.Services;

namespace StickyNotes.Tests.ApplicationTests;

[TestFixture]
public class NoteServiceTests
{
    private NoteService _service;
    private FakeNoteRepository _repository;
    private Guid _userId;

    [SetUp]
    public void Setup()
    {
        _repository = new FakeNoteRepository();
        _service = new NoteService(_repository);
        _userId = Guid.NewGuid();
    }

    [Test]
    public async Task CreateNote_Should_AddNote()
    {
        var note = await _service.CreateNoteAsync("Test", "Content", _userId);

        var allNotes = await _repository.GetAllAsync(_userId);
        Assert.Multiple(() =>
        {
            Assert.That(allNotes.Count(), Is.EqualTo(1));
            Assert.That(note.Title, Is.EqualTo("Test"));
        });
    }

    [Test]
    public async Task UpdateNote_Should_ChangeTitleAndContent()
    {
        var note = await _service.CreateNoteAsync("Old", "Old", _userId);

        var updated = await _service.UpdateNoteAsync(note.Id, "New", "New");

        Assert.Multiple(() =>
        {
            Assert.That(updated.Title, Is.EqualTo("New"));
            Assert.That(updated.Content, Is.EqualTo("New"));
        });
    }

    [Test]
    public async Task PinNote_Should_SetPinnedTrue()
    {
        var note = await _service.CreateNoteAsync("A", "B", _userId);
        await _service.PinNoteAsync(note.Id);

        var result = await _service.GetNoteByIdAsync(note.Id);
        Assert.That(result.Pinned, Is.True);
    }

    [Test]
    public async Task ArchiveNote_Should_SetIsArchivedTrue()
    {
        var note = await _service.CreateNoteAsync("A", "B", _userId);
        await _service.ArchiveNoteAsync(note.Id);

        var result = await _service.GetNoteByIdAsync(note.Id);
        Assert.That(result.IsArchived, Is.True);
    }

    [Test]
    public async Task ChangeColor_Should_UpdateColor()
    {
        var note = await _service.CreateNoteAsync("A", "B", _userId);
        await _service.ChangeColorAsync(note.Id, "#FF0000");

        var result = await _service.GetNoteByIdAsync(note.Id);
        Assert.That(result.Color, Is.EqualTo("#FF0000"));
    }

    [Test]
    public async Task AddTag_Should_AddNewTag()
    {
        var note = await _service.CreateNoteAsync("A", "B", _userId);
        await _service.AddTagAsync(note.Id, "work");

        var result = await _service.GetNoteByIdAsync(note.Id);
        Assert.That(result.Tags, Does.Contain("work"));
    }

    [Test]
    public async Task DeleteNote_Should_RemoveNote()
    {
        var note = await _service.CreateNoteAsync("A", "B", _userId);
        await _service.DeleteNoteAsync(note.Id);

        var all = await _repository.GetAllAsync(_userId);
        Assert.That(all.Any(), Is.False);
    }
}