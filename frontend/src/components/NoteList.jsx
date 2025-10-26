import { useEffect, useState } from 'react';
import { getAllNotes } from '../services/noteService';
import NoteCard from './NoteCard';

const NoteList = ({ userId }) => {
    const [notes, setNotes] = useState([]);

    const fetchNotes = () => {
        getAllNotes(userId).then(res => setNotes(res.data));
    };

    useEffect(() => {
        fetchNotes();
    }, [userId]);

    return (
        <div className="d-flex flex-wrap">
            {notes.map(note => (
                <NoteCard key={note.id} note={note} onUpdate={fetchNotes} />
            ))}
        </div>
    );
};

export default NoteList;