import React, { useState , useEffect} from 'react';

function TodoEditCard({ todo }) 
{
    //Állapotok létrehozása szerkesztéshez
    const [title, setTitle] = useState('');
    const [competed, setCompleted] = useState(todo.isCompeted);
    
    //Akkor töltjük be a user adatait, ha azok már megjöttek
    useEffect(() => {
        if (todo) {
            setTitle(todo.title || '');
            setCompleted(todo.isCompeted || '');
        }
    }, [todo]); // csak akkor fut le, ha a `user` változik

    
    //PUT kérés küldése mentéshez
    const handleSave = () => {
        const updatedTodo = {
        ...todo,
        title,
        isCompeted: competed
        };

        fetch(`http://192.168.1.8:5000/api/todo/${todo.id}`, {
            method: 'PUT',
            headers: {
              'Content-Type': 'application/json'
            },
            body: JSON.stringify(updatedTodo)
          })
            .then(res => {
              if (res.ok) {
                alert('Feladat frissítve!');
              } else {
                alert('Hiba történt a mentés közben!');
              }
            })
            .catch(err => {
              console.error('Hiba:', err);
              alert('Szerverhiba!');
            });
    }

  return(
    <div className="bg-white rounded-2xl shadow-md p-6 mx-auto w-full max-w-md">
      <h2 className="text-xl font-semibold text-indigo-600 mb-4">✏️ Feladat Id: {todo.id}</h2>

      {/* IsCompleted mező */}
      <label className="block text-sm font-medium text-gray-700 text-left">Status:</label>
      <input
        type="checkbox"
        checked={competed}
        onChange={(e) => setCompleted(e.target.checked)}
        className="mt-1 mb-4 w-full border rounded-lg p-2 text-sm"
      />

      {/* Title mező */}
      <label className="block text-sm font-medium text-gray-700 text-left">Title:</label>
        <textarea
        value={title}
        onChange={(e) => setTitle(e.target.value)}
        rows={4} // hány sor magas legyen
        className="mt-1 mb-4 w-full border rounded-lg p-2 text-sm resize-y"
        />

          
      {/* Mentés gomb */}
      <div className="mt-6 text-right">
        <button
          onClick={handleSave}
          className="bg-indigo-500 hover:bg-indigo-600 text-white font-medium py-2 px-4 rounded-lg text-sm"
        >
          💾 Mentés
        </button>
      </div>

    </div>
  );
}

export default TodoEditCard;