import React, { useState } from 'react';
import { Link } from 'react-router-dom';
import { EnvelopeIcon, XCircleIcon, CheckCircleIcon } from '@heroicons/react/24/outline';

function ForgotPassword() {
  const [email, setEmail] = useState('');
  const [error, setError] = useState('');
  const [success, setSuccess] = useState('');
  const [isLoading, setIsLoading] = useState(false);

  const handleSubmit = async (e) => {
    e.preventDefault();
    setError('');
    setSuccess('');
    setIsLoading(true);

    try {
      const response = await fetch('https://localhost:7226/api/auth/forgot-password', {
        method: 'POST',
        headers: {
          'Content-Type': 'application/json',
        },
        body: JSON.stringify({ email }),
      });

      const data = await response.json();

      if (response.ok) {
        setSuccess('Password reset link has been sent to your email.');
        setEmail(''); // Clear the input
      } else {
        setError(data.message || 'Failed to process request. Please try again.');
      }
    } catch (error) {
      setError('An error occurred. Please try again.');
    } finally {
      setIsLoading(false);
    }
  };

  return (
    <div className="min-h-screen bg-gray-900 flex flex-col items-center justify-center px-4">
      <div className="max-w-md w-full bg-gray-800 rounded-lg border border-green-500 p-8 space-y-6">
        <div className="text-center">
          <h2 className="text-3xl font-bold text-green-500 font-mono">Reset Password</h2>
          <p className="mt-2 text-gray-400 font-mono">
            Enter your email address and we'll send you a link to reset your password.
          </p>
        </div>

        <form onSubmit={handleSubmit} className="space-y-6">
          <div>
            <label htmlFor="email" className="block text-sm font-mono text-green-500 mb-2">
              Email Address
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

          {error && (
            <div className="text-red-500 font-mono text-sm flex items-center">
              <XCircleIcon className="h-5 w-5 mr-2" />
              {error}
            </div>
          )}

          {success && (
            <div className="text-green-500 font-mono text-sm flex items-center">
              <CheckCircleIcon className="h-5 w-5 mr-2" />
              {success}
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
                Sending...
              </>
            ) : (
              <>
                <EnvelopeIcon className="h-5 w-5 mr-2" />
                Send Reset Link
              </>
            )}
          </button>
        </form>

        <div className="text-center">
          <Link to="/login" className="text-green-500 hover:text-green-400 text-sm font-mono">
            Back to Login
          </Link>
        </div>
      </div>
    </div>
  );
}

export default ForgotPassword; 