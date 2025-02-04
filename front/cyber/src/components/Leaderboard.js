import React from 'react';
import { TrophyIcon, SparklesIcon } from '@heroicons/react/24/outline';

const users = [
  { id: 1, username: "h4ck3r_supreme", points: 2500, solved: 15, rank: 1 },
  { id: 2, username: "cyber_ninja", points: 2200, solved: 13, rank: 2 },
  { id: 3, username: "binary_beast", points: 2100, solved: 12, rank: 3 },
  { id: 4, username: "code_phantom", points: 1800, solved: 11, rank: 4 },
  { id: 5, username: "debug_master", points: 1600, solved: 10, rank: 5 }
];

function Leaderboard() {
  return (
    <div className="max-w-7xl mx-auto py-12 px-4 sm:px-6 lg:px-8">
      <div className="text-center mb-12">
        <h2 className="text-4xl font-extrabold text-green-500 glitch-text">
          {'<Global_Leaderboard/>'}
        </h2>
        <p className="mt-3 max-w-2xl mx-auto text-xl text-green-400 font-mono">
          {'> Top hackers who dominate the challenges'}
        </p>
      </div>

      <div className="bg-gray-800 rounded-lg border border-green-500 overflow-hidden">
        <div className="p-6">
          <div className="overflow-x-auto">
            <table className="min-w-full">
              <thead>
                <tr className="border-b border-green-500">
                  <th className="px-6 py-3 text-left text-xs font-mono text-green-400">Rank</th>
                  <th className="px-6 py-3 text-left text-xs font-mono text-green-400">Username</th>
                  <th className="px-6 py-3 text-left text-xs font-mono text-green-400">Points</th>
                  <th className="px-6 py-3 text-left text-xs font-mono text-green-400">Challenges Solved</th>
                </tr>
              </thead>
              <tbody>
                {users.map((user) => (
                  <tr 
                    key={user.id} 
                    className="border-b border-gray-700 hover:bg-gray-700 transition-colors"
                  >
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
                        <span className="font-mono text-green-400">#{user.rank}</span>
                      </div>
                    </td>
                    <td className="px-6 py-4 whitespace-nowrap">
                      <span className="font-mono text-white">{user.username}</span>
                    </td>
                    <td className="px-6 py-4 whitespace-nowrap">
                      <div className="flex items-center">
                        <SparklesIcon className="h-5 w-5 text-green-500 mr-2" />
                        <span className="font-mono text-green-400">{user.points}</span>
                      </div>
                    </td>
                    <td className="px-6 py-4 whitespace-nowrap">
                      <span className="font-mono text-green-400">{user.solved}</span>
                    </td>
                  </tr>
                ))}
              </tbody>
            </table>
          </div>
        </div>
      </div>
    </div>
  );
}

export default Leaderboard;