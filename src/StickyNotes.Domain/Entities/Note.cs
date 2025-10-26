using System;
using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;
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

        [ExcludeFromCodeCoverage]
        private Note() { }

        public void Update(string title, string content)
        {
            SetTitle(title);
            SetContent(content);
            UpdatedAt = DateTime.UtcNow;
        }

        public void ChangeColor(string color)
        {
            if (string.IsNullOrWhiteSpace(color))
                throw new ArgumentException("Color cannot be empty");
            Color = color;
            UpdatedAt = DateTime.UtcNow;
        }

        public void Pin() => Pinned = true;
        public void Unpin() => Pinned = false;

        public void Archive()
        {
            IsArchived = true;
            Pinned = false;
            UpdatedAt = DateTime.UtcNow;
        }

        public void Restore()
        {
            IsArchived = false;
            UpdatedAt = DateTime.UtcNow;
        }

        public void AddTag(string tag)
        {
            if (!string.IsNullOrWhiteSpace(tag) && !Tags.Contains(tag))
            {
                Tags.Add(tag);
                UpdatedAt = DateTime.UtcNow;
            }
        }

        public void RemoveTag(string tag)
        {
            if (Tags.Contains(tag))
            {
                Tags.Remove(tag);
                UpdatedAt = DateTime.UtcNow;
            }
        }

        public void SetPosition(float x, float y)
        {
            PositionX = x;
            PositionY = y;
            UpdatedAt = DateTime.UtcNow;
        }

        private void SetTitle(string title)
        {
            if (string.IsNullOrWhiteSpace(title))
                throw new ArgumentException("Title cannot be empty");
            Title = title;
        }

        private void SetContent(string content)
        {
            Content = content ?? string.Empty;
        }
    }
}