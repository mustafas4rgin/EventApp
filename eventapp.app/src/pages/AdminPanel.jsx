import React, { useEffect, useState } from 'react';
import { Link, Outlet } from 'react-router-dom';
import {
    LayoutDashboard,
    Users,
    CalendarDays,
    LogOut,
    Trash2,
    PlusCircle,
} from 'lucide-react';
import axios from 'axios';
import { jwtDecode } from 'jwt-decode';
import LogoutButton from '../buttons/LogoutButton';



const AdminPanel = () => {

    return (
        <div className="min-h-screen flex bg-gray-100 dark:bg-gray-900 text-gray-800 dark:text-gray-100">
            {/* Sidebar */}
            <aside className="w-64 bg-white dark:bg-gray-800 shadow-lg hidden md:block">
                <div className="p-6 text-2xl font-bold border-b border-gray-200 dark:border-gray-700">
                    Admin Panel
                </div>
                <nav className="p-4 space-y-2">
                    <Link to="/admin/dashboard" className="flex items-center gap-3 px-4 py-2 rounded-lg hover:bg-blue-100 dark:hover:bg-blue-800 transition">
                        <LayoutDashboard size={20} /> Dashboard
                    </Link>
                    <Link to="/admin/users" className="flex items-center gap-3 px-4 py-2 rounded-lg hover:bg-blue-100 dark:hover:bg-blue-800 transition">
                        <Users size={20} /> Users
                    </Link>
                    <Link to="/admin/events" className="flex items-center gap-3 px-4 py-2 rounded-lg hover:bg-blue-100 dark:hover:bg-blue-800 transition">
                        <CalendarDays size={20} /> Events
                    </Link>
                    <LogoutButton />
                </nav>
            </aside>

            {/* Content */}
            <main className="flex-1 p-6">
                <Outlet />
            </main>
        </div>
    );
};

export default AdminPanel;



export const AdminDashboard = () => {
    const [stats, setStats] = useState({
        eventCount: 0,
        userCount: 0,
        resarvationCount: 0,
    });

    useEffect(() => {
        const fetchStats = async () => {
            try {
                const response = await axios.get('/api/Admin/dashboard-summary');
                if (response.status !== 200) {
                    throw new Error('Failed to fetch stats');
                }
                setStats(response.data.data);
            } catch (error) {
                console.error('Error fetching stats:', error);
            }
        };

        fetchStats();
    }, []);
    const { userCount, eventCount, resarvationCount } = stats;
    return (
        <div className="space-y-6">
            <h1 className="text-3xl font-bold mb-6">Yönetim Paneli</h1>

            <div className="grid grid-cols-1 sm:grid-cols-2 lg:grid-cols-3 gap-6">
                <div className="bg-white dark:bg-gray-800 p-6 rounded-lg shadow">
                    <h2 className="text-lg font-semibold mb-2">Toplam Kullanıcı</h2>
                    <p className="text-2xl font-bold text-blue-600 dark:text-blue-400">{stats.userCount}</p>
                </div>

                <div className="bg-white dark:bg-gray-800 p-6 rounded-lg shadow">
                    <h2 className="text-lg font-semibold mb-2">Toplam Etkinlik</h2>
                    <p className="text-2xl font-bold text-green-600 dark:text-green-400">{stats.eventCount}</p>
                </div>

                <div className="bg-white dark:bg-gray-800 p-6 rounded-lg shadow">
                    <h2 className="text-lg font-semibold mb-2">Toplam Rezervasyon</h2>
                    <p className="text-2xl font-bold text-purple-600 dark:text-purple-400">{stats.resarvationCount}</p>
                </div>
            </div>
        </div>
    );
};

export const AdminUsers = () => {
    const [users, setUsers] = useState([]);
    const [roles, setRoles] = useState([]);
    const [loading, setLoading] = useState(true);
    const [search, setSearch] = useState('');
    const [currentPage, setCurrentPage] = useState(1);
    const usersPerPage = 10;

    useEffect(() => {
        axios.get('/api/User/get-users?include=role')
            .then(res => setUsers(res.data))
            .catch(err => console.error('Kullanıcı verileri alınamadı:', err))
            .finally(() => setLoading(false));

        axios.get('/api/Role/GetRoles')
            .then(res => setRoles(res.data))
            .catch(err => console.error('Roller alınamadı:', err));
    }, []);

    const handleDelete = (id) => {
        if (!window.confirm('Bu kullanıcıyı silmek istediğinize emin misiniz?')) return;
        axios.delete(`/api/User/${id}`)
            .then(() => {
                setUsers(prev => prev.filter(user => user.id !== id));
            })
            .catch(err => console.error('Kullanıcı silinirken hata oluştu:', err));
    };

    const handleRoleChange = (userId, newRoleId) => {
        const roleIdNumber = parseInt(newRoleId);
        const selectedRole = roles.find(r => r.id === roleIdNumber);
        if (!selectedRole) return;

        axios
            .put(`/api/Admin/updaterole/${userId}?roleId=${roleIdNumber}`, { roleId: roleIdNumber })
            .then(() => {
                setUsers(prevUsers =>
                    prevUsers.map(user =>
                        user.id === userId ? { ...user, role: { id: selectedRole.id, name: selectedRole.name } } : user
                    )
                );
            })
            .catch(err => console.error('Rol değiştirilirken hata oluştu:', err));
    };

    const filteredUsers = users.filter(user =>
        user.name.toLowerCase().includes(search.toLowerCase()) ||
        user.email.toLowerCase().includes(search.toLowerCase())
    );

    const totalPages = Math.ceil(filteredUsers.length / usersPerPage);
    const currentUsers = filteredUsers.slice((currentPage - 1) * usersPerPage, currentPage * usersPerPage);

    return (
        <div>
            <h1 className="text-3xl font-bold mb-6">Kullanıcılar</h1>

            <input
                type="text"
                placeholder="İsim veya email ile ara..."
                value={search}
                onChange={(e) => {
                    setSearch(e.target.value);
                    setCurrentPage(1);
                }}
                className="mb-4 px-4 py-2 w-full max-w-sm border rounded shadow-sm text-sm dark:bg-gray-800 dark:text-white"
            />

            {loading ? (
                <p className="text-gray-500 dark:text-gray-300">Yükleniyor...</p>
            ) : (
                <div className="overflow-x-auto">
                    <table className="min-w-full table-auto bg-white dark:bg-gray-800 shadow-md rounded-lg">
                        <thead className="bg-gray-100 dark:bg-gray-700">
                            <tr>
                                <th className="px-4 py-2 text-left">ID</th>
                                <th className="px-4 py-2 text-left">İsim</th>
                                <th className="px-4 py-2 text-left">Email</th>
                                <th className="px-4 py-2 text-left">Rol</th>
                                <th className="px-4 py-2 text-left">Rol Güncelle</th>
                                <th className="px-4 py-2 text-left">Sil</th>
                            </tr>
                        </thead>
                        <tbody>
                            {currentUsers.map(user => (
                                <tr key={user.id} className="border-b border-gray-200 dark:border-gray-700 hover:bg-gray-50 dark:hover:bg-gray-700">
                                    <td className="px-4 py-2">{user.id}</td>
                                    <td className="px-4 py-2">{user.name}</td>
                                    <td className="px-4 py-2">{user.email}</td>
                                    <td className="px-4 py-2">
                                        <span className={`px-2 py-1 text-xs font-semibold rounded-full ${user.role.name === 'Admin' ? 'bg-blue-100 text-blue-600' : 'bg-green-100 text-green-600'}`}>{user.role.name}</span>
                                    </td>
                                    <td className="px-4 py-2">
                                        <select
                                            value={user.role.id || ''}
                                            onChange={(e) => handleRoleChange(user.id, e.target.value)}
                                            className="bg-transparent border rounded px-2 py-1 text-sm"
                                        >
                                            <option value="" disabled>Rol Seç</option>
                                            {roles.map((role) => (
                                                <option key={role.id} value={role.id}>{role.name}</option>
                                            ))}
                                        </select>
                                    </td>
                                    <td className="px-4 py-2">
                                        <button
                                            onClick={() => handleDelete(user.id)}
                                            className="text-red-600 hover:text-red-800 dark:hover:text-red-400 transition"
                                            title="Sil"
                                        >
                                            <Trash2 size={18} />
                                        </button>
                                    </td>
                                </tr>
                            ))}
                        </tbody>
                    </table>

                    <div className="flex justify-center items-center gap-2 mt-4">
                        {Array.from({ length: totalPages }, (_, i) => (
                            <button
                                key={i}
                                onClick={() => setCurrentPage(i + 1)}
                                className={`px-3 py-1 rounded text-sm ${currentPage === i + 1 ? 'bg-blue-600 text-white' : 'bg-gray-200 dark:bg-gray-700 text-gray-800 dark:text-white'}`}
                            >
                                {i + 1}
                            </button>
                        ))}
                    </div>
                </div>
            )}
        </div>
    );
};

export const AdminEvents = () => {
    const [userId, setUserId] = useState(null);
    const [events, setEvents] = useState([]);
    const [loading, setLoading] = useState(true);
    const [search, setSearch] = useState('');
    const [currentPage, setCurrentPage] = useState(1);
    const [newEvent, setNewEvent] = useState({ title: '', description: '', creatorId: null });
    const itemsPerPage = 10;

    const token = localStorage.getItem('token');

    useEffect(() => {
        if (token) {
            try {
                const decoded = jwtDecode(token);
                setUserId(decoded.id);
            } catch (err) {
                console.error('Token çözümleme hatası:', err);
            }
        }
    }, [token]);


    useEffect(() => {
        fetchEvents();
    }, []);

    const fetchEvents = () => {
        axios.get('/api/Event/get-events?include=role')
            .then(res => setEvents(res.data))
            .catch(err => console.error('Etkinlikler alınamadı:', err))
            .finally(() => setLoading(false));
    };

    const handleDeleteEvent = (id) => {
        if (!window.confirm('Bu etkinliği silmek istediğinize emin misiniz?')) return;
        axios.delete(`/api/Event/Delete/${id}`)
            .then(() => {
                setEvents(prev => prev.filter(event => event.id !== id));
            })
            .catch(err => console.error('Etkinlik silinirken hata oluştu:', err));
    };

    const handleAddEvent = () => {
        if (!newEvent.title || !newEvent.description) return;

        const eventToCreate = {
            ...newEvent,
            createdByUserId: userId,
        };

        axios.post('/api/Event/Create', eventToCreate)
            .then(() => {
                fetchEvents();
                setNewEvent({ title: '', description: '', creatorId: null });
            })
            .catch(err => console.error('Etkinlik oluşturulamadı:', err));
    };


    const filteredEvents = events.filter(event =>
        event.title.toLowerCase().includes(search.toLowerCase()) ||
        event.description.toLowerCase().includes(search.toLowerCase())
    );

    const totalPages = Math.ceil(filteredEvents.length / itemsPerPage);
    const currentEvents = filteredEvents.slice(
        (currentPage - 1) * itemsPerPage,
        currentPage * itemsPerPage
    );

    return (
        <div>
            <h1 className="text-3xl font-bold mb-6">Etkinlikler</h1>

            <div className="grid grid-cols-1 lg:grid-cols-3 gap-4 mb-4">
                <input
                    type="text"
                    placeholder="Etkinlik ara..."
                    value={search}
                    onChange={(e) => {
                        setSearch(e.target.value);
                        setCurrentPage(1);
                    }}
                    className="px-4 py-2 w-full border rounded shadow-sm text-sm dark:bg-gray-800 dark:text-white"
                />

                <input
                    type="text"
                    placeholder="Yeni etkinlik başlığı"
                    value={newEvent.title}
                    onChange={(e) => setNewEvent(prev => ({ ...prev, title: e.target.value }))}
                    className="px-3 py-2 w-full border rounded text-sm dark:bg-gray-800 dark:text-white"
                />
                <div className="flex gap-2">
                    <input
                        type="text"
                        placeholder="Açıklama"
                        value={newEvent.description}
                        onChange={(e) => setNewEvent(prev => ({ ...prev, description: e.target.value }))}
                        className="px-3 py-2 flex-1 border rounded text-sm dark:bg-gray-800 dark:text-white"
                    />
                    <button
                        onClick={handleAddEvent}
                        className="px-3 py-2 bg-green-600 hover:bg-green-700 text-white rounded text-sm flex items-center gap-1"
                    >
                        <PlusCircle size={16} /> Ekle
                    </button>
                </div>
            </div>

            {loading ? (
                <p className="text-gray-500 dark:text-gray-300">Yükleniyor...</p>
            ) : (
                <>
                    <div className="overflow-x-auto">
                        <table className="min-w-full table-auto bg-white dark:bg-gray-800 shadow-md rounded-lg">
                            <thead className="bg-gray-100 dark:bg-gray-700">
                                <tr>
                                    <th className="px-4 py-2 text-left">Başlık</th>
                                    <th className="px-4 py-2 text-left">Açıklama</th>
                                    <th className="px-4 py-2 text-left">Oluşturan</th>
                                    <th className="px-4 py-2 text-left">İşlem</th>
                                </tr>
                            </thead>
                            <tbody>
                                {currentEvents.map(event => (
                                    <tr key={event.id} className="border-b border-gray-200 dark:border-gray-700 hover:bg-gray-50 dark:hover:bg-gray-700">
                                        <td className="px-4 py-2 font-medium">{event.title}</td>
                                        <td className="px-4 py-2 text-sm text-gray-600 dark:text-gray-300 line-clamp-2">{event.description}</td>
                                        <td className="px-4 py-2 text-sm text-gray-500 dark:text-gray-400">{event.createdByUser?.name}</td>
                                        <td className="px-4 py-2">
                                            <button
                                                onClick={() => handleDeleteEvent(event.id)}
                                                className="px-3 py-1 text-sm bg-red-600 hover:bg-red-700 text-white rounded"
                                            >
                                                Sil
                                            </button>
                                        </td>
                                    </tr>
                                ))}
                            </tbody>
                        </table>
                    </div>

                    <div className="flex justify-center items-center gap-2 mt-4">
                        {Array.from({ length: totalPages }, (_, i) => (
                            <button
                                key={i}
                                onClick={() => setCurrentPage(i + 1)}
                                className={`px-3 py-1 rounded text-sm ${currentPage === i + 1 ? 'bg-blue-600 text-white' : 'bg-gray-200 dark:bg-gray-700 text-gray-800 dark:text-white'}`}
                            >
                                {i + 1}
                            </button>
                        ))}
                    </div>
                </>
            )}
        </div>
    );
};
