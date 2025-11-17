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
      safe(() => changeColor(id, color), "Failed to change note color."),

    addTag: (id, tag) =>
      safe(() => addTag(id, tag), "Failed to add tag."),

    removeTag: (id, tag) =>
      safe(() => removeTag(id, tag), "Failed to remove tag."),

    togglePin: (note) =>
      safe(() => (note.pinned ? unpinNote(note.id) : pinNote(note.id)), "Failed to update pin state."),

    toggleArchive: (note) =>
      safe(() => (note.isArchived ? restoreNote(note.id) : archiveNote(note.id)), "Failed to update archive state."),

    deleteNoteById: (id) =>
      safe(() => deleteNote(id), "Failed to delete note.")
  };
};