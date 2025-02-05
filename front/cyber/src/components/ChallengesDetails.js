import React, { useState,  useEffect  } from 'react';
import { useParams } from 'react-router-dom';
import { LockClosedIcon, CheckCircleIcon, XCircleIcon, InformationCircleIcon, TrophyIcon, UserCircleIcon } from '@heroicons/react/24/outline';

const challengesData = {
  1: {
    title: "Web Security Challenge 2023",
    description: "Test your web application security skills with challenges ranging from XSS to SQL injection.",
    about: {
      organizer: "CyberSec Team",
      startDate: "2023-08-01",
      endDate: "2023-08-03",
      difficulty: "Intermediate",
      totalPoints: 1000,
      prerequisites: ["Basic JavaScript", "Web Security Fundamentals", "HTTP Protocol Knowledge"]
    },
    challenges: {
      "Web Exploitation": [
        {
          id: 1,
          title: "Simple XSS",
          description: "Find and exploit a cross-site scripting vulnerability in the given web application.",
          points: 100,

          difficulty: "Easy",
          solved: false
        },
        {
          id: 2,
          title: "CSRF Attack",
          description: "Exploit CSRF vulnerabilities to perform unauthorized actions.",
          points: 150,
          difficulty: "Medium",
          solved: false
        }
      ],
      "SQL Injection": [
        {
          id: 3,
          title: "SQL Injection Master",
          description: "Bypass the login system using SQL injection techniques.",
          points: 200,
          difficulty: "Medium",
          solved: false
        }
      ],
      "Session Security": [
        {
          id: 4,
          title: "Session Hijacking",
          description: "Exploit session management vulnerabilities to gain unauthorized access.",
          points: 300,
          difficulty: "Hard",
          solved: false
        }
      ]
    },
    leaderboard: [
      { rank: 1, username: "web_wizard", points: 600, solved: 3 },
      { rank: 2, username: "hack_master", points: 500, solved: 2 },
      { rank: 3, username: "security_pro", points: 300, solved: 1 }
    ],
    userStatus: {
      username: "current_user",
      totalPoints: 200,
      rank: 5,
      solvedTasks: 1,
      remainingTasks: 3
    }
  }
};

function ChallengesDetails() {
  const { id } = useParams();
  const challenge = challengesData[id];
  const [challenges, setChallenges] = useState([]);
  const [correctSubmissions, setCorrectSubmissions] = useState([]);

  const fetchChallenges = async () => {
    try {
      const token = localStorage.getItem('token');
      
      const response = await fetch('https://localhost:7226/api/challenges', {
        method: 'GET',
        headers: {
          'Authorization': `Bearer ${token}`,
          'Content-Type': 'application/json'
        }
      });
  
      if (!response.ok) {
        throw new Error('Failed to fetch challenges');
      }
  
      const data = await response.json();
      // Make sure we're setting the actual array of challenges
      setChallenges(data.items.$values);
    } catch (error) {
      console.error('Error fetching challenges:', error);
      setChallenges([]); // Set empty array on error
    }
  };

  const fetchCorrectSubmissions = async () => {
    try {
      const token = localStorage.getItem('token');
      const response = await fetch('https://localhost:7226/api/user/correctSubmissions', {
        method: 'GET',
        headers: {
          'Authorization': `Bearer ${token}`,
          'Content-Type': 'application/json'
        }
      });

      if (!response.ok) {
        throw new Error('Failed to fetch correct submissions');
      }

      const data = await response.json();
      setCorrectSubmissions(data.$values);
    } catch (error) {
      console.error('Error fetching correct submissions:', error);
      setCorrectSubmissions([]);
    }
  };

  useEffect(() => {
    fetchChallenges();
    fetchCorrectSubmissions();
  }, []);

  const [selectedTask, setSelectedTask] = useState(null);
  const [flag, setFlag] = useState('');
  const [submitStatus, setSubmitStatus] = useState(null);
  const [activeTab, setActiveTab] = useState('tasks');

  const handleFlagSubmit = async (taskId) => {
    try {
      const token = localStorage.getItem('token');
      const response = await fetch('https://localhost:7226/api/challenges/solve', {
        method: 'POST',
        headers: {
          'Authorization': `Bearer ${token}`,
          'Content-Type': 'application/json'
        },
        body: JSON.stringify({
          challengeId: taskId,
          submittedFlag: flag
        })
      });

      const data = await response.json();
      
      if (response.ok && data.message === "Correct") {
        setSubmitStatus('success');
        // Refresh both challenges and submissions
        fetchChallenges();
        fetchCorrectSubmissions();
      } else {
        setSubmitStatus('error');
      }
    } catch (error) {
      console.error('Error submitting flag:', error);
      setSubmitStatus('error');
    }

    setTimeout(() => setSubmitStatus(null), 3000);
    setFlag('');
    setSelectedTask(null);
  };

  if (!challenge) return <div>Challenge not found</div>;

  // Add a check to ensure challenges exists before mapping
  if (!challenges) {
    return <div>Loading...</div>;
  }

  const TabButton = ({ name, label, icon: Icon }) => (
    <button
      onClick={() => setActiveTab(name)}
      className={`flex items-center px-4 py-2 rounded-lg font-mono ${
        activeTab === name
          ? 'bg-green-500 text-black'
          : 'text-green-500 hover:bg-gray-700'
      }`}
    >
      <Icon className="h-5 w-5 mr-2" />
      {label}
    </button>
  );

  return (
    <div className="max-w-7xl mx-auto py-12 px-4 sm:px-6 lg:px-8">
      <h2 className="text-4xl font-extrabold text-green-500 glitch-text mb-8">
        {challenge.title}
      </h2>

      {/* Navigation Tabs */}
      <div className="flex space-x-4 mb-8">
        <TabButton name="tasks" label="> Tasks" icon={LockClosedIcon} />
        <TabButton name="about" label="> About" icon={InformationCircleIcon} />
        <TabButton name="leaderboard" label="> Leaderboard" icon={TrophyIcon} />
        <TabButton name="status" label="> Your Status" icon={UserCircleIcon} />
      </div>

      {/* Tasks Tab */}
      {activeTab === 'tasks' && (
        <div className="grid grid-cols-1 gap-8">
          {/* Group challenges by category */}
          {Object.entries(
            challenges.reduce((acc, challenge) => {
              if (!acc[challenge.category]) {
                acc[challenge.category] = [];
              }
              acc[challenge.category].push(challenge);
              return acc;
            }, {})
          ).map(([category, categoryChallenges]) => (
            <div key={category} className="bg-gray-800 rounded-lg border border-green-500 p-6">
              <h3 className="text-2xl font-mono text-green-400 mb-6">{`> ${category}`}</h3>
              <div className="space-y-4">
                {categoryChallenges.map((challenge) => {
                  const isSolved = correctSubmissions.some(
                    submission => submission.challengeId === challenge.challengeId
                  );

                  return (
                    <div 
                      key={challenge.challengeId}
                      className="bg-gray-900 rounded-lg p-4 border border-green-500 hover:shadow-[0_0_10px_rgba(34,197,94,0.3)] cursor-pointer"
                      onClick={() => setSelectedTask(challenge)}
                    >
                      <div className="flex justify-between items-start">
                        <div>
                          <h4 className="text-lg font-mono text-green-400">{challenge.name}</h4>
                          <p className="text-gray-400 font-mono mt-2">{challenge.description}</p>
                        </div>
                        <div className="flex items-center space-x-4">
                          <span className="text-green-500 font-mono">{challenge.points} pts</span>
                          {isSolved ? (
                            <CheckCircleIcon className="h-6 w-6 text-green-500" />
                          ) : (
                            <LockClosedIcon className="h-6 w-6 text-gray-500" />
                          )}
                        </div>
                      </div>
                    </div>
                  );
                })}
              </div>
            </div>
          ))}
        </div>
      )}

      {/* About Tab */}
      {activeTab === 'about' && (
        <div className="bg-gray-800 rounded-lg border border-green-500 p-6">
          <h3 className="text-2xl font-mono text-green-400 mb-6">{`> About_Challenge`}</h3>
          <div className="space-y-4">
            <div className="grid grid-cols-1 md:grid-cols-2 gap-6">
              <div>
                <h4 className="text-green-400 font-mono mb-2">Description</h4>
                <p className="text-gray-300 font-mono">{challenge.description}</p>
              </div>
              <div>
                <h4 className="text-green-400 font-mono mb-2">Details</h4>
                <ul className="space-y-2 text-gray-300 font-mono">
                  <li>Organizer: {challenge.about.organizer}</li>
                  <li>Start Date: {challenge.about.startDate}</li>
                  <li>End Date: {challenge.about.endDate}</li>
                  <li>Difficulty: {challenge.about.difficulty}</li>
                  <li>Total Points: {challenge.about.totalPoints}</li>
                </ul>
              </div>
            </div>
            <div>
              <h4 className="text-green-400 font-mono mb-2">Prerequisites</h4>
              <ul className="list-disc list-inside text-gray-300 font-mono">
                {challenge.about.prerequisites.map((prereq, index) => (
                  <li key={index}>{prereq}</li>
                ))}
              </ul>
            </div>
          </div>
        </div>
      )}

      {/* Leaderboard Tab */}
      {activeTab === 'leaderboard' && (
        <div className="bg-gray-800 rounded-lg border border-green-500 p-6">
          <h3 className="text-2xl font-mono text-green-400 mb-6">{`> Challenge_Leaderboard`}</h3>
          <div className="space-y-4">
            {challenge.leaderboard.map((player) => (
              <div 
                key={player.rank}
                className="bg-gray-900 rounded-lg p-4 border border-green-500"
              >
                <div className="flex justify-between items-center">
                  <div className="flex items-center">
                    <span className="font-mono text-green-400 mr-4">#{player.rank}</span>
                    <span className="font-mono text-white">{player.username}</span>
                  </div>
                  <div className="text-green-500 font-mono">{player.points} pts</div>
                </div>
              </div>
            ))}
          </div>
        </div>
      )}

      {/* Status Tab */}
      {activeTab === 'status' && (
        <div className="bg-gray-800 rounded-lg border border-green-500 p-6">
          <h3 className="text-2xl font-mono text-green-400 mb-6">{`> Your_Status`}</h3>
          <div className="grid grid-cols-1 md:grid-cols-2 gap-8">
            <div className="bg-gray-900 rounded-lg p-6 border border-green-500">
              <h4 className="text-xl font-mono text-green-400 mb-4">Progress Overview</h4>
              <div className="space-y-4">
                <div className="flex justify-between items-center">
                  <span className="text-gray-300 font-mono">Total Points:</span>
                  <span className="text-green-500 font-mono">{challenge.userStatus.totalPoints}</span>
                </div>
                <div className="flex justify-between items-center">
                  <span className="text-gray-300 font-mono">Current Rank:</span>
                  <span className="text-green-500 font-mono">#{challenge.userStatus.rank}</span>
                </div>
                <div className="flex justify-between items-center">
                  <span className="text-gray-300 font-mono">Solved Tasks:</span>
                  <span className="text-green-500 font-mono">{challenge.userStatus.solvedTasks}</span>
                </div>
                <div className="flex justify-between items-center">
                  <span className="text-gray-300 font-mono">Remaining Tasks:</span>
                  <span className="text-green-500 font-mono">{challenge.userStatus.remainingTasks}</span>
                </div>
              </div>
            </div>
            <div className="bg-gray-900 rounded-lg p-6 border border-green-500">
              <h4 className="text-xl font-mono text-green-400 mb-4">Achievement Progress</h4>
              <div className="relative pt-1">
                <div className="flex mb-2 items-center justify-between">
                  <div>
                    <span className="text-xs font-semibold inline-block py-1 px-2 uppercase rounded-full text-green-500 bg-green-200">
                      Task Progress
                    </span>
                  </div>
                  <div className="text-right">
                    <span className="text-xs font-semibold inline-block text-green-500">
                      {Math.round((challenge.userStatus.solvedTasks / (challenge.userStatus.solvedTasks + challenge.userStatus.remainingTasks)) * 100)}%
                    </span>
                  </div>
                </div>
                <div className="overflow-hidden h-2 mb-4 text-xs flex rounded bg-green-200">
                  <div
                    style={{ width: `${(challenge.userStatus.solvedTasks / (challenge.userStatus.solvedTasks + challenge.userStatus.remainingTasks)) * 100}%` }}
                    className="shadow-none flex flex-col text-center whitespace-nowrap text-white justify-center bg-green-500"
                  ></div>
                </div>
              </div>
            </div>
          </div>
        </div>
      )}

      {/* Flag Submission Modal */}
      {selectedTask && (
        <div className="fixed inset-0 bg-black bg-opacity-50 flex items-center justify-center p-4">
          <div className="bg-gray-800 rounded-lg border border-green-500 p-6 max-w-md w-full">
            <h4 className="text-xl font-mono text-green-400 mb-4">{selectedTask.name}</h4>
            <p className="text-gray-400 font-mono mb-4">{selectedTask.description}</p>
            <div className="space-y-4">
              <input
                type="text"
                value={flag}
                onChange={(e) => setFlag(e.target.value)}
                placeholder="Enter flag (e.g., FLAG{flag})"
                className="w-full bg-gray-900 border border-green-500 text-green-400 rounded px-4 py-2 font-mono focus:outline-none focus:ring-2 focus:ring-green-500"
              />
              {submitStatus && (
                <div className={`flex items-center ${
                  submitStatus === 'success' ? 'text-green-500' : 'text-red-500'
                }`}>
                  {submitStatus === 'success' ? (
                    <CheckCircleIcon className="h-5 w-5 mr-2" />
                  ) : (
                    <XCircleIcon className="h-5 w-5 mr-2" />
                  )}
                  <span className="font-mono">
                    {submitStatus === 'success' ? 'Correct flag!' : 'Invalid flag, try again!'}
                  </span>
                </div>
              )}
              <div className="flex justify-end space-x-4">
                <button
                  onClick={() => setSelectedTask(null)}
                  className="px-4 py-2 font-mono text-gray-400 hover:text-white"
                >
                  Cancel
                </button>
                <button
                  onClick={() => handleFlagSubmit(selectedTask.challengeId)}
                  className="px-4 py-2 bg-green-500 text-black rounded font-mono hover:bg-green-400"
                >
                  Submit Flag
                </button>
              </div>
            </div>
          </div>
        </div>
      )}
    </div>
  );
}

export default ChallengesDetails;