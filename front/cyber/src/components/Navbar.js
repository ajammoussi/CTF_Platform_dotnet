import React, { useState } from 'react';
import { Link, useLocation } from 'react-router-dom';
import { Bars3Icon, XMarkIcon, TrophyIcon, InformationCircleIcon, UserCircleIcon } from '@heroicons/react/24/outline';
import { ROUTES } from '../routes';
import SignInModal from './Auth/SignInModal';
import SignUpModal from './Auth/SignUpModal';

function Navbar() {
  const [isOpen, setIsOpen] = useState(false);
  const [showSignIn, setShowSignIn] = useState(false);
  const [showSignUp, setShowSignUp] = useState(false);
  const location = useLocation();

  const isActive = (path) => {
    return location.pathname === path ? 'bg-gray-700' : '';
  };

  const navItems = [
    { path: ROUTES.HOME, label: 'Home' },
    { path: ROUTES.EVENTS, label: 'Events' },
    { path: ROUTES.LEADERBOARD, label: 'Leaderboard', icon: TrophyIcon },
    { path: ROUTES.ABOUT, label: 'About', icon: InformationCircleIcon },
  ];

  const handleSignInClick = () => {
    setShowSignIn(true);
    setIsOpen(false);
  };

  const handleSignUpClick = () => {
    setShowSignUp(true);
    setIsOpen(false);
  };

  const switchToSignUp = () => {
    setShowSignIn(false);
    setShowSignUp(true);
  };

  const switchToSignIn = () => {
    setShowSignUp(false);
    setShowSignIn(true);
  };

  return (
    <>
      <nav className="bg-gray-800">
        <div className="max-w-7xl mx-auto px-4 sm:px-6 lg:px-8">
          <div className="flex items-center justify-between h-16">
            <div className="flex-shrink-0">
              <Link to={ROUTES.HOME} className="flex items-center">
                <span className="text-2xl font-bold text-green-500">{'{CTF}'}</span>
                <span className="ml-2 text-xl font-semibold text-white">CyberChallenge</span>
              </Link>
            </div>
            
            {/* Desktop menu */}
            <div className="hidden md:flex md:items-center md:space-x-4">
              {navItems.map((item) => (
                <Link 
                  key={item.path}
                  to={item.path}
                  className={`px-3 py-2 rounded-md text-sm font-mono text-white hover:bg-gray-700 flex items-center ${isActive(item.path)}`}
                >
                  {item.icon && <item.icon className="h-4 w-4 mr-1" />}
                  {`> ${item.label}`}
                </Link>
              ))}
              
              {/* Auth buttons */}
              <div className="flex items-center space-x-2 ml-4">
                <button
                  onClick={handleSignInClick}
                  className="px-3 py-2 rounded-md text-sm font-mono text-white hover:bg-gray-700 flex items-center"
                >
                  <UserCircleIcon className="h-4 w-4 mr-1" />
                  {'> Sign_In'}
                </button>
                <button
                  onClick={handleSignUpClick}
                  className="px-3 py-2 rounded-md text-sm font-mono bg-green-500 text-black hover:bg-green-400"
                >
                  {'> Sign_Up'}
                </button>
              </div>
            </div>

            {/* Mobile menu button */}
            <div className="md:hidden">
              <button
                onClick={() => setIsOpen(!isOpen)}
                className="inline-flex items-center justify-center p-2 rounded-md text-gray-400 hover:text-white hover:bg-gray-700 focus:outline-none"
              >
                {isOpen ? (
                  <XMarkIcon className="block h-6 w-6" />
                ) : (
                  <Bars3Icon className="block h-6 w-6" />
                )}
              </button>
            </div>
          </div>
        </div>

        {/* Mobile menu */}
        {isOpen && (
          <div className="md:hidden">
            <div className="px-2 pt-2 pb-3 space-y-1 sm:px-3">
              {navItems.map((item) => (
                <Link
                  key={item.path}
                  to={item.path}
                  className={`block px-3 py-2 rounded-md text-base font-mono text-white hover:bg-gray-700 flex items-center ${isActive(item.path)}`}
                  onClick={() => setIsOpen(false)}
                >
                  {item.icon && <item.icon className="h-4 w-4 mr-1" />}
                  {`> ${item.label}`}
                </Link>
              ))}
              
              {/* Mobile auth buttons */}
              <button
                onClick={handleSignInClick}
                className="block w-full text-left px-3 py-2 rounded-md text-base font-mono text-white hover:bg-gray-700 flex items-center"
              >
                <UserCircleIcon className="h-4 w-4 mr-1" />
                {'> Sign_In'}
              </button>
              <button
                onClick={handleSignUpClick}
                className="block w-full text-left px-3 py-2 rounded-md text-base font-mono bg-green-500 text-black hover:bg-green-400"
              >
                {'> Sign_Up'}
              </button>
            </div>
          </div>
        )}
      </nav>

      {/* Auth Modals */}
      {showSignIn && (
        <SignInModal
          onClose={() => setShowSignIn(false)}
          onSwitchToSignUp={switchToSignUp}
        />
      )}
      {showSignUp && (
        <SignUpModal
          onClose={() => setShowSignUp(false)}
          onSwitchToSignIn={switchToSignIn}
        />
      )}
    </>
  );
}

export default Navbar;