import logo from './logo.svg';
import './App.css';
import EventList from './pages/EventList';
import { BrowserRouter as Router, Route, Routes } from 'react-router-dom';
import Home from './pages/Home';
import Header from './components/Header';
import Footer from './components/Footer';
import EventDetail from './pages/EventDetail';
import Login from './pages/Login';
import Dashboard from './pages/Dashboard';
import ProtectedPublicRoute from './components/ProtectedPublicRoute';
import Register from './pages/Register';
import ProtectedPrivateRoute from './components/ProtectedPrivateRoute';
import AdminPanel, { AdminDashboard, AdminEvents, AdminUsers } from './pages/AdminPanel';
import ProtectedAdminRoute from './components/ProtectedAdminRout';
import ForgotPassword from './pages/ForgotPassword';
import ResetPassword from './pages/ResetPassword';
import NotFound from './pages/NotFound';

function App() {
  return (
    <Router>
      <Header />
      <Routes>
        <Route path="/" element={<Home />}></Route>
        <Route path="/events" element={<EventList />}></Route>
        <Route path="/event/:id" element={<EventDetail />} />
        <Route
          path="/admin"
          element={
            <ProtectedAdminRoute>
              <AdminPanel />
            </ProtectedAdminRoute>
          }
        >
          <Route
            path="dashboard"
            element={
              <ProtectedAdminRoute>
                <AdminDashboard />
              </ProtectedAdminRoute>
            } />
          <Route
            path="users"
            element={
              <ProtectedAdminRoute>
                <AdminUsers />
              </ProtectedAdminRoute>
            } />
          <Route
            path="events"
            element={
              <ProtectedAdminRoute>
                <AdminEvents />
              </ProtectedAdminRoute>} />
          {/* daha sonra users, events de buraya eklersin */}
        </Route>
        <Route
          path="/login"
          element={
            <ProtectedPublicRoute>
              <Login />
            </ProtectedPublicRoute>
          }
        />
        <Route
          path="/dashboard"
          element={
            <ProtectedPrivateRoute>
              <Dashboard />
            </ProtectedPrivateRoute>
          }
        />
        <Route
          path="/register"
          element={
            <ProtectedPublicRoute>
              <Register />
            </ProtectedPublicRoute>
          }
        />

        <Route
          path="forgot-password"
          element={
            <ProtectedPublicRoute>
              <ForgotPassword />
            </ProtectedPublicRoute>
          }
        />

        <Route
          path="/reset-password"
          element={
            <ProtectedPublicRoute>
              <ResetPassword />
            </ProtectedPublicRoute>
          }
        />

        <Route path="*" element={<NotFound />} />
      </Routes>
      <Footer />
    </Router>

  );
}

export default App;
