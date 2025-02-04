import React from 'react';
import { InformationCircleIcon, ShieldCheckIcon, TrophyIcon, UserGroupIcon } from '@heroicons/react/24/outline';

function About() {
  const features = [
    {
      icon: ShieldCheckIcon,
      title: "Real-World Security Challenges",
      description: "Practice with carefully designed challenges that simulate real-world cybersecurity scenarios. From web exploitation to cryptography, enhance your skills across various security domains."
    },
    {
      icon: UserGroupIcon,
      title: "Global Community",
      description: "Join a thriving community of security enthusiasts, share knowledge, and learn from peers. Participate in discussions and collaborate with others to solve complex security challenges."
    },
    {
      icon: TrophyIcon,
      title: "Competitive Learning",
      description: "Climb the global leaderboard, earn points, and showcase your skills. Regular competitions and challenges keep you engaged and motivated to learn more."
    },
    {
      icon: InformationCircleIcon,
      title: "Comprehensive Learning Path",
      description: "Start from beginner-friendly challenges and progress to advanced security concepts. Each challenge includes detailed explanations and resources to help you learn and improve."
    }
  ];

  return (
    <div className="max-w-7xl mx-auto py-12 px-4 sm:px-6 lg:px-8">
      <div className="text-center mb-12">
        <h2 className="text-4xl font-extrabold text-green-500 glitch-text">
          {'<About_CyberChallenge/>'}
        </h2>
        <p className="mt-4 text-xl text-gray-300 font-mono">
          {'> Your Gateway to Cybersecurity Excellence'}
        </p>
      </div>

      <div className="bg-gray-800 rounded-lg border border-green-500 p-8 mb-12">
        <h3 className="text-2xl font-mono text-green-400 mb-6">{'> Our_Mission'}</h3>
        <p className="text-gray-300 font-mono leading-relaxed">
          CyberChallenge is dedicated to providing a dynamic platform for cybersecurity enthusiasts to enhance their skills through practical challenges and competitive learning. We believe in learning by doing and creating an environment where security professionals can grow together.
        </p>
      </div>

      <div className="grid grid-cols-1 md:grid-cols-2 gap-8">
        {features.map((feature, index) => (
          <div key={index} className="bg-gray-800 rounded-lg border border-green-500 p-6 hover:shadow-[0_0_10px_rgba(34,197,94,0.3)] transition-shadow">
            <div className="flex items-center mb-4">
              <feature.icon className="h-6 w-6 text-green-500 mr-3" />
              <h3 className="text-xl font-mono text-green-400">{feature.title}</h3>
            </div>
            <p className="text-gray-300 font-mono">{feature.description}</p>
          </div>
        ))}
      </div>

      <div className="mt-12 bg-gray-800 rounded-lg border border-green-500 p-8">
        <h3 className="text-2xl font-mono text-green-400 mb-6">{'> Get_Started'}</h3>
        <div className="space-y-4 text-gray-300 font-mono">
          <p>1. Create an account to track your progress</p>
          <p>2. Browse available challenges in the Events section</p>
          <p>3. Start with beginner-friendly challenges</p>
          <p>4. Join the community and share your knowledge</p>
          <p>5. Compete in regular CTF events</p>
        </div>
      </div>
    </div>
  );
}

export default About;