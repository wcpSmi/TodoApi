import React, { useEffect, useState } from 'react';
import { useNavigate, useParams,useLocation } from 'react-router-dom';
import TodoEditCard from '../components/TodoEditCard';

//Ez az oldal a todo szerkesztésére van
function EditTodoPage()
{

    const location = useLocation();
    const user = location.state?.user; 

    const navigate = useNavigate();
    const { id } =useParams();
    const [todo, setTodo] = useState(null);
    

      //Adatok lekérése a backend -től
      useEffect(() => {
        fetch(`http://192.168.1.8:5000/api/Todo/${id}`) //ID alapján kérjük le
          .then(res => res.json())
          .then(data => setTodo(data))
          .catch(err => console.error('Hiba a feladat lekérésekor:', err));
      }, [id]);

      return(
     
        <div className="text-center">
        <h1 className="text-2xl font-bold text-indigo-700">{user.name} </h1>
        <h1 className="text-2xl font-bold text-indigo-700">Feladat szerkesztése  </h1>
    
          <div className='mt-6 max-w-screen-md'>
              {/* Ha megjött az adat, csak akkor jelenítjük meg a kártyát */}
              {todo ? (
              <TodoEditCard todo={todo} /> //A szülő komponens propként adja át
              ) : (
              <p className="text-gray-500 italic">Betöltés...</p>
              )}
          </div>
        <button
          onClick={() => navigate(`/edit-user/${user.id}`)}
          className="mt-6 bg-gray-300 hover:bg-gray-400 text-black font-medium py-2 px-4 rounded-lg justify-center"
        >
          ⬅️ Vissza
        </button>
      </div>
      );
}

export default EditTodoPage;