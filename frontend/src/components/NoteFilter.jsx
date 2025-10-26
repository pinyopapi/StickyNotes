import { useState } from 'react';

const NoteFilter = ({ allTags, onFilter }) => {
    const [filterBy, setFilterBy] = useState('title');
    const [filterText, setFilterText] = useState('');
    const [selectedTags, setSelectedTags] = useState([]);

    const toggleTag = (tag) => {
        const newTags = selectedTags.includes(tag)
            ? selectedTags.filter(t => t !== tag)
            : [...selectedTags, tag];
        setSelectedTags(newTags);
        onFilter('tag', newTags); 
    };

    const handleTextChange = (e) => {
        const text = e.target.value;
        setFilterText(text);
        onFilter(filterBy, text);
    };

    const handleFilterByChange = (e) => {
        const newFilter = e.target.value;
        setFilterBy(newFilter);
        setFilterText('');
        setSelectedTags([]);
        onFilter(newFilter, newFilter === 'tag' ? [] : ''); 
    };

    return (
        <div className="card p-3 mb-3" style={{ position: 'sticky', top: '1rem', zIndex: 100 }}>
            <h5>Filter Notes</h5>
            <select
                className="form-select mb-2"
                value={filterBy}
                onChange={handleFilterByChange}
            >
                <option value="title">Title</option>
                <option value="content">Content</option>
                <option value="tag">Tag</option>
            </select>

            {filterBy !== 'tag' ? (
                <input
                    type="text"
                    className="form-control"
                    placeholder={`Filter by ${filterBy}...`}
                    value={filterText}
                    onChange={handleTextChange}
                />
            ) : (
                <div className="d-flex flex-wrap gap-1">
                    {allTags.map(tag => (
                        <button
                            key={tag}
                            className={`btn btn-sm ${selectedTags.includes(tag) ? 'btn-primary' : 'btn-outline-secondary'}`}
                            onClick={() => toggleTag(tag)}
                        >
                            {tag}
                        </button>
                    ))}
                </div>
            )}
        </div>
    );
};

export default NoteFilter;