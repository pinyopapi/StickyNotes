using StickyNotes.Application.Interfaces;
using StickyNotes.Domain.Entities;

namespace StickyNotes.Tests.ApplicationTests;
public class FakeNoteRepository : INoteRepository
{
    private readonly List<Note> _notes = [];

    public Task AddAsync(Note note)
    {
        _notes.Add(note);
        return Task.CompletedTask;
    }

    public Task DeleteAsync(Guid id)
    {
        var note = _notes.FirstOrDefault(n => n.Id == id);
        if (note != null)
            _notes.Remove(note);
        return Task.CompletedTask;
    }

    public Task<IEnumerable<Note>> GetAllAsync(Guid? userId = null)
    {
        IEnumerable<Note> result = _notes;
        if (userId.HasValue)
            result = result.Where(n => n.UserId == userId.Value);
        return Task.FromResult(result);
    }

    public Task<Note> GetByIdAsync(Guid id)
    {
        var note = _notes.FirstOrDefault(n => n.Id == id);
        return Task.FromResult(note);
    }

    public Task UpdateAsync(Note note)
    {
        var existing = _notes.FirstOrDefault(n => n.Id == note.Id);
        if (existing != null)
        {
            _notes.Remove(existing);
            _notes.Add(note);
        }
        return Task.CompletedTask;
    }
}
