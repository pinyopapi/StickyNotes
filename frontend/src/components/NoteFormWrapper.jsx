import { useState } from 'react';
import NoteForm from './NoteForm';

const NoteFormWrapper = ({ userId, onCreated }) => {
    const [open, setOpen] = useState(false);

    return (
        <div 
            style={{
                position: 'sticky',
                top: 0,
                zIndex: 1000,
                backgroundColor: '#ffffff',
                padding: '1rem',
                borderRadius: '8px',
                boxShadow: '0 4px 8px rgba(0,0,0,0.1)',
                marginBottom: '1rem'
            }}
        >
            <button 
                className="btn btn-primary w-100 mb-2"
                onClick={() => setOpen(!open)}
            >
                {open ? 'Close Note Form' : 'Add Note'}
            </button>
            {open && <NoteForm userId={userId} onCreated={onCreated} />}
        </div>
    );
};

export default NoteFormWrapper;