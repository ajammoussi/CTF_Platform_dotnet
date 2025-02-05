import React, { useState, useEffect } from 'react';
import { useNavigate, useSearchParams, Link } from 'react-router-dom';
import { KeyIcon, XCircleIcon } from '@heroicons/react/24/outline';

function ResetPassword() {
  const [formData, setFormData] = useState({
    newPassword: '',
    confirmPassword: '',
  });
  const [error, setError] = useState('');
  const [isLoading, setIsLoading] = useState(false);
  const navigate = useNavigate();
  const [searchParams] = useSearchParams();
  const token = searchParams.get('token');

  const handleSubmit = async (e) => {
    e.preventDefault();
    setError('');

    if (formData.newPassword !== formData.confirmPassword) {
      setError('Passwords do not match');
      return;
    }

    if (formData.newPassword.length < 8) {
      setError('Password must be at least 8 characters long');
      return;
    }

    setIsLoading(true);

    try {
      const response = await fetch('https://localhost:7226/api/auth/reset-password', {
        method: 'POST',
        headers: {
          'Content-Type': 'application/json',
        },
        body: JSON.stringify({
          token: token,
          newPassword: formData.newPassword,
          confirmPassword: formData.confirmPassword,
        }),
      });

      const data = await response.json();

      if (response.ok) {
        navigate('/login', { 
          state: { message: 'Password has been reset successfully. Please login with your new password.' }
        });
      } else {
        setError(data.message || 'Failed to reset password. Please try again.');
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

  if (!token) {
    return (
      <div className="min-h-screen bg-gray-900 flex flex-col items-center justify-center px-4">
        <div className="max-w-md w-full bg-gray-800 rounded-lg border border-red-500 p-8 space-y-6 text-center">
          <XCircleIcon className="h-12 w-12 mx-auto text-red-500" />
          <h2 className="text-xl font-bold text-red-500 font-mono">Invalid Reset Link</h2>
          <p className="text-gray-400 font-mono">
            The password reset link is invalid or has expired.
          </p>
          <div className="mt-4">
            <Link 
              to="/forgot-password" 
              className="text-green-500 hover:text-green-400 font-mono"
            >
              Request a new password reset
            </Link>
          </div>
        </div>
      </div>
    );
  }

  return (
    <div className="min-h-screen bg-gray-900 flex flex-col items-center justify-center px-4">
      <div className="max-w-md w-full bg-gray-800 rounded-lg border border-green-500 p-8 space-y-6">
        <div className="text-center">
          <h2 className="text-3xl font-bold text-green-500 font-mono">Reset Password</h2>
          <p className="mt-2 text-gray-400 font-mono">
            Enter your new password below.
          </p>
        </div>

        <form onSubmit={handleSubmit} className="space-y-6">
          <div>
            <label htmlFor="newPassword" className="block text-sm font-mono text-green-500 mb-2">
              New Password
            </label>
            <input
              type="password"
              id="newPassword"
              name="newPassword"
              value={formData.newPassword}
              onChange={handleChange}
              className="w-full bg-gray-900 border border-green-500 text-green-400 rounded px-4 py-2 font-mono focus:outline-none focus:ring-2 focus:ring-green-500"
              required
              minLength={8}
            />
          </div>

          <div>
            <label htmlFor="confirmPassword" className="block text-sm font-mono text-green-500 mb-2">
              Confirm Password
            </label>
            <input
              type="password"
              id="confirmPassword"
              name="confirmPassword"
              value={formData.confirmPassword}
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
                Resetting...
              </>
            ) : (
              <>
                <KeyIcon className="h-5 w-5 mr-2" />
                Reset Password
              </>
            )}
          </button>
        </form>
      </div>
    </div>
  );
}

export default ResetPassword; 