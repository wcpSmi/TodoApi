// React importálása, hogy JSX-t (HTML-szerű szintaxist) használhassunk
import React from 'react';

// UserCard komponens definíciója
// A `user` nevű objektumot props-ként (bemeneti adatként) kapja meg
function UserCard({ user }) {
  return (
    // A teljes kártya konténer eleme
    // Tailwind osztályok: fehér háttér, lekerekített sarkok, árnyék, padding
    <div className="bg-white rounded-2xl shadow-md p-4">

      {/* Felhasználó neve – nagyobb, félkövér, kékeslila színű (indigo) */}
      <h2 className="text-xl font-semibold text-indigo-600">{user.name}</h2>

      {/* Email cím – kisebb, szürke szöveg, margóval alatta */}
      <p className="text-sm text-gray-500 mb-2">{user.email}</p>

      {/* Feltételes megjelenítés: ha a user-nek van legalább 1 todo-ja */}
      {user.todos && user.todos.length > 0 ? (

        // Lista elem, ami felsorolja a todo-kat
        // Tailwind: belső margó, felsorolás ponttal, szürke szöveg, kis méret
        <ul className="mt-3 list-disc list-inside text-gray-700 text-sm">

          {/* A todo-k végigiterálása: minden feladathoz egy <li> */}
          {user.todos.map(todo => (
            <li key={todo.id}>

              {/* A feladat címe – félkövér */}
              <span className="font-medium">{todo.title}</span>{' '}

              {/* A státusz – ha kész zöld, ha nem, sárga szöveg */}
              <span className={todo.isCompeted ? 'text-green-600' : 'text-yellow-600'}>
                {todo.isCompeted ? '✅ kész' : '⏳ folyamatban'}
              </span>
            </li>
          ))}
        </ul>
      ) : (
        // Ha nincs egyetlen todo sem: egy szürke, dőlt betűs szöveg
        <p className="text-sm text-gray-400 mt-3 italic">Nincsenek feladatai.</p>
      )}

      {/* ➕ Gomb hozzáadása */}
      <div className="mt-4 text-right">
        <button
          onClick={() => console.log(`Szerkesztés ID: ${user.id}`)}
          className="bg-indigo-500 hover:bg-indigo-600 text-white font-medium py-1 px-3 rounded-lg text-sm"
        >
          ✏️ Szerkesztés
        </button>
      </div>

    </div>
  );
}

// A komponens exportálása, hogy máshol (pl. App.js) be lehessen importálni
export default UserCard;
