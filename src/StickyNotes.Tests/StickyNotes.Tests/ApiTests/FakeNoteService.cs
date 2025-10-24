using StickyNotes.Application.Interfaces;
using StickyNotes.Domain.Entities;

namespace StickyNotes.Tests.ApiTests;
public class FakeNoteService : INoteService
{
    private readonly List<Note> _notes = new();

    public Task<IEnumerable<Note>> GetAllNotesAsync(Guid? userId = null)
    {
        IEnumerable<Note> result = userId.HasValue
            ? _notes.FindAll(n => n.UserId == userId.Value)
            : _notes;
        return Task.FromResult(result);
    }

    public Task<Note> GetNoteByIdAsync(Guid id)
    {
        var note = _notes.Find(n => n.Id == id);
        if (note == null) throw new KeyNotFoundException();
        return Task.FromResult(note);
    }

    public Task<Note> CreateNoteAsync(string title, string content, Guid userId)
    {
        if (userId == Guid.Empty) throw new ArgumentException("Invalid user ID.");
        var note = new Note(title, content, userId);
        _notes.Add(note);
        return Task.FromResult(note);
    }

    public Task<Note> UpdateNoteAsync(Guid id, string title, string content)
    {
        var note = _notes.Find(n => n.Id == id);
        if (note == null) throw new KeyNotFoundException();
        note.Update(title, content);
        return Task.FromResult(note);
    }

    public Task DeleteNoteAsync(Guid id)
    {
        var note = _notes.Find(n => n.Id == id);
        if (note != null) _notes.Remove(note);
        return Task.CompletedTask;
    }

    public Task PinNoteAsync(Guid id) { var n = _notes.Find(n => n.Id == id); n?.Pin(); return Task.CompletedTask; }
    public Task UnpinNoteAsync(Guid id) { var n = _notes.Find(n => n.Id == id); n?.Unpin(); return Task.CompletedTask; }
    public Task ArchiveNoteAsync(Guid id) { var n = _notes.Find(n => n.Id == id); n?.Archive(); return Task.CompletedTask; }
    public Task RestoreNoteAsync(Guid id) { var n = _notes.Find(n => n.Id == id); n?.Restore(); return Task.CompletedTask; }
    public Task AddTagAsync(Guid id, string tag) { var n = _notes.Find(n => n.Id == id); n?.AddTag(tag); return Task.CompletedTask; }
    public Task RemoveTagAsync(Guid id, string tag) { var n = _notes.Find(n => n.Id == id); n?.RemoveTag(tag); return Task.CompletedTask; }
    public Task ChangeColorAsync(Guid id, string color) { var n = _notes.Find(n => n.Id == id); n?.ChangeColor(color); return Task.CompletedTask; }
    public Task SetPositionAsync(Guid id, float x, float y) { var n = _notes.Find(n => n.Id == id); n?.SetPosition(x, y); return Task.CompletedTask; }
}