import { useState } from 'react';
import { createNote } from '../services/noteService';

const NoteForm = ({ userId, onCreated }) => {
    const [title, setTitle] = useState('');
    const [content, setContent] = useState('');

    const handleSubmit = async (e) => {
        e.preventDefault();
        if (!title.trim() && !content.trim()) return;
        const res = await createNote(title, content, userId);
        onCreated(res.data);
        setTitle('');
        setContent('');
    };

    return (
        <div className="card mb-4 shadow-sm" style={{ maxWidth: '600px' }}>
            <div className="card-body d-flex flex-column gap-3">
                <h5 className="card-title">New Note</h5>
                <input
                    type="text"
                    className="form-control form-control-lg"
                    value={title}
                    onChange={e => setTitle(e.target.value)}
                    placeholder="Title"
                />
                <textarea
                    className="form-control"
                    value={content}
                    onChange={e => setContent(e.target.value)}
                    placeholder="Write your note here..."
                    rows={3}
                />
                <div className="d-flex justify-content-end">
                    <button className="btn btn-success" type="submit" onClick={handleSubmit}>
                        Add Note
                    </button>
                </div>
            </div>
        </div>
    );
};

export default NoteForm;