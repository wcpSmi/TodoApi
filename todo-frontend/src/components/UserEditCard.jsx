import React, { useState , useEffect} from 'react';
import { useNavigate } from 'react-router-dom';

function UserEditCard({ user }) {
  const navigate = useNavigate();

  //Állapotok létrehozása szerkesztéshez
  const [name, setName] = useState('');
  const [email, setEmail] = useState('');

    //Akkor töltjük be a user adatait, ha azok már megjöttek
    useEffect(() => {
        if (user) {
          setName(user.name || '');
          setEmail(user.email || '');
        }
      }, [user]); // csak akkor fut le, ha a `user` változik

  //PUT kérés küldése mentéshez
  const handleSave = () => {
    const updatedUser = {
      ...user,
      name,
      email
    };



    fetch(`http://192.168.1.8:5000/api/user/${user.id}`, {
      method: 'PUT',
      headers: {
        'Content-Type': 'application/json'
      },
      body: JSON.stringify(updatedUser)
    })
      .then(res => {
        if (res.ok) {
          alert('Felhasználó frissítve!');
          navigate('/'); // vissza a főoldalra
        } else {
          alert('Hiba történt a mentés közben!');
        }
      })
      .catch(err => {
        console.error('Hiba:', err);
        alert('Szerverhiba!');
      });
  };

  return (
    <div className="bg-white rounded-2xl shadow-md p-6 mx-auto w-full max-w-md">
      <h2 className="text-xl font-semibold text-indigo-600 mb-4">✏️ </h2>

      {/* Név mező */}
      <label className="block text-sm font-medium text-gray-700 text-left">Név:</label>
      <input
        type="text"
        value={name}
        onChange={(e) => setName(e.target.value)}
        className="mt-1 mb-4 w-full border rounded-lg p-2 text-sm"
      />

      {/* Email mező */}
      <label className="block text-sm font-medium text-gray-700 text-left">Email:</label>
      <input
        type="email"
        value={email}
        onChange={(e) => setEmail(e.target.value)}
        className="mt-1 mb-4 w-full border rounded-lg p-2 text-sm"
      />

      
      {/* Todo lista csak olvasható formában */}
      {user.todos?.length > 0 ? (
        <ul className="mt-3 list-disc list-inside text-gray-700 text-sm text-left">
          {user.todos.map(todo => (
            <li key={todo.id} className="flex justify-between items-center mb-1">
            {/* Bal oldal: cím + státusz */}
            <div>
                <span className="font-medium">{todo.title}</span>{' '}
                <span className={todo.isCompeted ? 'text-green-600' : 'text-yellow-600'}>
                {todo.isCompeted ? '✅ kész' : '⏳ folyamatban'}
                </span>
            </div>

            {/* Jobb oldal: szerkesztő gomb kis szünettel */}
            <button
                //data-todo-id={todo.id}
                onClick={() => navigate(`/edit-todo/${todo.id}`,{state: {user}})}
                className="ml-3 w-6 h-6 bg-indigo-500 hover:bg-indigo-600 text-white rounded flex items-center justify-center text-xs"
            >
                ✏️
            </button>
            </li>
          ))}
        </ul>
      ) : (
        <p className="text-sm text-gray-400 mt-3 italic">Nincsenek feladatai.</p>
      )}


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

export default UserEditCard;
