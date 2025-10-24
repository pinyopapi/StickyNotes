using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using StickyNotes.Domain.Entities;
using StickyNotes.Application.Interfaces;

namespace StickyNotes.Application.Services
{
    public class NoteService : INoteService
    {
        private readonly INoteRepository _repository;

        public NoteService(INoteRepository repository)
        {
            _repository = repository ?? throw new ArgumentNullException(nameof(repository));
        }

        public async Task<IEnumerable<Note>> GetAllNotesAsync(Guid? userId = null)
        {
            return await _repository.GetAllAsync(userId);
        }

        public async Task<Note> GetNoteByIdAsync(Guid id)
        {
            var note = await _repository.GetByIdAsync(id)
                       ?? throw new KeyNotFoundException($"Note with id {id} not found.");
            return note;
        }

        public async Task<Note> CreateNoteAsync(string title, string content, Guid userId)
        {
            if (userId == Guid.Empty)
                throw new ArgumentException("Invalid user ID.");

            var note = new Note(title, content, userId);
            await _repository.AddAsync(note);
            return note;
        }

        public async Task<Note> UpdateNoteAsync(Guid id, string title, string content)
        {
            var note = await GetNoteByIdAsync(id);
            note.Update(title, content);
            await _repository.UpdateAsync(note);
            return note;
        }

        public async Task DeleteNoteAsync(Guid id)
        {
            var note = await GetNoteByIdAsync(id);
            await _repository.DeleteAsync(note.Id);
        }

        public async Task PinNoteAsync(Guid id) => await ApplyDomainAction(id, n => n.Pin());
        public async Task UnpinNoteAsync(Guid id) => await ApplyDomainAction(id, n => n.Unpin());
        public async Task ArchiveNoteAsync(Guid id) => await ApplyDomainAction(id, n => n.Archive());
        public async Task RestoreNoteAsync(Guid id) => await ApplyDomainAction(id, n => n.Restore());
        public async Task AddTagAsync(Guid id, string tag) => await ApplyDomainAction(id, n => n.AddTag(tag));
        public async Task RemoveTagAsync(Guid id, string tag) => await ApplyDomainAction(id, n => n.RemoveTag(tag));
        public async Task ChangeColorAsync(Guid id, string color) => await ApplyDomainAction(id, n => n.ChangeColor(color));
        public async Task SetPositionAsync(Guid id, float x, float y) => await ApplyDomainAction(id, n => n.SetPosition(x, y));

    }
}