using System;
using System.Collections.Generic;
using System.Linq;

namespace StickyNotes.Domain.Entities
{
    public class Note
    {
        public Guid Id { get; private set; } = Guid.NewGuid();
        public string Title { get; private set; }
        public string Content { get; private set; }
        public string Color { get; private set; } = "#FFFFFF";
        public bool Pinned { get; private set; } = false;
        public bool IsArchived { get; private set; } = false;
        public List<string> Tags { get; private set; } = new List<string>();
        public float PositionX { get; private set; } = 0;
        public float PositionY { get; private set; } = 0;
        public Guid UserId { get; private set; }
        public DateTime CreatedAt { get; private set; } = DateTime.UtcNow;
        public DateTime? UpdatedAt { get; private set; } = null;

        public Note(string title, string content, Guid userId)
        {
            SetTitle(title);
            SetContent(content);
            UserId = userId;
        }

    }
}
