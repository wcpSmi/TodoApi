import React, { useEffect, useState } from 'react';
import { useNavigate, useParams } from 'react-router-dom';
import UserEditCard from '../components/UserEditCard';


// 📝 Ez az oldal felel a felhasználó szerkesztés megjelenítéséért
function EditUserPage() {
  const navigate = useNavigate();
  const { id } =useParams();
  const [user, setUser] = React.useState([id]);

  //Adatok lekérése a backend -től
  useEffect(() => {
    fetch(`http://192.168.1.8:5000/api/user/${id}`) // 🔗 ID alapján kérjük le
      .then(res => res.json())
      .then(data => setUser(data))
      .catch(err => console.error('Hiba a felhasználó lekérésekor:', err));
  }, [id]);

  return (
    <div className="text-center">
      <h1 className="text-2xl font-bold text-indigo-700">Felhasználó szerkesztése </h1>
      <p className="mt-4 text-gray-700 text-lg">Szerkesztett ID: {id}</p>     
        <div className='mt-6 max-w-screen-md'>
            {/* Ha megjött az adat, csak akkor jelenítjük meg a kártyát */}
            {user ? (
            <UserEditCard user={user} />
            ) : (
            <p className="text-gray-500 italic">Betöltés...</p>
            )}
        </div>
      <button
        onClick={() => navigate('/')}
        className="mt-6 bg-gray-300 hover:bg-gray-400 text-black font-medium py-2 px-4 rounded-lg justify-center"
      >
        ⬅️ Vissza
      </button>
    </div>
  );
}

export default EditUserPage;
