import React, { useEffect, useState } from 'react';
import UserCard from '../components/UserCard';

// Főoldal – ahol a kártyák vannak
function UserList() {
    const [users, setUsers] = React.useState([]);
  
    React.useEffect(() => {
      fetch('http://192.168.1.8:5000/api/user')
        .then(res => res.json())
        .then(data => setUsers(data))
        .catch(err => console.error('Hiba a lekérés során:', err));
    }, []);
  
    return (
      <>
        <h1 className="text-3xl font-bold text-center text-indigo-700 mb-8">
          📋 Felhasználók és feladataik
        </h1>
        {users.length === 0 ? (
          <p className="text-center text-gray-500">Nincs még felhasználó.</p>
        ) : (
          <div className="grid grid-cols-1 md:grid-cols-2 lg:grid-cols-3 gap-6">
            {users.map(user => (
              <UserCard key={user.id} user={user} />
            ))}
          </div>
        )}
  
            {/* ➕ Gomb hozzáadása */}
            <div className="mt-4 text-right">
          <button
          onClick={() => console.log(`Új felgasználó`)}
          className="bg-indigo-500 hover:bg-indigo-600 text-white font-medium py-1 px-3 rounded-lg text-sm"
          >
          ➕Hozzáadás
          </button>
      </div>
      </>
    );
  }

  export default UserList;