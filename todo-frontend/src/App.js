import React from 'react';
import { Routes, Route } from 'react-router-dom';
import Layout from './components/Layout';

import UserList from './pages/UserList';
import EditUserPage from './pages/EditUserPage';
import EditTodoPage from './pages/EditTodoPage';

//Itt döntjük el, hogy a böngészőben éppen melyik oldal jelenjen meg
function App() {
  return (
    <Layout>
      <Routes>
        <Route path="/" element={<UserList />} />
        <Route path="/edit-user/:id" element={<EditUserPage />} />
        <Route path="/edit-todo/:id" element={<EditTodoPage />} />
      </Routes>
    </Layout>
  );
}

export default App;




