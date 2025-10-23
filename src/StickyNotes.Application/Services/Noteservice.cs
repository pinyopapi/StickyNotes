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
            _repository = repository;
        }

        public async Task<IEnumerable<Note>> GetAllNotesAsync(Guid? userId = null)
        {
            return await _repository.GetAllAsync(userId);
        }

        public async Task<Note> GetNoteByIdAsync(Guid id)
        {
            var note = await _repository.GetByIdAsync(id);
            if (note == null) throw new KeyNotFoundException("Note not found");
            return note;
        }

        public async Task<Note> CreateNoteAsync(string title, string content, Guid userId)
        {
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
            await _repository.DeleteAsync(id);
        }

        public async Task PinNoteAsync(Guid id)
        {
            var note = await GetNoteByIdAsync(id);
            note.Pin();
            await _repository.UpdateAsync(note);
        }

        public async Task UnpinNoteAsync(Guid id)
        {
            var note = await GetNoteByIdAsync(id);
            note.Unpin();
            await _repository.UpdateAsync(note);
        }

        public async Task ArchiveNoteAsync(Guid id)
        {
            var note = await GetNoteByIdAsync(id);
            note.Archive();
            await _repository.UpdateAsync(note);
        }

        public async Task RestoreNoteAsync(Guid id)
        {
            var note = await GetNoteByIdAsync(id);
            note.Restore();
            await _repository.UpdateAsync(note);
        }

        public async Task AddTagAsync(Guid id, string tag)
        {
            var note = await GetNoteByIdAsync(id);
            note.AddTag(tag);
            await _repository.UpdateAsync(note);
        }

        public async Task RemoveTagAsync(Guid id, string tag)
        {
            var note = await GetNoteByIdAsync(id);
            note.RemoveTag(tag);
            await _repository.UpdateAsync(note);
        }

        public async Task ChangeColorAsync(Guid id, string color)
        {
            var note = await GetNoteByIdAsync(id);
            note.ChangeColor(color);
            await _repository.UpdateAsync(note);
        }

        public async Task SetPositionAsync(Guid id, float x, float y)
        {
            var note = await GetNoteByIdAsync(id);
            note.SetPosition(x, y);
            await _repository.UpdateAsync(note);
        }
    }
}