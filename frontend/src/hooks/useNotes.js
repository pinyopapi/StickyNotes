import { useState, useEffect } from 'react';
import { getAllNotes } from '../services/noteService';

export const useNotes = (userId) => {
  const [notes, setNotes] = useState([]);
  const [loading, setLoading] = useState(false);

  const fetchNotes = async () => {
    setLoading(true);
    try {
      const response = await getAllNotes(userId);
      const sortedNotes = response.data.sort((a, b) => b.pinned - a.pinned);
      setNotes(sortedNotes);
    } catch (err) {
      console.error(err);
      alert("Failed to load notes.");
    } finally {
      setLoading(false);
    }
  };

  useEffect(() => {
    if (userId) fetchNotes();
  }, [userId]);

  return { notes, fetchNotes, loading };
};