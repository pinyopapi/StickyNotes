import { useEffect, useState } from 'react';
import { getAllNotes } from '../services/noteService';
import NoteCard from './NoteCard';

const NoteList = ({ userId }) => {
    const [notes, setNotes] = useState([]);

    useEffect(() => {
        getAllNotes(userId).then(res => setNotes(res.data));
    }, [userId]);

    return (
        <div className="d-flex flex-wrap">
            {notes.map(note => <NoteCard key={note.id} note={note} />)}
        </div>
    );
};

export default NoteList;