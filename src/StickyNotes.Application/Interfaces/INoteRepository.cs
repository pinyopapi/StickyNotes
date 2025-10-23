using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using StickyNotes.Domain.Entities;

namespace StickyNotes.Application.Interfaces
{
    public interface INoteRepository
    {
        Task<IEnumerable<Note>> GetAllAsync(Guid? userId = null);
        Task<Note> GetByIdAsync(Guid id);
        Task AddAsync(Note note);
        Task UpdateAsync(Note note);
        Task DeleteAsync(Guid id);
    }
}