import React, { useState } from 'react';
import { useNavigate } from 'react-router-dom';
import { UsersIcon, XMarkIcon } from '@heroicons/react/24/outline';

function CreateTeam() {
  const [teamName, setTeamName] = useState('');
  const [error, setError] = useState('');
  const [isLoading, setIsLoading] = useState(false);
  const navigate = useNavigate();

  const validateTeamName = (name) => {
    if (name.length === 0) {
      return 'Team name is required';
    }
    if (name.length > 100) {
      return 'Team name must be less than 100 characters';
    }
    return null;
  };

  const handleSubmit = async (e) => {
    e.preventDefault();
    
    // Validate team name before submission
    const validationError = validateTeamName(teamName);
    if (validationError) {
      setError(validationError);
      return;
    }

    setError('');
    setIsLoading(true);

    try {
      const token = localStorage.getItem('token');
      const response = await fetch('https://localhost:7226/api/team/create-team', {
        method: 'POST',
        headers: {
          'Authorization': `Bearer ${token}`,
          'Content-Type': 'application/json'
        },
        body: JSON.stringify({
          teamName: teamName
        })
      });

      const data = await response.json();

      if (response.ok) {
        // Redirect to challenges page
        navigate('/challenges');
      } else {
        // Handle specific error messages from the API
        if (data.message) {
          setError(data.message);
        } else if (response.status === 400) {
          setError('You are already part of a team. Please leave your current team first.');
        } else if (response.status === 401) {
          setError('You must be logged in to create a team.');
          // Optionally redirect to login
          // navigate('/login');
        } else {
          setError('Failed to create team. Please try again.');
        }
      }
    } catch (error) {
      console.error('Error creating team:', error);
      setError('An error occurred while creating the team. Please try again.');
    } finally {
      setIsLoading(false);
    }
  };

  return (
    <div className="min-h-screen bg-gray-900 flex flex-col items-center justify-center px-4">
      <div className="max-w-md w-full bg-gray-800 rounded-lg border border-green-500 p-8 space-y-6">
        {/* Header */}
        <div className="flex items-center justify-between">
          <div className="flex items-center">
            <UsersIcon className="h-8 w-8 text-green-500 mr-3" />
            <h2 className="text-2xl font-bold text-green-500 font-mono">Create Team</h2>
          </div>
          <button
            onClick={() => navigate(-1)}
            className="text-gray-400 hover:text-white"
          >
            <XMarkIcon className="h-6 w-6" />
          </button>
        </div>

        {/* Form */}
        <form onSubmit={handleSubmit} className="space-y-6">
          <div>
            <label htmlFor="teamName" className="block text-sm font-mono text-green-500 mb-2">
              Team Name
            </label>
            <input
              type="text"
              id="teamName"
              value={teamName}
              onChange={(e) => {
                setTeamName(e.target.value);
                const validationError = validateTeamName(e.target.value);
                if (validationError) {
                  setError(validationError);
                } else {
                  setError('');
                }
              }}
              maxLength={100}
              className={`w-full bg-gray-900 border ${
                error ? 'border-red-500' : 'border-green-500'
              } text-green-400 rounded px-4 py-2 font-mono focus:outline-none focus:ring-2 focus:ring-green-500`}
              placeholder="Enter team name"
              required
            />
          </div>

          {error && (
            <div className="text-red-500 font-mono text-sm">
              {error}
            </div>
          )}

          <div className="flex justify-end space-x-4">
            <button
              type="button"
              onClick={() => navigate(-1)}
              className="px-4 py-2 font-mono text-gray-400 hover:text-white"
            >
              Cancel
            </button>
            <button
              type="submit"
              disabled={isLoading}
              className={`px-6 py-2 bg-green-500 text-black rounded font-mono hover:bg-green-400 flex items-center ${
                isLoading ? 'opacity-50 cursor-not-allowed' : ''
              }`}
            >
              {isLoading ? (
                <>
                  <svg className="animate-spin -ml-1 mr-3 h-5 w-5 text-black" xmlns="http://www.w3.org/2000/svg" fill="none" viewBox="0 0 24 24">
                    <circle className="opacity-25" cx="12" cy="12" r="10" stroke="currentColor" strokeWidth="4"></circle>
                    <path className="opacity-75" fill="currentColor" d="M4 12a8 8 0 018-8V0C5.373 0 0 5.373 0 12h4zm2 5.291A7.962 7.962 0 014 12H0c0 3.042 1.135 5.824 3 7.938l3-2.647z"></path>
                  </svg>
                  Creating...
                </>
              ) : (
                'Create Team'
              )}
            </button>
          </div>
        </form>

        {/* Info Section */}
        <div className="mt-8 border-t border-gray-700 pt-6">
          <h3 className="text-lg font-mono text-green-400 mb-4">Important Information</h3>
          <ul className="list-disc list-inside space-y-2 text-gray-300 font-mono text-sm">
            <li>Team names must be unique and less than 100 characters</li>
            <li>You must leave your current team before creating a new one</li>
            <li>As a team creator, you cannot leave the team (you must delete it instead)</li>
            <li>Your points will be transferred to the new team</li>
            <li>Team name cannot be changed after creation</li>
          </ul>
        </div>
      </div>
    </div>
  );
}

export default CreateTeam; 