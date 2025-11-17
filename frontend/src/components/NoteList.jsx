import NoteCard from './NoteCard';
import NoteFilter from './NoteFilter';
import { useNotes } from '../hooks/useNotes';

const NoteList = ({ userId }) => {
  const { notes, fetchNotes, loading } = useNotes(userId);
  const [filteredNotes, setFilteredNotes] = useState([]);
  const [allTags, setAllTags] = useState([]);

  useEffect(() => {
    setFilteredNotes(notes);
    const tags = [...new Set(notes.flatMap(n => n.tags))];
    setAllTags(tags);
  }, [notes]);

  const handleFilter = (filterBy, value) => {
    const filtered = notes.filter(note => {
      if (filterBy === 'tag') return value.every(tag => note.tags.includes(tag));
      return note[filterBy].toLowerCase().includes(value.toLowerCase());
    });
    setFilteredNotes(filtered);
  };

  if (loading) return <p>Loading notes...</p>;

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