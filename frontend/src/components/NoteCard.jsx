import { useState } from 'react';
import Draggable from 'react-draggable';
import { pinNote, unpinNote, archiveNote, restoreNote, changeColor, addTag, removeTag, setPosition } from '../services/noteService';

const NoteCard = ({ note, onUpdate }) => {
    const [color, setColor] = useState(note.color);
    const [newTag, setNewTag] = useState('');

    const handlePinToggle = async () => {
        if (note.pinned) {
            await unpinNote(note.id);
        } else {
            await pinNote(note.id);
        }
        onUpdate();
    };

    const handleArchiveToggle = async () => {
        if (note.isArchived) {
            await restoreNote(note.id);
        } else {
            await archiveNote(note.id);
        }
        onUpdate();
    };

    const handleColorChange = async (e) => {
        const newColor = e.target.value;
        setColor(newColor);
        await changeColor(note.id, newColor);
        onUpdate();
    };

    const handleAddTag = async () => {
        if (newTag.trim() === '') return;
        await addTag(note.id, newTag);
        setNewTag('');
        onUpdate();
    };

    const handleRemoveTag = async (tag) => {
        await removeTag(note.id, tag);
        onUpdate();
    };

    const handleStop = async (e, data) => {
        await setPosition(note.id, data.x, data.y);
        onUpdate();
    };

    return (
        <Draggable
            defaultPosition={{ x: note.positionX, y: note.positionY }}
            onStop={handleStop}
        >
            <div className={`card m-2 ${note.isArchived ? 'bg-light text-muted' : ''}`} style={{ backgroundColor: color, width: '250px', cursor: 'move' }}>
                <div className="card-body">
                    <h5 className="card-title">
                        {note.title} 
                        {note.pinned && <span className="ms-2">📌</span>}
                        {note.isArchived && <span className="ms-2">🗄️</span>}
                    </h5>
                    <p className="card-text">{note.content}</p>

                    <input type="color" value={color} onChange={handleColorChange} className="me-2" />

                    <button className="btn btn-sm btn-outline-primary me-2" onClick={handlePinToggle}>
                        {note.pinned ? 'Unpin' : 'Pin'}
                    </button>
                    <button className="btn btn-sm btn-outline-secondary me-2" onClick={handleArchiveToggle}>
                        {note.isArchived ? 'Restore' : 'Archive'}
                    </button>

                    <div className="mb-2 mt-2">
                        {note.tags.map(tag => (
                            <span key={tag} className="badge bg-secondary me-1">
                                {tag} 
                                <button className="btn-close btn-close-white btn-sm ms-1" onClick={() => handleRemoveTag(tag)}></button>
                            </span>
                        ))}
                    </div>
                    <input type="text" value={newTag} onChange={e => setNewTag(e.target.value)} placeholder="Add tag" />
                    <button className="btn btn-sm btn-primary ms-1" onClick={handleAddTag}>Add</button>
                </div>
            </div>
        </Draggable>
    );
};

export default NoteCard;