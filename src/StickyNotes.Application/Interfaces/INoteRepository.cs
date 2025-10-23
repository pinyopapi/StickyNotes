using StickyNotes.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace StickyNotes.Application.Interfaces
{
    public interface INoteRepository
    {
        Task<IEnumerable<Note>> GetAllNotesAsync(Guid? userId = null);
        Task<Note> GetNoteByIdAsync(Guid id);
        Task<Note> CreateNoteAsync(string title, string content, Guid userId);
        Task<Note> UpdateNoteAsync(Guid id, string title, string content);
        Task DeleteNoteAsync(Guid id);
        Task PinNoteAsync(Guid id);
        Task UnpinNoteAsync(Guid id);
        Task ArchiveNoteAsync(Guid id);
        Task RestoreNoteAsync(Guid id);
        Task AddTagAsync(Guid id, string tag);
        Task RemoveTagAsync(Guid id, string tag);
        Task ChangeColorAsync(Guid id, string color);
        Task SetPositionAsync(Guid id, float x, float y);
    }
}