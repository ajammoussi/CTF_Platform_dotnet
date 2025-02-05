import { createBrowserRouter } from 'react-router-dom';
import Layout from '../components/Layout';
import LandingPage from '../components/LandingPage';
import EventsPage from '../components/EventsPage';
import Leaderboard from '../components/Leaderboard';
import ChallengesDetails from '../components/ChallengesDetails';
import About from '../components/About';
import AdminDashboard from '../components/AdminDashboard';
import CreateTeam from '../components/CreateTeam';
import InviteToTeam from '../components/InviteToTeam';
import Login from '../components/Login';
import Signup from '../components/Signup';
import ForgotPassword from '../components/ForgotPassword';
import ResetPassword from '../components/ResetPassword';

export const ROUTES = {
  HOME: '/',
  EVENTS: '/Challenges',
  LEADERBOARD: '/leaderboard',
  CHALLENGES: '/challenge/:id',
  ABOUT: '/about',
  ADMIN: '/adminDashboard',
  CREATE_TEAM: '/create-team',
  INVITE_TEAM: '/invite-team',
  LOGIN: '/login',
  SIGNUP: '/signup',
  FORGOT_PASSWORD: '/forgot-password',
  RESET_PASSWORD: '/reset-password',
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
        path: ROUTES.CHALLENGES,
        element: <ChallengesDetails />,
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
  {
    path: ROUTES.CREATE_TEAM,
    element: <CreateTeam />,
  },
  {
    path: ROUTES.INVITE_TEAM,
    element: <InviteToTeam />,
  },
  {
    path: ROUTES.LOGIN,
    element: <Login />,
  },
  {
    path: ROUTES.SIGNUP,
    element: <Signup />,
  },
  {
    path: ROUTES.FORGOT_PASSWORD,
    element: <ForgotPassword />,
  },
  {
    path: ROUTES.RESET_PASSWORD,
    element: <ResetPassword />,
  },
]);