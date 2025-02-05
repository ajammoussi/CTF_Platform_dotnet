import React, { useState, useEffect } from "react";
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
  Bars3Icon,
} from "@heroicons/react/24/outline";
import { useNavigate } from "react-router-dom";

function AdminDashboard() {
  const [activeTab, setActiveTab] = useState("users");
  const [isSidebarOpen, setIsSidebarOpen] = useState(true);
  const navigate = useNavigate();

  const [stats, setStats] = useState([]);
  const [users, setUsers] = useState([]);
  const [teams, setTeams] = useState([]);
  const [teamScoreboard, setTeamScoreboard] = useState([]);
  const [userScoreboard, setUserScoreboard] = useState([]);
  const [challenges, setChallenges] = useState([]);
  const [submissions, setSubmissions] = useState([]);

  const API_BASE_URL = "https://localhost:7226/api/admin";

  const fetchData = async (endpoint, pageNumber = 1, pageSize = 10) => {
    try {
      const token = localStorage.getItem('token');
      const response = await fetch(`${API_BASE_URL}/${endpoint}?pageNumber=${pageNumber}&pageSize=${pageSize}`, {
        method: 'GET',
        headers: {
          'Authorization': `Bearer ${token}`,
          'Content-Type': 'application/json'
        }
      });

      if (!response.ok) {
        throw new Error(`HTTP error! status: ${response.status}`);
      }

      const data = await response.json();
      return data;
    } catch (error) {
      console.error(`Error fetching ${endpoint}:`, error);
      return null;
    }
  };

  useEffect(() => {
    const loadDashboardData = async () => {
      // Fetch dashboard stats
      const statsData = await fetchData('dashboard');
      if (statsData) {
        setStats(statsData);
      }

      // Fetch data based on active tab
      let data;
      switch (activeTab) {
        case 'users':
          data = await fetchData('users');
          if (data?.items?.$values) setUsers(data.items.$values);
          break;
        case 'teams':
          data = await fetchData('teams');
          if (data?.items?.$values) setTeams(data.items.$values);
          break;
        case 'teamScoreboard':
          data = await fetchData('scoreboard/teams');
          if (data?.items?.$values) setTeamScoreboard(data.items.$values);
          break;
        case 'userScoreboard':
          data = await fetchData('scoreboard/users');
          if (data?.items?.$values) setUserScoreboard(data.items.$values);
          break;
        case 'challenges':
          data = await fetchData('challenges');
          if (data?.items?.$values) setChallenges(data.items.$values);
          break;
        case 'submissions':
          data = await fetchData('submissions');
          if (data?.items?.$values) setSubmissions(data.items.$values);
          break;
      }
    };

    loadDashboardData();
  }, [activeTab]);

  const getStatsCards = (data) => {
    if (!data) return [];
    return [
      {
        title: "Total Users",
        value: data.totalUsers || 0,
        icon: UserGroupIcon,
      },
      {
        title: "Total Teams",
        value: data.totalTeams || 0,
        icon: UsersIcon,
      },
      {
        title: "Total Challenges",
        value: data.totalChallenges || 0,
        icon: FlagIcon,
      },
      {
        title: "Total Submissions",
        value: data.totalSubmissions || 0,
        icon: DocumentCheckIcon,
      },
      {
        title: "Most Solved",
        value: data.mostSolvedChallenge?.challengeName || "None",
        subValue: data.mostSolvedChallenge
          ? `${data.mostSolvedChallenge.count} solves`
          : "0 solves",
        icon: TrophyIcon,
      },
      {
        title: "Least Solved",
        value: data.leastSolvedChallenge?.challengeName || "None",
        subValue: data.leastSolvedChallenge
          ? `${data.leastSolvedChallenge.count} solve`
          : "0 solves",
        icon: XCircleIcon,
      },
    ];
  };

  const TabButton = ({ name, label, icon: Icon }) => (
    <button
      onClick={() => setActiveTab(name)}
      className={`flex items-center w-full px-4 py-3 rounded-lg font-mono transition-colors duration-200 ${
        activeTab === name
          ? "bg-red-500 text-white"
          : "text-red-500 hover:bg-gray-700"
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
              onClick={() => navigate("/")}
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
        <aside
          className={`fixed left-0 top-16 h-[calc(100vh-4rem)] bg-gray-800 border-r border-red-500 transition-all duration-300 z-40 ${
            isSidebarOpen ? "w-64" : "w-20"
          }`}
        >
          <div className="p-4 space-y-2">
            {[
              {
                name: "users",
                label: isSidebarOpen ? "> Users" : "",
                icon: UserGroupIcon,
              },
              {
                name: "teams",
                label: isSidebarOpen ? "> Teams" : "",
                icon: UsersIcon,
              },
              {
                name: "teamScoreboard",
                label: isSidebarOpen ? "> Team Scores" : "",
                icon: TrophyIcon,
              },
              {
                name: "userScoreboard",
                label: isSidebarOpen ? "> User Scores" : "",
                icon: ChartBarIcon,
              },
              {
                name: "challenges",
                label: isSidebarOpen ? "> Challenges" : "",
                icon: FlagIcon,
              },
              {
                name: "submissions",
                label: isSidebarOpen ? "> Submissions" : "",
                icon: DocumentCheckIcon,
              },
            ].map((item) => (
              <TabButton
                key={item.name}
                name={item.name}
                label={item.label}
                icon={item.icon}
              />
            ))}
          </div>
        </aside>

        {/* Main Content Area */}
        <main
          className={`flex-1 transition-all duration-300 ${
            isSidebarOpen ? "ml-64" : "ml-20"
          }`}
        >
          <div className="p-8">
            {/* Stats Grid */}
            <div className="grid grid-cols-1 md:grid-cols-2 lg:grid-cols-4 gap-6 mb-8">
              {getStatsCards(stats).map((stat, index) => (
                <div
                  key={index}
                  className="bg-gray-800 rounded-lg border border-red-500 p-6 hover:shadow-[0_0_10px_rgba(239,68,68,0.3)] transition-all duration-300"
                >
                  <div className="flex items-center">
                    <stat.icon className="h-8 w-8 text-red-500 mr-3" />
                    <div>
                      <p className="text-sm font-mono text-gray-400">
                        {stat.title}
                      </p>
                      <p className="text-2xl font-bold text-red-500">
                        {stat.value}
                      </p>
                      {stat.subValue && (
                        <p className="text-sm font-mono text-gray-400 mt-1">
                          {stat.subValue}
                        </p>
                      )}
                    </div>
                  </div>
                </div>
              ))}
            </div>

            {/* Content Area */}
            <div className="bg-gray-800 rounded-lg border border-red-500 p-6">
              {activeTab === "teams" && (
                <div className="bg-gray-800 rounded-lg border border-red-500 overflow-hidden">
                  <div className="p-6">
                    <h3 className="text-2xl font-mono text-red-400 mb-6">{`> Team_Management`}</h3>
                    <div className="overflow-x-auto">
                      <table className="min-w-full">
                        <thead>
                          <tr className="border-b border-red-500">
                            <th className="px-6 py-3 text-left text-xs font-mono text-red-400">
                              ID
                            </th>
                            <th className="px-6 py-3 text-left text-xs font-mono text-red-400">
                              Team Name
                            </th>
                            <th className="px-6 py-3 text-left text-xs font-mono text-red-400">
                              Created At
                            </th>
                            <th className="px-6 py-3 text-left text-xs font-mono text-red-400">
                              Total Points
                            </th>
                            <th className="px-6 py-3 text-left text-xs font-mono text-red-400">
                              Actions
                            </th>
                          </tr>
                        </thead>
                        <tbody>
                          {teams?.length > 0 ? (
                            teams.map((team) => (
                              <tr
                                key={team.teamId}
                                className="border-b border-gray-700"
                              >
                                <td className="px-6 py-4 whitespace-nowrap font-mono text-gray-300">
                                  #{team.teamId}
                                </td>
                                <td className="px-6 py-4 whitespace-nowrap font-mono text-white">
                                  {team.teamName}
                                </td>
                                <td className="px-6 py-4 whitespace-nowrap font-mono text-gray-300">
                                  {new Date(team.createdAt).toLocaleDateString()}
                                </td>
                                <td className="px-6 py-4 whitespace-nowrap font-mono text-red-400">
                                  {team.totalPoints.toLocaleString()}
                                </td>
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
                            ))
                          ) : (
                            <tr>
                              <td
                                colSpan="5"
                                className="px-6 py-4 text-center font-mono text-gray-400"
                              >
                                No teams found
                              </td>
                            </tr>
                          )}
                        </tbody>
                      </table>
                    </div>
                  </div>
                </div>
              )}

              {activeTab === "teamScoreboard" && (
                <div className="bg-gray-800 rounded-lg border border-red-500 overflow-hidden">
                  <div className="p-6">
                    <h3 className="text-2xl font-mono text-red-400 mb-6">{`> Team_Scoreboard`}</h3>
                    <div className="overflow-x-auto">
                      <table className="min-w-full">
                        <thead>
                          <tr className="border-b border-red-500">
                            <th className="px-6 py-3 text-left text-xs font-mono text-red-400">
                              Rank
                            </th>
                            <th className="px-6 py-3 text-left text-xs font-mono text-red-400">
                              Team Name
                            </th>
                            <th className="px-6 py-3 text-left text-xs font-mono text-red-400">
                              Points
                            </th>
                            <th className="px-6 py-3 text-left text-xs font-mono text-red-400">
                              Solved
                            </th>
                          </tr>
                        </thead>
                        <tbody>
                          {teamScoreboard?.length > 0 ? (
                            teamScoreboard.map((team, index) => (
                              <tr
                                key={index}
                                className="border-b border-gray-700"
                              >
                                <td className="px-6 py-4 whitespace-nowrap">
                                  <div className="flex items-center">
                                    {index <= 2 && (
                                      <TrophyIcon
                                        className={`h-5 w-5 mr-2 ${
                                          index === 0
                                            ? "text-yellow-400"
                                            : index === 1
                                            ? "text-gray-400"
                                            : "text-yellow-700"
                                        }`}
                                      />
                                    )}
                                    <span className="font-mono text-red-400">
                                      #{index + 1}
                                    </span>
                                  </div>
                                </td>
                                <td className="px-6 py-4 whitespace-nowrap font-mono text-white">
                                  {team.teamName}
                                </td>
                                <td className="px-6 py-4 whitespace-nowrap font-mono text-red-400">
                                  {team.totalPoints}
                                </td>
                                <td className="px-6 py-4 whitespace-nowrap font-mono text-gray-300">
                                  {team.totalSolves}
                                </td>
                              </tr>
                            ))
                          ) : (
                            <tr>
                              <td
                                colSpan="5"
                                className="px-6 py-4 text-center font-mono text-gray-400"
                              >
                                No teams found
                              </td>
                            </tr>
                          )}
                        </tbody>
                      </table>
                    </div>
                  </div>
                </div>
              )}

              {activeTab === "userScoreboard" && (
                <div className="bg-gray-800 rounded-lg border border-red-500 overflow-hidden">
                  <div className="p-6">
                    <h3 className="text-2xl font-mono text-red-400 mb-6">{`> User_Scoreboard`}</h3>
                    <div className="overflow-x-auto">
                      <table className="min-w-full">
                        <thead>
                          <tr className="border-b border-red-500">
                            <th className="px-6 py-3 text-left text-xs font-mono text-red-400">
                              Rank
                            </th>
                            <th className="px-6 py-3 text-left text-xs font-mono text-red-400">
                              Username
                            </th>
                            <th className="px-6 py-3 text-left text-xs font-mono text-red-400">
                              Points
                            </th>
                            <th className="px-6 py-3 text-left text-xs font-mono text-red-400">
                              Solved
                            </th>
                          </tr>
                        </thead>
                        <tbody>
                          {userScoreboard?.length > 0 ? (
                            userScoreboard.map((user, index) => (
                              <tr
                                key={index}
                                className="border-b border-gray-700"
                              >
                                <td className="px-6 py-4 whitespace-nowrap">
                                  <div className="flex items-center">
                                    {index <= 2 && (
                                      <TrophyIcon
                                        className={`h-5 w-5 mr-2 ${
                                          index === 0
                                            ? "text-yellow-400"
                                            : index === 1
                                            ? "text-gray-400"
                                            : "text-yellow-700"
                                        }`}
                                      />
                                    )}
                                    <span className="font-mono text-red-400">
                                      #{index + 1}
                                    </span>
                                  </div>
                                </td>
                                <td className="px-6 py-4 whitespace-nowrap font-mono text-white">
                                  {user.username}
                                </td>
                                <td className="px-6 py-4 whitespace-nowrap font-mono text-red-400">
                                  {user.points}
                                </td>
                                <td className="px-6 py-4 whitespace-nowrap font-mono text-gray-300">
                                  {user.totalSolves}
                                </td>
                              </tr>
                            ))
                          ) : (
                            <tr>
                              <td
                                colSpan="5"
                                className="px-6 py-4 text-center font-mono text-gray-400"
                              >
                                No users found
                              </td>
                            </tr>
                          )}
                        </tbody>
                      </table>
                    </div>
                  </div>
                </div>
              )}

              {activeTab === "users" && (
                <div className="bg-gray-800 rounded-lg border border-red-500 overflow-hidden">
                  <div className="p-6">
                    <h3 className="text-2xl font-mono text-red-400 mb-6">{`> User_Management`}</h3>
                    <div className="overflow-x-auto">
                      <table className="min-w-full">
                        <thead>
                          <tr className="border-b border-red-500">
                            <th className="px-6 py-3 text-left text-xs font-mono text-red-400">
                              Username
                            </th>
                            <th className="px-6 py-3 text-left text-xs font-mono text-red-400">
                              Email
                            </th>
                            <th className="px-6 py-3 text-left text-xs font-mono text-red-400">
                              Join Date
                            </th>
                            <th className="px-6 py-3 text-left text-xs font-mono text-red-400">
                              Solved
                            </th>
                          </tr>
                        </thead>
                        <tbody>
                          {users?.length > 0 ? (
                            users.map((user) => (
                              <tr
                                key={user.userId}
                                className="border-b border-gray-700"
                              >
                                <td className="px-6 py-4 whitespace-nowrap font-mono text-white">
                                  {user.username}
                                </td>
                                <td className="px-6 py-4 whitespace-nowrap font-mono text-gray-300">
                                  {user.email}
                                </td>
                                <td className="px-6 py-4 whitespace-nowrap font-mono text-gray-300">
                                  {new Date(user.createdAt).toLocaleDateString()}
                                </td>
                                <td className="px-6 py-4 whitespace-nowrap font-mono text-red-400">
                                  {user.totalSolves}
                                </td>
                              </tr>
                            ))
                          ) : (
                            <tr>
                              <td
                                colSpan="5"
                                className="px-6 py-4 text-center font-mono text-gray-400"
                              >
                                No users found
                              </td>
                            </tr>
                          )}
                        </tbody>
                      </table>
                    </div>
                  </div>
                </div>
              )}

              {activeTab === "challenges" && (
                <div className="bg-gray-800 rounded-lg border border-red-500 overflow-hidden">
                  <div className="p-6">
                    <h3 className="text-2xl font-mono text-red-400 mb-6">{`> Challenge_Management`}</h3>
                    <div className="overflow-x-auto">
                      <table className="min-w-full">
                        <thead>
                          <tr className="border-b border-red-500">
                            <th className="px-6 py-3 text-left text-xs font-mono text-red-400">
                              Title
                            </th>
                            <th className="px-6 py-3 text-left text-xs font-mono text-red-400">
                              Category
                            </th>
                            <th className="px-6 py-3 text-left text-xs font-mono text-red-400">
                              Difficulty
                            </th>
                          </tr>
                        </thead>
                        <tbody>
                          {challenges?.length > 0 ? (
                            challenges.map((challenge) => (
                              <tr
                                key={challenge.challengeId}
                                className="border-b border-gray-700"
                              >
                                <td className="px-6 py-4 whitespace-nowrap font-mono text-white">
                                  {challenge.name}
                                </td>
                                <td className="px-6 py-4 whitespace-nowrap font-mono text-gray-300">
                                  {challenge.category}
                                </td>
                                <td className="px-6 py-4 whitespace-nowrap">
                                  <span
                                    className={`px-2 py-1 text-xs font-mono rounded-full ${
                                      challenge.difficulty === "Easy"
                                        ? "bg-green-500 text-white"
                                        : challenge.difficulty === "Medium"
                                        ? "bg-yellow-500 text-white"
                                        : "bg-red-500 text-white"
                                    }`}
                                  >
                                    {challenge.difficulty}
                                  </span>
                                </td>
                              </tr>
                            ))
                          ) : (
                            <tr>
                              <td
                                colSpan="5"
                                className="px-6 py-4 text-center font-mono text-gray-400"
                              >
                                No challenges found
                              </td>
                            </tr>
                          )}
                        </tbody>
                      </table>
                    </div>
                  </div>
                </div>
              )}

              {activeTab === "submissions" && (
                <div className="bg-gray-800 rounded-lg border border-red-500 overflow-hidden">
                  <div className="p-6">
                    <h3 className="text-2xl font-mono text-red-400 mb-6">{`> Submission_History`}</h3>
                    <div className="overflow-x-auto">
                      <table className="min-w-full">
                        <thead>
                          <tr className="border-b border-red-500">
                            <th className="px-6 py-3 text-left text-xs font-mono text-red-400">
                              Username
                            </th>
                            <th className="px-6 py-3 text-left text-xs font-mono text-red-400">
                              Challenge
                            </th>
                            <th className="px-6 py-3 text-left text-xs font-mono text-red-400">
                              Status
                            </th>
                            <th className="px-6 py-3 text-left text-xs font-mono text-red-400">
                              Timestamp
                            </th>
                          </tr>
                        </thead>
                        <tbody>
                          {submissions.map((submission) => (
                            <tr
                              key={submission.id}
                              className="border-b border-gray-700"
                            >
                              <td className="px-6 py-4 whitespace-nowrap font-mono text-white">
                                {submission.username}
                              </td>
                              <td className="px-6 py-4 whitespace-nowrap font-mono text-gray-300">
                                {submission.challenge}
                              </td>
                              <td className="px-6 py-4 whitespace-nowrap">
                                <span
                                  className={`flex items-center ${
                                    submission.status === "correct"
                                      ? "text-green-500"
                                      : "text-red-500"
                                  }`}
                                >
                                  {submission.status === "correct" ? (
                                    <CheckCircleIcon className="h-5 w-5 mr-1" />
                                  ) : (
                                    <XCircleIcon className="h-5 w-5 mr-1" />
                                  )}
                                  {submission.status}
                                </span>
                              </td>
                              <td className="px-6 py-4 whitespace-nowrap font-mono text-gray-300">
                                {submission.timestamp}
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