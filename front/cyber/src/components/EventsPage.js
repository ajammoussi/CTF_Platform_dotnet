import React, { useState } from 'react';
import { Link } from 'react-router-dom';
import { CalendarIcon, ClockIcon, UsersIcon, AdjustmentsHorizontalIcon } from '@heroicons/react/24/outline';

const events = [
  {
    id: 1,
    title: "Web Security Challenge 2023",
    description: "Test your web application security skills with challenges ranging from XSS to SQL injection.",
    startDate: "2023-08-01",
    duration: "48 hours",
    difficulty: "Intermediate",
    participants: 156,
    prize: "$1,000",
    categories: ["Web", "API", "Authentication"],
    status: "upcoming"
  },
  {
    id: 2,
    title: "Cryptography Masters",
    description: "Break complex encryption schemes and solve challenging cryptographic puzzles.",
    startDate: "2023-08-15",
    duration: "24 hours",
    difficulty: "Advanced",
    participants: 89,
    prize: "$500",
    categories: ["Cryptography", "Mathematics"],
    status: "upcoming"
  },
  {
    id: 3,
    title: "Network Security Challenge",
    description: "Analyze network traffic, exploit vulnerabilities, and secure systems.",
    startDate: "2023-07-15",
    duration: "36 hours",
    difficulty: "Beginner",
    participants: 234,
    prize: "$750",
    categories: ["Network", "Forensics"],
    status: "ongoing"
  }
];

function EventsPage() {
  const [selectedDifficulty, setSelectedDifficulty] = useState('all');
  const [selectedDate, setSelectedDate] = useState('all');
  const [isFilterOpen, setIsFilterOpen] = useState(false);

  const difficulties = ['all', 'Beginner', 'Intermediate', 'Advanced'];
  const dateFilters = ['all', 'upcoming', 'ongoing'];

  const filteredEvents = events.filter(event => {
    const difficultyMatch = selectedDifficulty === 'all' || event.difficulty === selectedDifficulty;
    const dateMatch = selectedDate === 'all' || event.status === selectedDate;
    return difficultyMatch && dateMatch;
  });

  return (
    <div className="max-w-7xl mx-auto py-12 px-4 sm:px-6 lg:px-8">
      <div className="text-center">
        <h2 className="text-3xl font-extrabold text-white sm:text-4xl glitch-text">
          {'<Events_Directory/>'}
        </h2>
        <p className="mt-3 max-w-2xl mx-auto text-xl text-gray-300 sm:mt-4 font-mono">
          {'> Browse_Available_Challenges()'}
        </p>
      </div>

      {/* Filters Section */}
      <div className="mt-8">
        <button
          onClick={() => setIsFilterOpen(!isFilterOpen)}
          className="flex items-center space-x-2 bg-gray-800 text-green-500 px-4 py-2 rounded-lg border border-green-500 hover:bg-gray-700 transition-colors duration-300 font-mono"
        >
          <AdjustmentsHorizontalIcon className="h-5 w-5" />
          <span>{'> Configure_Filters'}</span>
        </button>

        {isFilterOpen && (
          <div className="mt-4 bg-gray-800 p-6 rounded-lg border border-green-500 shadow-[0_0_10px_rgba(34,197,94,0.3)]">
            <div className="grid grid-cols-1 md:grid-cols-2 gap-6">
              <div>
                <label className="block text-green-500 font-mono mb-2">{'> Select_Difficulty'}</label>
                <div className="space-y-2">
                  {difficulties.map(difficulty => (
                    <button
                      key={difficulty}
                      onClick={() => setSelectedDifficulty(difficulty)}
                      className={`w-full text-left px-4 py-2 rounded font-mono ${
                        selectedDifficulty === difficulty
                          ? 'bg-green-500 text-black'
                          : 'bg-gray-700 text-green-500 hover:bg-gray-600'
                      }`}
                    >
                      {`> ${difficulty === 'all' ? 'All_Difficulties' : difficulty}`}
                    </button>
                  ))}
                </div>
              </div>

              <div>
                <label className="block text-green-500 font-mono mb-2">{'> Select_Status'}</label>
                <div className="space-y-2">
                  {dateFilters.map(filter => (
                    <button
                      key={filter}
                      onClick={() => setSelectedDate(filter)}
                      className={`w-full text-left px-4 py-2 rounded font-mono ${
                        selectedDate === filter
                          ? 'bg-green-500 text-black'
                          : 'bg-gray-700 text-green-500 hover:bg-gray-600'
                      }`}
                    >
                      {`> ${filter === 'all' ? 'All_Events' : filter.charAt(0).toUpperCase() + filter.slice(1)}`}
                    </button>
                  ))}
                </div>
              </div>
            </div>
          </div>
        )}
      </div>

      <div className="mt-12 grid gap-8 md:grid-cols-2 lg:grid-cols-3">
        {filteredEvents.map((event) => (
          <div
            key={event.id}
            className="flex flex-col bg-gray-800 rounded-lg shadow-lg overflow-hidden hover:shadow-[0_0_15px_rgba(34,197,94,0.3)] transition-all duration-300"
          >
            <div className="px-6 py-8">
              <div className="flex items-center">
                <span className={`px-2 py-1 text-xs font-semibold rounded-full ${
                  event.status === 'ongoing' ? 'bg-green-500 text-white' : 'bg-blue-500 text-white'
                }`}>
                  {event.status.toUpperCase()}
                </span>
                <span className="ml-2 text-sm text-gray-400">{event.difficulty}</span>
              </div>
              
              <h3 className="mt-4 text-2xl font-bold text-white">{event.title}</h3>
              
              <p className="mt-3 text-gray-300">{event.description}</p>
              
              <div className="mt-6 grid grid-cols-2 gap-4">
                <div className="flex items-center text-gray-300">
                  <CalendarIcon className="h-5 w-5 mr-2" />
                  <span>{event.startDate}</span>
                </div>
                <div className="flex items-center text-gray-300">
                  <ClockIcon className="h-5 w-5 mr-2" />
                  <span>{event.duration}</span>
                </div>
              </div>
              
              <div className="mt-4 flex items-center text-gray-300">
                <UsersIcon className="h-5 w-5 mr-2" />
                <span>{event.participants} participants</span>
              </div>

              <div className="mt-6">
                <div className="text-sm text-gray-400">Prize Pool</div>
                <div className="text-2xl font-bold text-green-500">{event.prize}</div>
              </div>

              <div className="mt-4 flex flex-wrap gap-2">
                {event.categories.map((category) => (
                  <span
                    key={category}
                    className="px-2 py-1 text-xs font-medium bg-gray-700 text-gray-300 rounded"
                  >
                    {category}
                  </span>
                ))}
              </div>
            </div>

            <div className="px-6 py-4 bg-gray-700 mt-auto">
              <Link
                to={`/challenge/${event.id}`}
                className="w-full bg-green-500 text-black py-2 px-4 rounded font-mono hover:bg-green-400 transition-colors duration-300 block text-center"
              >
                {'> Join_Challenge()'}
              </Link>
            </div>
          </div>
        ))}
      </div>
    </div>
  );
}

export default EventsPage;