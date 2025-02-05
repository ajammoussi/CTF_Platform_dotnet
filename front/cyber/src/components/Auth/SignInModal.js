import React, { useState } from 'react';
import { XMarkIcon } from '@heroicons/react/24/outline';

function SignInModal({ onClose, onSwitchToSignUp }) {
  const [email, setEmail] = useState('');
  const [password, setPassword] = useState('');

  const handleSubmit = (e) => {
    e.preventDefault();
    // Handle sign in logic here
    console.log('Sign in:', { email, password });
  };

  return (
    <div className="fixed inset-0 bg-black bg-opacity-50 flex items-center justify-center p-4 z-50">
      <div className="bg-gray-800 rounded-lg border border-green-500 p-6 max-w-md w-full">
        <div className="flex justify-between items-center mb-6">
          <h2 className="text-2xl font-mono text-green-400">{'> Sign_In'}</h2>
          <button onClick={onClose} className="text-gray-400 hover:text-white">
            <XMarkIcon className="h-6 w-6" />
          </button>
        </div>

        <form onSubmit={handleSubmit} className="space-y-4">
          <div>
            <label htmlFor="email" className="block text-sm font-mono text-green-400 mb-2">
              {'> Email'}
            </label>
            <input
              type="email"
              id="email"
              value={email}
              onChange={(e) => setEmail(e.target.value)}
              className="w-full bg-gray-900 border border-green-500 text-green-400 rounded px-4 py-2 font-mono focus:outline-none focus:ring-2 focus:ring-green-500"
              required
            />
          </div>

          <div>
            <label htmlFor="password" className="block text-sm font-mono text-green-400 mb-2">
              {'> Password'}
            </label>
            <input
              type="password"
              id="password"
              value={password}
              onChange={(e) => setPassword(e.target.value)}
              className="w-full bg-gray-900 border border-green-500 text-green-400 rounded px-4 py-2 font-mono focus:outline-none focus:ring-2 focus:ring-green-500"
              required
            />
          </div>

          <button
            type="submit"
            className="w-full bg-green-500 text-black py-2 px-4 rounded font-mono hover:bg-green-400 transition-colors duration-300"
          >
            {'> Sign_In()'}
          </button>
        </form>

        <div className="mt-4 text-center">
          <button
            onClick={onSwitchToSignUp}
            className="text-green-400 hover:text-green-300 font-mono text-sm"
          >
            {'> New_User? Create_Account()'}
          </button>
        </div>
      </div>
    </div>
  );
}

export default SignInModal;