import { useState } from 'react';
import 'bootstrap/dist/css/bootstrap.min.css';
import NoteList from './components/NoteList';
import NoteFormWrapper from './components/NoteFormWrapper';

function App() {
  const userId = "00000000-0000-0000-0000-000000000001";
  const [refreshKey, setRefreshKey] = useState(0);

  const handleNoteCreated = () => {
    setRefreshKey(prev => prev + 1);
  };

  return (
    <div className="container mt-1" style={{
      background: 'linear-gradient(to bottom right, #ecebaaff, #fffd86ff)'
    }}>
      <NoteFormWrapper userId={userId} onCreated={handleNoteCreated} />
      <NoteList key={refreshKey} userId={userId} />
    </div>
  );
}

export default App;