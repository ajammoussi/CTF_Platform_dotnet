import React, { useState } from 'react';
import { Link, useNavigate } from 'react-router-dom';
import { UserPlusIcon, XCircleIcon } from '@heroicons/react/24/outline';

function Signup() {
  const [formData, setFormData] = useState({
    username: '',
    email: '',
    password: '',
  });
  const [error, setError] = useState('');
  const [isLoading, setIsLoading] = useState(false);
  const navigate = useNavigate();

  const handleSubmit = async (e) => {
    e.preventDefault();
    setError('');
    setIsLoading(true);

    try {
      const response = await fetch('https://localhost:7226/api/auth/signup', {
        method: 'POST',
        headers: {
          'Content-Type': 'application/json',
        },
        body: JSON.stringify(formData),
      });

      const data = await response.json();

      if (response.ok) {
        // The signup endpoint automatically logs in the user
        localStorage.setItem('token', data.token);
        localStorage.setItem('userId', data.user.userId);
        localStorage.setItem('role', data.user.role);
        localStorage.setItem('username', data.user.username);
        navigate('/challenges');
      } else {
        setError(data.message || 'Registration failed. Please try again.');
      }
    } catch (error) {
      setError('An error occurred. Please try again.');
    } finally {
      setIsLoading(false);
    }
  };

  const handleChange = (e) => {
    setFormData({
      ...formData,
      [e.target.name]: e.target.value,
    });
  };

  return (
    <div className="min-h-screen bg-gray-900 flex flex-col items-center justify-center px-4">
      <div className="max-w-md w-full bg-gray-800 rounded-lg border border-green-500 p-8 space-y-6">
        <div className="text-center">
          <h2 className="text-3xl font-bold text-green-500 font-mono">Create Account</h2>
        </div>

        <form onSubmit={handleSubmit} className="space-y-6">
          <div>
            <label htmlFor="username" className="block text-sm font-mono text-green-500 mb-2">
              Username
            </label>
            <input
              type="text"
              id="username"
              name="username"
              value={formData.username}
              onChange={handleChange}
              className="w-full bg-gray-900 border border-green-500 text-green-400 rounded px-4 py-2 font-mono focus:outline-none focus:ring-2 focus:ring-green-500"
              required
            />
          </div>

          <div>
            <label htmlFor="email" className="block text-sm font-mono text-green-500 mb-2">
              Email
            </label>
            <input
              type="email"
              id="email"
              name="email"
              value={formData.email}
              onChange={handleChange}
              className="w-full bg-gray-900 border border-green-500 text-green-400 rounded px-4 py-2 font-mono focus:outline-none focus:ring-2 focus:ring-green-500"
              required
            />
          </div>

          <div>
            <label htmlFor="password" className="block text-sm font-mono text-green-500 mb-2">
              Password
            </label>
            <input
              type="password"
              id="password"
              name="password"
              value={formData.password}
              onChange={handleChange}
              className="w-full bg-gray-900 border border-green-500 text-green-400 rounded px-4 py-2 font-mono focus:outline-none focus:ring-2 focus:ring-green-500"
              required
            />
          </div>

          {error && (
            <div className="text-red-500 font-mono text-sm flex items-center">
              <XCircleIcon className="h-5 w-5 mr-2" />
              {error}
            </div>
          )}

          <div className="text-center">
            <Link to="/login" className="text-green-500 hover:text-green-400 text-sm font-mono">
              Already have an account? Login
            </Link>
          </div>

          <button
            type="submit"
            disabled={isLoading}
            className={`w-full px-6 py-3 bg-green-500 text-black rounded font-mono hover:bg-green-400 flex items-center justify-center ${
              isLoading ? 'opacity-50 cursor-not-allowed' : ''
            }`}
          >
            {isLoading ? (
              <>
                <svg className="animate-spin -ml-1 mr-3 h-5 w-5 text-black" xmlns="http://www.w3.org/2000/svg" fill="none" viewBox="0 0 24 24">
                  <circle className="opacity-25" cx="12" cy="12" r="10" stroke="currentColor" strokeWidth="4"></circle>
                  <path className="opacity-75" fill="currentColor" d="M4 12a8 8 0 018-8V0C5.373 0 0 5.373 0 12h4zm2 5.291A7.962 7.962 0 014 12H0c0 3.042 1.135 5.824 3 7.938l3-2.647z"></path>
                </svg>
                Creating Account...
              </>
            ) : (
              <>
                <UserPlusIcon className="h-5 w-5 mr-2" />
                Sign Up
              </>
            )}
          </button>
        </form>
      </div>
    </div>
  );
}

export default Signup; 