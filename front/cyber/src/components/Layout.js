import React from 'react';
import { Outlet } from 'react-router-dom';
import Navbar from './Navbar';
import Footer from './Footer';

function Layout() {
  return (
    <div className="min-h-screen bg-gray-900 text-white">
      <Navbar />
      <Outlet />
      <Footer />
    </div>
  );
}

export default Layout