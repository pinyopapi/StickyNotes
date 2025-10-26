import React, { useEffect, useState } from 'react';
import NoteCard from './NoteCard';
import { getAllNotes } from '../services/noteService';

const NoteList = ({ userId }) => {
    const [notes, setNotes] = useState([]);
    const [filterBy, setFilterBy] = useState('title');
    const [filterText, setFilterText] = useState('');
    const [selectedTags, setSelectedTags] = useState([]);

    const fetchNotes = async () => {
        try {
            const response = await getAllNotes(userId);
            setNotes(response.data);
        } catch (error) {
            console.error('Failed to fetch notes:', error);
        }
    };

    useEffect(() => {
        fetchNotes();
    }, [userId]);

    const allTags = Array.from(new Set(notes.flatMap(n => n.tags)));

    const toggleTag = (tag) => {
        setSelectedTags(prev => 
            prev.includes(tag) ? prev.filter(t => t !== tag) : [...prev, tag]
        );
    };

    const filteredNotes = notes.filter(note => {
        if (filterBy === 'title') return note.title.toLowerCase().includes(filterText.toLowerCase());
        if (filterBy === 'content') return note.content.toLowerCase().includes(filterText.toLowerCase());
        if (filterBy === 'tag') {
            if (selectedTags.length === 0) return true;
            return note.tags.some(tag => selectedTags.includes(tag));
        }
        return true;
    });

    return (
        <div>
            <div className="d-flex mb-3 gap-2">
                {filterBy !== 'tag' ? (
                    <input
                        type="text"
                        value={filterText}
                        onChange={e => setFilterText(e.target.value)}
                        placeholder={`Filter by ${filterBy}...`}
                        className="form-control"
                    />
                ) : (
                    <div className="d-flex flex-wrap gap-1">
                        {allTags.map(tag => (
                            <button
                                key={tag}
                                className={`btn btn-sm ${selectedTags.includes(tag) ? 'btn-primary' : 'btn-outline-secondary'}`}
                                onClick={() => toggleTag(tag)}
                            >
                                {tag}
                            </button>
                        ))}
                    </div>
                )}

                <select
                    value={filterBy}
                    onChange={e => {
                        setFilterBy(e.target.value);
                        setFilterText('');
                        setSelectedTags([]);
                    }}
                    className="form-select"
                >
                    <option value="title">Title</option>
                    <option value="content">Content</option>
                    <option value="tag">Tag</option>
                </select>
            </div>

            <div className="d-flex flex-wrap">
                {filteredNotes.length === 0 ? (
                    <p>No notes found</p>
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