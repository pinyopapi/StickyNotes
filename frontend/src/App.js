import React, { useState } from 'react';
import NoteList from './components/NoteList';

function App() {
  const [userId] = useState('11111111-1111-1111-1111-111111111111');

  return (
    <div className="container mt-4">
      <h1>Sticky Notes</h1>
      <NoteList userId={userId} />
    </div>
  );
}

export default App;