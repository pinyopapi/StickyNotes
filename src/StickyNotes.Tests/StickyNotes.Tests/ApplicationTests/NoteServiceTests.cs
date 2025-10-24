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
        Assert.That(allNotes.Count(), Is.EqualTo(1));
        Assert.That(note.Title, Is.EqualTo("Test"));
    }

}
