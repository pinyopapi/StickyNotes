import { createNote as apiCreateNote } from '../services/noteService';

export const useNoteFormActions = (onCreated) => {
  const safeCreateNote = async (title, content, userId) => {
    try {
      const res = await apiCreateNote(title, content, userId);
      onCreated(res.data);
    } catch (err) {
      console.error(err);
      alert("Failed to create note.");
    }
  };

  return { safeCreateNote };
};