import { pinNote, unpinNote, archiveNote, restoreNote } from '../services/noteService';

const NoteCard = ({ note, onUpdate }) => {

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

    return (
        <div className={`card m-2 ${note.isArchived ? 'bg-light text-muted' : ''}`} style={{ backgroundColor: note.color }}>
            <div className="card-body">
                <h5 className="card-title">
                    {note.title} 
                    {note.pinned && <span className="ms-2">📌</span>}
                    {note.isArchived && <span className="ms-2">🗄️</span>}
                </h5>
                <p className="card-text">{note.content}</p>
                <button className="btn btn-sm btn-outline-primary me-2" onClick={handlePinToggle}>
                    {note.pinned ? 'Unpin' : 'Pin'}
                </button>
                <button className="btn btn-sm btn-outline-secondary" onClick={handleArchiveToggle}>
                    {note.isArchived ? 'Restore' : 'Archive'}
                </button>
            </div>
        </div>
    );
};

export default NoteCard;