import { useState, useEffect } from "react";
import NoteCard from "./NoteCard";
import NoteFilter from "./NoteFilter";
import { useNotes } from "../hooks/useNotes";

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
      if (filterBy === "tag") return value.every(tag => note.tags.includes(tag));
      return note[filterBy].toLowerCase().includes(value.toLowerCase());
    });
    setFilteredNotes(filtered);
  };

  if (loading) return <p className="text-center mt-4">Loading notes...</p>;

  return (
    <div className="container-fluid mt-4">
      <div className="row">

        <div className="col-12 col-md-3 col-lg-2 border-end mb-4 mb-md-0">
          <div className="p-3">
            <h5 className="mb-3">Filters</h5>
            <NoteFilter allTags={allTags} onFilter={handleFilter} />
          </div>
        </div>

        <div className="col-12 col-md-9 col-lg-10">
          <div className="row g-4 p-3">
            {filteredNotes.length === 0 ? (
              <p>No notes available</p>
            ) : (
              filteredNotes.map(note => (
                <div key={note.id} className="col-12 col-sm-6 col-md-4 col-lg-3">
                  <NoteCard note={note} onUpdate={fetchNotes} />
                </div>
              ))
            )}
          </div>
        </div>
      </div>
    </div>
  );
};

export default NoteList;