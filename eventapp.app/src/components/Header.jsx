import axios from 'axios';
import { jwtDecode } from 'jwt-decode';
import React, { useEffect, useState } from 'react';
import { Link } from 'react-router-dom';
import LoginButton from '../buttons/LoginButton';
import RegisterButton from '../buttons/RegisterButton';

function Header() {
    const token = localStorage.getItem('token');
    const [userId, setUserId] = useState(null);
    const [user, setUser] = useState(null);
    const [isAdmin, setIsAdmin] = useState(false);
    const [loading, setLoading] = useState(true);
    const [darkMode, setDarkMode] = useState(
        localStorage.getItem('theme') === 'dark'
    );

    // Dark mode toggle effect
    useEffect(() => {
        if (darkMode) {
            document.documentElement.classList.add('dark');
            localStorage.setItem('theme', 'dark');
        } else {
            document.documentElement.classList.remove('dark');
            localStorage.setItem('theme', 'light');
        }
    }, [darkMode]);

    const toggleDarkMode = () => {
        setDarkMode(!darkMode);
    };

    useEffect(() => {
        if (token) {
            try {
                const decoded = jwtDecode(token);
                setUserId(decoded.id);
            } catch (error) {
                console.error('Token çözümlemesi başarısız:', error);
            }
        }
    }, [token]);

    useEffect(() => {
        const fetchUserData = async () => {
            if (userId) {
                try {
                    const response = await axios.get(`/api/User/users/${userId}?include=role`);
                    if (response.status === 200) {
                        setUser(response.data);
                        setIsAdmin(response.data.role.name === 'Admin');
                    }
                } catch (error) {
                    console.error('Kullanıcı verisi alınamadı:', error);
                } finally {
                    setLoading(false);
                }
            }
        };

        if (userId) {
            fetchUserData();
        }
    }, [userId]);

    const handleLogout = () => {
        localStorage.removeItem('token');
        localStorage.removeItem('user');
        window.location.href = '/';
    };

    return (
        <header className="bg-white dark:bg-gray-900 shadow-md dark:shadow-lg">
            <nav className="container mx-auto px-4 py-4 flex items-center justify-between">
                <div className="text-2xl font-bold text-blue-600 dark:text-white">
                    <Link to="/">EventApp</Link>
                </div>

                <ul className="flex items-center space-x-6">
                    <li>
                        <Link to="/" className="text-gray-700 dark:text-gray-300 hover:text-blue-600 dark:hover:text-blue-400 transition">
                            Home
                        </Link>
                    </li>
                    <li>
                        <Link to="/events" className="text-gray-700 dark:text-gray-300 hover:text-blue-600 dark:hover:text-blue-400 transition">
                            Events
                        </Link>
                    </li>
                    <li>
                        <Link to="/dashboard" className="text-gray-700 dark:text-gray-300 hover:text-blue-600 dark:hover:text-blue-400 transition">
                            Dashboard
                        </Link>
                    </li>

                    {isAdmin && (
                        <li>
                            <Link to="/admin">
                                <button className="text-gray-700 dark:text-gray-300 hover:text-blue-600 dark:hover:text-blue-400 transition">Admin Panel</button>
                            </Link>
                        </li>
                    )}

                    <li>
                        <button
                            onClick={toggleDarkMode}
                            className="text-xl hover:scale-110 transition"
                            title="Tema Değiştir"
                        >
                            {darkMode ? '🌙' : '☀️'}
                        </button>
                    </li>

                    {!token ? (
                        <>
                            <li>
                                <RegisterButton />
                            </li>
                            <li>
                                <LoginButton />
                            </li>
                        </>
                    ) : (
                        <>
                            <li className="text-sm text-gray-600 dark:text-gray-300">
                                Merhaba, <span className="font-semibold text-gray-800 dark:text-white">{user?.name || 'Kullanıcı'}</span>
                            </li>
                            <li>
                                <button
                                    onClick={handleLogout}
                                    className="bg-red-600 text-white px-4 py-2 rounded-lg hover:bg-red-700 transition"
                                >
                                    Logout
                                </button>
                            </li>
                        </>
                    )}
                </ul>
            </nav>
        </header>
    );
}

export default Header;
