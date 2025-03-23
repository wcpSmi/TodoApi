import React from 'react';

function Layout({ children }) {
  return (
    <div className="min-h-screen bg-gray-100 p-6 font-sans">
      <div className="max-w-5xl mx-auto">
        {children}
      </div>
    </div>
  );
}

export default Layout;
