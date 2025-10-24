using Microsoft.AspNetCore.Mvc;
using StickyNotes.Application.Interfaces;
using StickyNotes.Domain.Entities;
using System;
using System.Threading.Tasks;

namespace StickyNotes.Api.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class NoteController : ControllerBase
    {
        private readonly INoteService _noteService;

        public NoteController(INoteService noteService)
        {
            _noteService = noteService;
        }

        [HttpGet]
        public async Task<IActionResult> GetAll([FromQuery] Guid? userId)
        {
            var notes = await _noteService.GetAllNotesAsync(userId);
            return Ok(notes);
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetById(Guid id)
        {
            var note = await _noteService.GetNoteByIdAsync(id);
            return Ok(note);
        }

        [HttpPost]
        public async Task<IActionResult> Create([FromBody] CreateNoteRequest request)
        {
            var note = await _noteService.CreateNoteAsync(request.Title, request.Content, request.UserId);
            return CreatedAtAction(nameof(GetById), new { id = note.Id }, note);
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> Update(Guid id, [FromBody] UpdateNoteRequest request)
        {
            var updated = await _noteService.UpdateNoteAsync(id, request.Title, request.Content);
            return Ok(updated);
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(Guid id)
        {
            await _noteService.DeleteNoteAsync(id);
            return NoContent();
        }

        [HttpPut("{id}/pin")]
        public async Task<IActionResult> Pin(Guid id)
        {
            await _noteService.PinNoteAsync(id);
            return NoContent();
        }

        [HttpPut("{id}/unpin")]
        public async Task<IActionResult> Unpin(Guid id)
        {
            await _noteService.UnpinNoteAsync(id);
            return NoContent();
        }

        [HttpPut("{id}/archive")]
        public async Task<IActionResult> Archive(Guid id)
        {
            await _noteService.ArchiveNoteAsync(id);
            return NoContent();
        }

        [HttpPut("{id}/restore")]
        public async Task<IActionResult> Restore(Guid id)
        {
            await _noteService.RestoreNoteAsync(id);
            return NoContent();
        }

        [HttpPut("{id}/color")]
        public async Task<IActionResult> ChangeColor(Guid id, [FromBody] ChangeColorRequest request)
        {
            await _noteService.ChangeColorAsync(id, request.Color);
            return NoContent();
        }

        [HttpPut("{id}/position")]
        public async Task<IActionResult> SetPosition(Guid id, [FromBody] PositionRequest request)
        {
            await _noteService.SetPositionAsync(id, request.X, request.Y);
            return NoContent();
        }

        [HttpPut("{id}/tags/add")]
        public async Task<IActionResult> AddTag(Guid id, [FromBody] TagRequest request)
        {
            await _noteService.AddTagAsync(id, request.Tag);
            return NoContent();
        }

        [HttpPut("{id}/tags/remove")]
        public async Task<IActionResult> RemoveTag(Guid id, [FromBody] TagRequest request)
        {
            await _noteService.RemoveTagAsync(id, request.Tag);
            return NoContent();
        }
    }

    public record CreateNoteRequest(string Title, string Content, Guid UserId);
    public record UpdateNoteRequest(string Title, string Content);
    public record ChangeColorRequest(string Color);
    public record PositionRequest(float X, float Y);
    public record TagRequest(string Tag);
}