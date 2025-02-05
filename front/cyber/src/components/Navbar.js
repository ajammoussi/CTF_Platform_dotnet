import React, { useState, useEffect } from 'react';
import { Link, useNavigate } from 'react-router-dom';
import {
  UserCircleIcon,
  ArrowRightOnRectangleIcon,
  Bars3Icon,
  XMarkIcon,
  UsersIcon,
  UserPlusIcon,
} from '@heroicons/react/24/outline';

function Navbar() {
  const [isOpen, setIsOpen] = useState(false);
  const [isLoggedIn, setIsLoggedIn] = useState(false);
  const [isAdmin, setIsAdmin] = useState(false);
  const [showTeamDropdown, setShowTeamDropdown] = useState(false);
  const navigate = useNavigate();

  useEffect(() => {
    const token = localStorage.getItem('token');
    const role = localStorage.getItem('role');
    setIsLoggedIn(!!token);
    setIsAdmin(role === 'Admin');
  }, []);

  const handleLogout = async () => {
    try {
      const token = localStorage.getItem('token');
      const userId = localStorage.getItem('userId');
      
      const response = await fetch('https://localhost:7226/api/auth/logout', {
        method: 'POST',
        headers: {
          'Content-Type': 'application/json',
          'Authorization': `Bearer ${token}`
        },
        body: JSON.stringify({
          token: token,
          userId: parseInt(userId)
        })
      });

      if (response.ok) {
        localStorage.removeItem('token');
        localStorage.removeItem('userId');
        localStorage.removeItem('role');
        localStorage.removeItem('username');
        setIsLoggedIn(false);
        setIsAdmin(false);
        navigate('/');
      } else {
        console.error('Logout failed');
      }
    } catch (error) {
      console.error('Error during logout:', error);
    }
  };

  const TeamDropdown = () => (
    <div className="relative">
      <button
        onClick={() => setShowTeamDropdown(!showTeamDropdown)}
        className="flex items-center space-x-2 text-gray-300 hover:text-white"
      >
        <UsersIcon className="h-6 w-6" />
        <span>Team</span>
      </button>
      {showTeamDropdown && (
        <div className="absolute right-0 mt-2 w-48 bg-gray-800 rounded-md shadow-lg py-1 z-10">
          <Link
            to="/create-team"
            className="block px-4 py-2 text-sm text-gray-300 hover:bg-gray-700 flex items-center"
            onClick={() => setShowTeamDropdown(false)}
          >
            <UsersIcon className="h-5 w-5 mr-2" />
            Create Team
          </Link>
          <Link
            to="/invite-team"
            className="block px-4 py-2 text-sm text-gray-300 hover:bg-gray-700 flex items-center"
            onClick={() => setShowTeamDropdown(false)}
          >
            <UserPlusIcon className="h-5 w-5 mr-2" />
            Invite Member
          </Link>
        </div>
      )}
    </div>
  );

  return (
    <nav className="bg-gray-900 border-b border-green-500">
      <div className="max-w-7xl mx-auto px-4 sm:px-6 lg:px-8">
        <div className="flex items-center justify-between h-16">
          {/* Logo and main nav links */}
          <div className="flex items-center">
            <Link to="/" className="text-green-500 font-bold text-xl font-mono">
              CTF Platform
            </Link>
            {isLoggedIn && (
              <div className="hidden md:flex ml-10 space-x-8">
                <Link to="/challenges" className="text-gray-300 hover:text-white">
                  Challenges
                </Link>
                <Link to="/leaderboard" className="text-gray-300 hover:text-white">
                  Leaderboard
                </Link>
                <Link to="/about" className="text-gray-300 hover:text-white">
                  About
                </Link>
              </div>
            )}
          </div>

          {/* Right side nav items */}
          <div className="hidden md:flex items-center space-x-8">
            {isLoggedIn ? (
              <>
                <TeamDropdown />
                {isAdmin && (
                  <Link to="/adminDashboard" className="text-gray-300 hover:text-white flex items-center">
                    <UserCircleIcon className="h-6 w-6 mr-1" />
                    Admin
                  </Link>
                )}
                <button
                  onClick={handleLogout}
                  className="text-gray-300 hover:text-white flex items-center"
                >
                  <ArrowRightOnRectangleIcon className="h-6 w-6 mr-1" />
                  Logout
                </button>
              </>
            ) : (
              <Link
                to="/login"
                className="text-gray-300 hover:text-white flex items-center"
              >
                <ArrowRightOnRectangleIcon className="h-6 w-6 mr-1" />
                Login
              </Link>
            )}
          </div>

          {/* Mobile menu button */}
          <div className="md:hidden flex items-center">
            <button
              onClick={() => setIsOpen(!isOpen)}
              className="text-gray-300 hover:text-white"
            >
              {isOpen ? (
                <XMarkIcon className="h-6 w-6" />
              ) : (
                <Bars3Icon className="h-6 w-6" />
              )}
            </button>
          </div>
        </div>
      </div>

      {/* Mobile menu */}
      {isOpen && (
        <div className="md:hidden bg-gray-800">
          <div className="px-2 pt-2 pb-3 space-y-1">
            {isLoggedIn ? (
              <>
                <Link
                  to="/challenges"
                  className="block px-3 py-2 text-gray-300 hover:text-white"
                >
                  Challenges
                </Link>
                <Link
                  to="/leaderboard"
                  className="block px-3 py-2 text-gray-300 hover:text-white"
                >
                  Leaderboard
                </Link>
                <Link
                  to="/about"
                  className="block px-3 py-2 text-gray-300 hover:text-white"
                >
                  About
                </Link>
                <Link
                  to="/create-team"
                  className="block px-3 py-2 text-gray-300 hover:text-white"
                >
                  Create Team
                </Link>
                <Link
                  to="/invite-team"
                  className="block px-3 py-2 text-gray-300 hover:text-white"
                >
                  Invite to Team
                </Link>
                {isAdmin && (
                  <Link
                    to="/adminDashboard"
                    className="block px-3 py-2 text-gray-300 hover:text-white"
                  >
                    Admin Dashboard
                  </Link>
                )}
                <button
                  onClick={handleLogout}
                  className="block w-full text-left px-3 py-2 text-gray-300 hover:text-white"
                >
                  Logout
                </button>
              </>
            ) : (
              <Link
                to="/login"
                className="block px-3 py-2 text-gray-300 hover:text-white"
              >
                Login
              </Link>
            )}
          </div>
        </div>
      )}
    </nav>
  );
}

export default Navbar;