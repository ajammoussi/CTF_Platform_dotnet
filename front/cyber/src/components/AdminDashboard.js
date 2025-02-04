import React, { useState } from 'react';
import { 
  UserGroupIcon, 
  FlagIcon, 
  DocumentCheckIcon, 
  ChartBarIcon,
  TrashIcon,
  PencilSquareIcon,
  NoSymbolIcon,
  CheckCircleIcon,
  XCircleIcon,
  UsersIcon,
  TrophyIcon,
  RocketLaunchIcon,
  FireIcon,
  ArrowLeftIcon,
  Bars3Icon
} from '@heroicons/react/24/outline';
import { useNavigate } from 'react-router-dom';

function AdminDashboard() {
  const [activeTab, setActiveTab] = useState('users');
  const [isSidebarOpen, setIsSidebarOpen] = useState(true);
  const navigate = useNavigate();

  const users = [
    { id: 1, username: "h4ck3r_supreme", email: "hacker@example.com", status: "active", joinDate: "2023-07-01", solvedChallenges: 15 },
    { id: 2, username: "cyber_ninja", email: "ninja@example.com", status: "banned", joinDate: "2023-07-02", solvedChallenges: 10 },
    { id: 3, username: "binary_beast", email: "beast@example.com", status: "active", joinDate: "2023-07-03", solvedChallenges: 8 }
  ];

  const teams = [
    { id: 1, name: "Hack Masters", members: 4, totalPoints: 2500, activeEvents: 3, rank: 1 },
    { id: 2, name: "Binary Bandits", members: 3, totalPoints: 2200, activeEvents: 2, rank: 2 },
    { id: 3, name: "Cyber Knights", members: 5, totalPoints: 1800, activeEvents: 3, rank: 3 }
  ];

  const teamScoreboard = [
    { rank: 1, name: "Hack Masters", points: 2500, solvedChallenges: 25, lastActive: "2023-07-15" },
    { rank: 2, name: "Binary Bandits", points: 2200, solvedChallenges: 22, lastActive: "2023-07-14" },
    { rank: 3, name: "Cyber Knights", points: 1800, solvedChallenges: 18, lastActive: "2023-07-15" }
  ];

  const userScoreboard = [
    { rank: 1, username: "h4ck3r_supreme", points: 1200, solvedChallenges: 15, team: "Hack Masters" },
    { rank: 2, username: "cyber_ninja", points: 1000, solvedChallenges: 12, team: "Binary Bandits" },
    { rank: 3, username: "binary_beast", points: 800, solvedChallenges: 10, team: "Cyber Knights" }
  ];

  const challenges = [
    { id: 1, title: "Web Injection 101", category: "Web", difficulty: "Easy", participants: 150, successRate: "45%" },
    { id: 2, title: "Advanced Cryptography", category: "Crypto", difficulty: "Hard", participants: 75, successRate: "15%" },
    { id: 3, title: "Network Forensics", category: "Network", difficulty: "Medium", participants: 100, successRate: "30%" }
  ];

  const submissions = [
    { id: 1, username: "h4ck3r_supreme", challenge: "Web Injection 101", status: "correct", timestamp: "2023-07-15 14:30" },
    { id: 2, username: "cyber_ninja", challenge: "Advanced Cryptography", status: "incorrect", timestamp: "2023-07-15 15:45" },
    { id: 3, username: "binary_beast", challenge: "Network Forensics", status: "correct", timestamp: "2023-07-15 16:20" }
  ];

  const stats = [
    { title: "Total Users", value: "1,234", icon: UserGroupIcon },
    { title: "Active Teams", value: "156", icon: UsersIcon },
    { title: "Active Challenges", value: "25", icon: FlagIcon },
    { title: "Total Points", value: "25.5K", icon: TrophyIcon },
    { title: "Today's Submissions", value: "156", icon: DocumentCheckIcon },
    { title: "Success Rate", value: "32%", icon: ChartBarIcon },
    { title: "Active Events", value: "5", icon: RocketLaunchIcon },
    { title: "Trending Challenge", value: "Web", icon: FireIcon }
  ];

  const TabButton = ({ name, label, icon: Icon }) => (
    <button
      onClick={() => setActiveTab(name)}
      className={`flex items-center w-full px-4 py-3 rounded-lg font-mono transition-colors duration-200 ${
        activeTab === name
          ? 'bg-red-500 text-white'
          : 'text-red-500 hover:bg-gray-700'
      }`}
    >
      <Icon className="h-5 w-5 mr-3" />
      {label}
    </button>
  );

  return (
    <div className="min-h-screen bg-gray-900 flex flex-col">
      {/* Top Navigation */}
      <header className="bg-gray-800 border-b border-red-500 h-16 fixed w-full top-0 z-50">
        <div className="h-full px-4 flex items-center justify-between">
          <div className="flex items-center">
            <button 
              onClick={() => setIsSidebarOpen(!isSidebarOpen)}
              className="p-2 rounded-lg text-red-500 hover:bg-gray-700 mr-4"
            >
              <Bars3Icon className="h-6 w-6" />
            </button>
            <button 
              onClick={() => navigate('/')}
              className="flex items-center text-red-500 hover:text-red-400 mr-4"
            >
              <ArrowLeftIcon className="h-5 w-5 mr-2" />
              <span className="font-mono">Back to Site</span>
            </button>
            <h1 className="text-2xl font-bold text-red-500">Admin Dashboard</h1>
          </div>
          <div className="flex items-center space-x-4">
            <span className="text-gray-300 font-mono">Admin User</span>
            <button className="bg-red-500 text-white px-4 py-2 rounded-lg hover:bg-red-600 font-mono">
              Logout
            </button>
          </div>
        </div>
      </header>

      {/* Main Content */}
      <div className="flex flex-1 pt-16">
        {/* Sidebar */}
        <aside className={`fixed left-0 top-16 h-[calc(100vh-4rem)] bg-gray-800 border-r border-red-500 transition-all duration-300 z-40 ${
          isSidebarOpen ? 'w-64' : 'w-20'
        }`}>
          <div className="p-4 space-y-2">
            {[
              { name: 'users', label: isSidebarOpen ? '> Users' : '', icon: UserGroupIcon },
              { name: 'teams', label: isSidebarOpen ? '> Teams' : '', icon: UsersIcon },
              { name: 'teamScoreboard', label: isSidebarOpen ? '> Team Scores' : '', icon: TrophyIcon },
              { name: 'userScoreboard', label: isSidebarOpen ? '> User Scores' : '', icon: ChartBarIcon },
              { name: 'challenges', label: isSidebarOpen ? '> Challenges' : '', icon: FlagIcon },
              { name: 'submissions', label: isSidebarOpen ? '> Submissions' : '', icon: DocumentCheckIcon }
            ].map((item) => (
              <TabButton key={item.name} name={item.name} label={item.label} icon={item.icon} />
            ))}
          </div>
        </aside>

        {/* Main Content Area */}
        <main className={`flex-1 transition-all duration-300 ${
          isSidebarOpen ? 'ml-64' : 'ml-20'
        }`}>
          <div className="p-8">
            {/* Stats Grid */}
            <div className="grid grid-cols-1 md:grid-cols-2 lg:grid-cols-4 gap-6 mb-8">
              {stats.map((stat, index) => (
                <div key={index} className="bg-gray-800 rounded-lg border border-red-500 p-6 hover:shadow-[0_0_10px_rgba(239,68,68,0.3)] transition-all duration-300">
                  <div className="flex items-center">
                    <stat.icon className="h-8 w-8 text-red-500 mr-3" />
                    <div>
                      <p className="text-sm font-mono text-gray-400">{stat.title}</p>
                      <p className="text-2xl font-bold text-red-500">{stat.value}</p>
                    </div>
                  </div>
                </div>
              ))}
            </div>

            {/* Content Area */}
            <div className="bg-gray-800 rounded-lg border border-red-500 p-6">
              {activeTab === 'teams' && (
                <div className="bg-gray-800 rounded-lg border border-red-500 overflow-hidden">
                  <div className="p-6">
                    <h3 className="text-2xl font-mono text-red-400 mb-6">{`> Team_Management`}</h3>
                    <div className="overflow-x-auto">
                      <table className="min-w-full">
                        <thead>
                          <tr className="border-b border-red-500">
                            <th className="px-6 py-3 text-left text-xs font-mono text-red-400">Team Name</th>
                            <th className="px-6 py-3 text-left text-xs font-mono text-red-400">Members</th>
                            <th className="px-6 py-3 text-left text-xs font-mono text-red-400">Total Points</th>
                            <th className="px-6 py-3 text-left text-xs font-mono text-red-400">Active Events</th>
                            <th className="px-6 py-3 text-left text-xs font-mono text-red-400">Rank</th>
                            <th className="px-6 py-3 text-left text-xs font-mono text-red-400">Actions</th>
                          </tr>
                        </thead>
                        <tbody>
                          {teams.map((team) => (
                            <tr key={team.id} className="border-b border-gray-700">
                              <td className="px-6 py-4 whitespace-nowrap font-mono text-white">{team.name}</td>
                              <td className="px-6 py-4 whitespace-nowrap font-mono text-gray-300">{team.members}</td>
                              <td className="px-6 py-4 whitespace-nowrap font-mono text-red-400">{team.totalPoints}</td>
                              <td className="px-6 py-4 whitespace-nowrap font-mono text-gray-300">{team.activeEvents}</td>
                              <td className="px-6 py-4 whitespace-nowrap font-mono text-red-400">#{team.rank}</td>
                              <td className="px-6 py-4 whitespace-nowrap">
                                <div className="flex space-x-2">
                                  <button className="text-yellow-500 hover:text-yellow-400">
                                    <PencilSquareIcon className="h-5 w-5" />
                                  </button>
                                  <button className="text-red-500 hover:text-red-400">
                                    <TrashIcon className="h-5 w-5" />
                                  </button>
                                </div>
                              </td>
                            </tr>
                          ))}
                        </tbody>
                      </table>
                    </div>
                  </div>
                </div>
              )}

              {activeTab === 'teamScoreboard' && (
                <div className="bg-gray-800 rounded-lg border border-red-500 overflow-hidden">
                  <div className="p-6">
                    <h3 className="text-2xl font-mono text-red-400 mb-6">{`> Team_Scoreboard`}</h3>
                    <div className="overflow-x-auto">
                      <table className="min-w-full">
                        <thead>
                          <tr className="border-b border-red-500">
                            <th className="px-6 py-3 text-left text-xs font-mono text-red-400">Rank</th>
                            <th className="px-6 py-3 text-left text-xs font-mono text-red-400">Team Name</th>
                            <th className="px-6 py-3 text-left text-xs font-mono text-red-400">Points</th>
                            <th className="px-6 py-3 text-left text-xs font-mono text-red-400">Solved</th>
                            <th className="px-6 py-3 text-left text-xs font-mono text-red-400">Last Active</th>
                          </tr>
                        </thead>
                        <tbody>
                          {teamScoreboard.map((team) => (
                            <tr key={team.rank} className="border-b border-gray-700">
                              <td className="px-6 py-4 whitespace-nowrap">
                                <div className="flex items-center">
                                  {team.rank <= 3 && (
                                    <TrophyIcon 
                                      className={`h-5 w-5 mr-2 ${
                                        team.rank === 1 ? 'text-yellow-400' :
                                        team.rank === 2 ? 'text-gray-400' :
                                        'text-yellow-700'
                                      }`}
                                    />
                                  )}
                                  <span className="font-mono text-red-400">#{team.rank}</span>
                                </div>
                              </td>
                              <td className="px-6 py-4 whitespace-nowrap font-mono text-white">{team.name}</td>
                              <td className="px-6 py-4 whitespace-nowrap font-mono text-red-400">{team.points}</td>
                              <td className="px-6 py-4 whitespace-nowrap font-mono text-gray-300">{team.solvedChallenges}</td>
                              <td className="px-6 py-4 whitespace-nowrap font-mono text-gray-300">{team.lastActive}</td>
                            </tr>
                          ))}
                        </tbody>
                      </table>
                    </div>
                  </div>
                </div>
              )}

              {activeTab === 'userScoreboard' && (
                <div className="bg-gray-800 rounded-lg border border-red-500 overflow-hidden">
                  <div className="p-6">
                    <h3 className="text-2xl font-mono text-red-400 mb-6">{`> User_Scoreboard`}</h3>
                    <div className="overflow-x-auto">
                      <table className="min-w-full">
                        <thead>
                          <tr className="border-b border-red-500">
                            <th className="px-6 py-3 text-left text-xs font-mono text-red-400">Rank</th>
                            <th className="px-6 py-3 text-left text-xs font-mono text-red-400">Username</th>
                            <th className="px-6 py-3 text-left text-xs font-mono text-red-400">Team</th>
                            <th className="px-6 py-3 text-left text-xs font-mono text-red-400">Points</th>
                            <th className="px-6 py-3 text-left text-xs font-mono text-red-400">Solved</th>
                          </tr>
                        </thead>
                        <tbody>
                          {userScoreboard.map((user) => (
                            <tr key={user.rank} className="border-b border-gray-700">
                              <td className="px-6 py-4 whitespace-nowrap">
                                <div className="flex items-center">
                                  {user.rank <= 3 && (
                                    <TrophyIcon 
                                      className={`h-5 w-5 mr-2 ${
                                        user.rank === 1 ? 'text-yellow-400' :
                                        user.rank === 2 ? 'text-gray-400' :
                                        'text-yellow-700'
                                      }`}
                                    />
                                  )}
                                  <span className="font-mono text-red-400">#{user.rank}</span>
                                </div>
                              </td>
                              <td className="px-6 py-4 whitespace-nowrap font-mono text-white">{user.username}</td>
                              <td className="px-6 py-4 whitespace-nowrap font-mono text-gray-300">{user.team}</td>
                              <td className="px-6 py-4 whitespace-nowrap font-mono text-red-400">{user.points}</td>
                              <td className="px-6 py-4 whitespace-nowrap font-mono text-gray-300">{user.solvedChallenges}</td>
                            </tr>
                          ))}
                        </tbody>
                      </table>
                    </div>
                  </div>
                </div>
              )}

              {activeTab === 'users' && (
                <div className="bg-gray-800 rounded-lg border border-red-500 overflow-hidden">
                  <div className="p-6">
                    <h3 className="text-2xl font-mono text-red-400 mb-6">{`> User_Management`}</h3>
                    <div className="overflow-x-auto">
                      <table className="min-w-full">
                        <thead>
                          <tr className="border-b border-red-500">
                            <th className="px-6 py-3 text-left text-xs font-mono text-red-400">Username</th>
                            <th className="px-6 py-3 text-left text-xs font-mono text-red-400">Email</th>
                            <th className="px-6 py-3 text-left text-xs font-mono text-red-400">Status</th>
                            <th className="px-6 py-3 text-left text-xs font-mono text-red-400">Join Date</th>
                            <th className="px-6 py-3 text-left text-xs font-mono text-red-400">Solved</th>
                            <th className="px-6 py-3 text-left text-xs font-mono text-red-400">Actions</th>
                          </tr>
                        </thead>
                        <tbody>
                          {users.map((user) => (
                            <tr key={user.id} className="border-b border-gray-700">
                              <td className="px-6 py-4 whitespace-nowrap font-mono text-white">{user.username}</td>
                              <td className="px-6 py-4 whitespace-nowrap font-mono text-gray-300">{user.email}</td>
                              <td className="px-6 py-4 whitespace-nowrap">
                                <span className={`px-2 py-1 text-xs font-mono rounded-full ${
                                  user.status === 'active' ? 'bg-green-500 text-white' : 'bg-red-500 text-white'
                                }`}>
                                  {user.status}
                                </span>
                              </td>
                              <td className="px-6 py-4 whitespace-nowrap font-mono text-gray-300">{user.joinDate}</td>
                              <td className="px-6 py-4 whitespace-nowrap font-mono text-red-400">{user.solvedChallenges}</td>
                              <td className="px-6 py-4 whitespace-nowrap">
                                <div className="flex space-x-2">
                                  <button className="text-yellow-500 hover:text-yellow-400">
                                    <PencilSquareIcon className="h-5 w-5" />
                                  </button>
                                  <button className="text-red-500 hover:text-red-400">
                                    <NoSymbolIcon className="h-5 w-5" />
                                  </button>
                                  <button className="text-red-500 hover:text-red-400">
                                    <TrashIcon className="h-5 w-5" />
                                  </button>
                                </div>
                              </td>
                            </tr>
                          ))}
                        </tbody>
                      </table>
                    </div>
                  </div>
                </div>
              )}

              {activeTab === 'challenges' && (
                <div className="bg-gray-800 rounded-lg border border-red-500 overflow-hidden">
                  <div className="p-6">
                    <h3 className="text-2xl font-mono text-red-400 mb-6">{`> Challenge_Management`}</h3>
                    <div className="overflow-x-auto">
                      <table className="min-w-full">
                        <thead>
                          <tr className="border-b border-red-500">
                            <th className="px-6 py-3 text-left text-xs font-mono text-red-400">Title</th>
                            <th className="px-6 py-3 text-left text-xs font-mono text-red-400">Category</th>
                            <th className="px-6 py-3 text-left text-xs font-mono text-red-400">Difficulty</th>
                            <th className="px-6 py-3 text-left text-xs font-mono text-red-400">Participants</th>
                            <th className="px-6 py-3 text-left text-xs font-mono text-red-400">Success Rate</th>
                            <th className="px-6 py-3 text-left text-xs font-mono text-red-400">Actions</th>
                          </tr>
                        </thead>
                        <tbody>
                          {challenges.map((challenge) => (
                            <tr key={challenge.id} className="border-b border-gray-700">
                              <td className="px-6 py-4 whitespace-nowrap font-mono text-white">{challenge.title}</td>
                              <td className="px-6 py-4 whitespace-nowrap font-mono text-gray-300">{challenge.category}</td>
                              <td className="px-6 py-4 whitespace-nowrap">
                                <span className={`px-2 py-1 text-xs font-mono rounded-full ${
                                  challenge.difficulty === 'Easy' ? 'bg-green-500 text-white' :
                                  challenge.difficulty === 'Medium' ? 'bg-yellow-500 text-white' :
                                  'bg-red-500 text-white'
                                }`}>
                                  {challenge.difficulty}
                                </span>
                              </td>
                              <td className="px-6 py-4 whitespace-nowrap font-mono text-red-400">{challenge.participants}</td>
                              <td className="px-6 py-4 whitespace-nowrap font-mono text-red-400">{challenge.successRate}</td>
                              <td className="px-6 py-4 whitespace-nowrap">
                                <div className="flex space-x-2">
                                  <button className="text-yellow-500 hover:text-yellow-400">
                                    <PencilSquareIcon className="h-5 w-5" />
                                  </button>
                                  <button className="text-red-500 hover:text-red-400">
                                    <TrashIcon className="h-5 w-5" />
                                  </button>
                                </div>
                              </td>
                            </tr>
                          ))}
                        </tbody>
                      </table>
                    </div>
                  </div>
                </div>
              )}

              {activeTab === 'submissions' && (
                <div className="bg-gray-800 rounded-lg border border-red-500 overflow-hidden">
                  <div className="p-6">
                    <h3 className="text-2xl font-mono text-red-400 mb-6">{`> Submission_History`}</h3>
                    <div className="overflow-x-auto">
                      <table className="min-w-full">
                        <thead>
                          <tr className="border-b border-red-500">
                            <th className="px-6 py-3 text-left text-xs font-mono text-red-400">Username</th>
                            <th className="px-6 py-3 text-left text-xs font-mono text-red-400">Challenge</th>
                            <th className="px-6 py-3 text-left text-xs font-mono text-red-400">Status</th>
                            <th className="px-6 py-3 text-left text-xs font-mono text-red-400">Timestamp</th>
                            <th className="px-6 py-3 text-left text-xs font-mono text-red-400">Actions</th>
                          </tr>
                        </thead>
                        <tbody>
                          {submissions.map((submission) => (
                            <tr key={submission.id} className="border-b border-gray-700">
                              <td className="px-6 py-4 whitespace-nowrap font-mono text-white">{submission.username}</td>
                              <td className="px-6 py-4 whitespace-nowrap font-mono text-gray-300">{submission.challenge}</td>
                              <td className="px-6 py-4 whitespace-nowrap">
                                <span className={`flex items-center ${
                                  submission.status === 'correct' ? 'text-green-500' : 'text-red-500'
                                }`}>
                                  {submission.status === 'correct' ? (
                                    <CheckCircleIcon className="h-5 w-5 mr-1" />
                                  ) : (
                                    <XCircleIcon className="h-5 w-5 mr-1" />
                                  )}
                                  {submission.status}
                                </span>
                              </td>
                              <td className="px-6 py-4 whitespace-nowrap font-mono text-gray-300">{submission.timestamp}</td>
                              <td className="px-6 py-4 whitespace-nowrap">
                                <div className="flex space-x-2">
                                  <button className="text-yellow-500 hover:text-yellow-400">
                                    <PencilSquareIcon className="h-5 w-5" />
                                  </button>
                                  <button className="text-red-500 hover:text-red-400">
                                    <TrashIcon className="h-5 w-5" />
                                  </button>
                                </div>
                              </td>
                            </tr>
                          ))}
                        </tbody>
                      </table>
                    </div>
                  </div>
                </div>
              )}
            </div>
          </div>
        </main>
      </div>
    </div>
  );
}

export default AdminDashboard;