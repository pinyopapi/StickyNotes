import { useState } from 'react';

const NoteFilter = ({ allTags, onFilter }) => {
  const [searchText, setSearchText] = useState('');
  const [selectedTags, setSelectedTags] = useState([]);

  const handleSearch = (e) => {
    const value = e.target.value;
    setSearchText(value);
    onFilter('title', value);
  };

  const toggleTag = (tag) => {
    let updated;

    if (selectedTags.includes(tag)) {
      updated = selectedTags.filter(t => t !== tag);
    } else {
      updated = [...selectedTags, tag];
    }

    setSelectedTags(updated);
    onFilter('tag', updated);
  };

  return (
    <div className="d-flex flex-column gap-3">

      <div>
        <label className="form-label fw-bold">Search</label>
        <input
          className="form-control"
          type="text"
          placeholder="Search by title..."
          value={searchText}
          onChange={handleSearch}
        />
      </div>

      <div>
        <label className="form-label fw-bold">Filter by Tags</label>

        <div className="d-flex flex-wrap gap-2">
          {allTags.length === 0 && (
            <span className="text-muted">No tags available</span>
          )}

          {allTags.map(tag => (
            <button
              key={tag}
              type="button"
              className={`btn btn-sm ${
                selectedTags.includes(tag) ? 'btn-primary' : 'btn-outline-primary'
              }`}
              onClick={() => toggleTag(tag)}
            >
              {tag}
            </button>
          ))}
        </div>
      </div>
    </div>
  );
};

export default NoteFilter;