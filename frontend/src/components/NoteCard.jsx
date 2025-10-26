const NoteCard = ({ note }) => {
    return (
        <div className="card m-2" style={{ backgroundColor: note.color }}>
            <div className="card-body">
                <h5 className="card-title">{note.title}</h5>
                <p className="card-text">{note.content}</p>
            </div>
        </div>
    );
};

export default NoteCard;