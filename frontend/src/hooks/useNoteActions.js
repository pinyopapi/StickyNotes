import {
  pinNote,
  unpinNote,
  archiveNote,
  restoreNote,
  changeColor as apiChangeColor,
  addTag as apiAddTag,
  removeTag as apiRemoveTag,
  deleteNote as apiDeleteNote
} from '../services/noteService';

export const useNoteActions = (onUpdate) => {
  const safe = async (fn, message) => {
    try {
      await fn();
      onUpdate();
    } catch (err) {
      console.error(err);
      alert(message);
    }
  };

  return {
    changeColor: (id, color) =>
      safe(() => apiChangeColor(id, color), "Failed to change note color."),

    addTag: (id, tag) =>
      safe(() => apiAddTag(id, tag), "Failed to add tag."),

    removeTag: (id, tag) =>
      safe(() => apiRemoveTag(id, tag), "Failed to remove tag."),

    togglePin: (note) =>
      safe(() => (note.pinned ? unpinNote(note.id) : pinNote(note.id)), "Failed to update pin state."),

    toggleArchive: (note) =>
      safe(() => (note.isArchived ? restoreNote(note.id) : archiveNote(note.id)), "Failed to update archive state."),

    deleteNoteById: (id) =>
      safe(() => apiDeleteNote(id), "Failed to delete note.")
  };
};