import { useState } from 'react';
import 'bootstrap/dist/css/bootstrap.min.css';
import NoteForm from './components/NoteForm';
import NoteList from './components/NoteList';

function App() {
  const userId = "00000000-0000-0000-0000-000000000001"; 
  const [refreshKey, setRefreshKey] = useState(0); 

  const handleNoteCreated = () => {
    setRefreshKey(prev => prev + 1);
  };

  return (
    <div className="container mt-4">
      <h1>Sticky Notes</h1>
      <NoteForm userId={userId} onCreated={handleNoteCreated} />
      <NoteList key={refreshKey} userId={userId} />
    </div>
  );
}

export default App;