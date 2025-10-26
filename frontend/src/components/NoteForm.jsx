import { useState } from 'react';
import { createNote } from '../services/noteService';

const NoteForm = ({ userId, onCreated }) => {
    const [title, setTitle] = useState('');
    const [content, setContent] = useState('');

    const handleSubmit = async (e) => {
        e.preventDefault();
        const res = await createNote(title, content, userId);
        onCreated(res.data);
        setTitle('');
        setContent('');
    };

    return (
        <form 
            onSubmit={handleSubmit} 
            className="mb-3 p-3 rounded shadow-sm"
            style={{ 
                position: 'sticky', 
                top: 0, 
                backgroundColor: '#ffffff', 
                zIndex: 1000 
            }}
        >
            <input 
                className="form-control mb-2" 
                value={title} 
                onChange={e => setTitle(e.target.value)} 
                placeholder="Title" 
            />
            <textarea 
                className="form-control mb-2" 
                value={content} 
                onChange={e => setContent(e.target.value)} 
                placeholder="Content" 
            />
            <button className="btn btn-primary w-100" type="submit">Add Note</button>
        </form>
    );
};

export default NoteForm;