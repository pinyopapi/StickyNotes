import React, { useEffect, useState } from 'react';
import NoteCard from './NoteCard';
import { getAllNotes } from '../services/noteService';

const NoteList = ({ userId }) => {
    const [notes, setNotes] = useState([]);

    const fetchNotes = async () => {
        try {
            const data = await getAllNotes(userId);
            setNotes(response.data);
        } catch (error) {
            console.error('Failed to fetch notes:', error);
        }
    };

    useEffect(() => {
        fetchNotes();
    }, [userId]);

    return (
        <div className="d-flex flex-wrap">
            {notes.length === 0 ? (
                <p>No notes available</p>
            ) : (
                notes.map(note => (
                    <NoteCard key={note.id} note={note} onUpdate={fetchNotes} />
                ))
            )}
        </div>
    );
};

export default NoteList;