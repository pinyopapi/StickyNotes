import React, { useEffect, useState } from 'react';
import NoteCard from './NoteCard';
import NoteFilter from './NoteFilter';
import { getAllNotes } from '../services/noteService';

const NoteList = ({ userId }) => {
    const [notes, setNotes] = useState([]);
    const [filteredNotes, setFilteredNotes] = useState([]);
    const [allTags, setAllTags] = useState([]);

    const fetchNotes = async () => {
        try {
            const response = await getAllNotes(userId);
            const sortedNotes = response.data.sort((a, b) => b.pinned - a.pinned);
            setNotes(sortedNotes);
            setFilteredNotes(sortedNotes);
            const tags = [...new Set(response.data.flatMap(n => n.tags))];
            setAllTags(tags);
        } catch (error) {
            console.error('Failed to fetch notes:', error);
        }
    };


    useEffect(() => {
        fetchNotes();
    }, [userId]);

    const handleFilter = (filterBy, value) => {
        const filtered = notes.filter(note => {
            if (filterBy === 'tag') {
                return value.every(tag => note.tags.includes(tag));
            } else {
                return note[filterBy].toLowerCase().includes(value.toLowerCase());
            }
        });
        setFilteredNotes(filtered);
    };

    return (
        <div className="d-flex">
            <div className="p-3 border-end" style={{ width: '200px' }}>
                <NoteFilter allTags={allTags} onFilter={handleFilter} />
            </div>

            <div className="d-flex flex-wrap p-3" style={{ gap: '1rem', flexGrow: 1 }}>
                {filteredNotes.length === 0 ? (
                    <p>No notes available</p>
                ) : (
                    filteredNotes.map(note => (
                        <NoteCard key={note.id} note={note} onUpdate={fetchNotes} />
                    ))
                )}
            </div>
        </div>
    );
};

export default NoteList;