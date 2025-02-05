import { createBrowserRouter } from 'react-router-dom';
import Layout from '../components/Layout';
import LandingPage from '../components/LandingPage';
import EventsPage from '../components/EventsPage';
import Leaderboard from '../components/Leaderboard';
import ChallengeDetails from '../components/ChallengeDetails';
import About from '../components/About';
import AdminDashboard from '../components/AdminDashboard';

export const ROUTES = {
  HOME: '/',
  EVENTS: '/events',
  LEADERBOARD: '/leaderboard',
  CHALLENGE: '/challenge/:id',
  ABOUT: '/about',
  ADMIN: '/adminDashboard',
};

export const router = createBrowserRouter([
  {
    path: '/',
    element: <Layout />,
    children: [
      {
        index: true,
        element: <LandingPage />,
      },
      {
        path: ROUTES.EVENTS,
        element: <EventsPage />,
      },
      {
        path: ROUTES.LEADERBOARD,
        element: <Leaderboard />,
      },
      {
        path: ROUTES.CHALLENGE,
        element: <ChallengeDetails />,
      },
      {
        path: ROUTES.ABOUT,
        element: <About />,
      },
    ],
  },
  {
    path: ROUTES.ADMIN,
    element: <AdminDashboard />,
  },
]);