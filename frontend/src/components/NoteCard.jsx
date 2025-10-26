import { useState } from 'react';
import {
    pinNote, unpinNote, archiveNote, restoreNote,
    changeColor, addTag, removeTag, deleteNote
} from '../services/noteService';

const NoteCard = ({ note, onUpdate }) => {
    const [color, setColor] = useState(note.color);
    const [newTag, setNewTag] = useState('');
    const [openMenu, setOpenMenu] = useState(false);

    const handlePinToggle = async () => {
        if (note.pinned) await unpinNote(note.id);
        else await pinNote(note.id);
        onUpdate();
    };

    const handleArchiveToggle = async () => {
        if (note.isArchived) await restoreNote(note.id);
        else await archiveNote(note.id);
        onUpdate();
    };

    const handleDelete = async () => {
        if (window.confirm('Are you sure you want to delete this note?')) {
            try {
                await deleteNote(note.id);
                onUpdate();
            } catch (err) {
                console.error(err);
            }
        }
    };

    const handleColorChange = async (e) => {
        const newColor = e.target.value;
        setColor(newColor);
        try {
            await changeColor(note.id, newColor);
        } catch (err) {
            console.error(err);
        }
    };

    const handleAddTag = async () => {
        if (!newTag.trim()) return;
        await addTag(note.id, newTag);
        setNewTag('');
        onUpdate();
    };

    const handleRemoveTag = async (tag) => {
        await removeTag(note.id, tag);
        onUpdate();
    };

    return (
        <div
            className={`card m-2 p-3 ${note.isArchived ? 'bg-light text-muted' : ''}`}
            style={{ backgroundColor: color, width: '250px', display: 'flex', flexDirection: 'column', gap: '0.5rem' }}
        >
            <h5 className="card-title d-flex justify-content-between align-items-center">
                {note.title}
                <span>
                    {note.pinned && '📌 '}
                    {note.isArchived && '🗄️'}
                </span>
            </h5>
            <p className="card-text">{note.content}</p>
            <div className="mt-2">
                {note.tags.map(tag => (
                    <span key={tag} className="badge bg-secondary me-1 mb-1">
                        {tag}
                        <button className="btn-close btn-close-white btn-sm ms-1" onClick={() => handleRemoveTag(tag)}></button>
                    </span>
                ))}
            </div>
            {openMenu && (
                <div className="mt-2 pt-2 border-top d-flex flex-column gap-2">
                    <div>
                        <span className="me-1">Color</span>
                        <input type="color" value={color} onChange={handleColorChange} className="form-control form-control-color mb-1" />
                    </div>
                    <button className="btn btn-sm btn-outline-primary" onClick={handlePinToggle}>
                        {note.pinned ? 'Unpin' : 'Pin'}
                    </button>
                    <button className="btn btn-sm btn-outline-secondary" onClick={handleArchiveToggle}>
                        {note.isArchived ? 'Restore' : 'Archive'}
                    </button>
                    <button className="btn btn-sm btn-outline-danger" onClick={handleDelete}>
                        Delete
                    </button>
                    <div className="d-flex gap-1">
                        <input
                            type="text"
                            value={newTag}
                            onChange={e => setNewTag(e.target.value)}
                            placeholder="Add tag"
                            className="form-control form-control-sm"
                        />
                        <button className="btn btn-sm btn-primary" onClick={handleAddTag}>Add</button>
                    </div>
                </div>
            )}
            <button className="btn btn-sm btn-secondary mt-auto" onClick={() => setOpenMenu(!openMenu)}>Settings</button>

        </div>
    );
};

export default NoteCard;