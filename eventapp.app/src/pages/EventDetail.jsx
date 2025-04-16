import React, { useEffect, useState } from 'react';
import { useParams } from 'react-router-dom';
import axios from 'axios';
import { jwtDecode } from 'jwt-decode';

function EventDetail() {
    const [event, setEvent] = useState(null);
    const [loading, setLoading] = useState(true);
    const [error, setError] = useState(null);
    const [userId, setUserId] = useState(null);
    const [resultMessage, setResultMessage] = useState(null);
    const { id } = useParams();

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
        axios
            .get(`/api/Event/${id}?include=creator-bookedusers`)
            .then((response) => {
                setEvent(response.data);
                setLoading(false);
            })
            .catch(() => {
                setError('Bir hata oluştu. Lütfen tekrar deneyin.');
                setLoading(false);
            });
    }, [id]);

    const handleBookEvent = async () => {
        try {
            const response = await axios.post(`/api/UserEventRel/Create/${userId}/${id}`);
            setResultMessage('Rezervasyon başarıyla eklendi.');

            const newUser = {
                id: userId,
                name: 'Siz', 
                profilePicture: null 
            };

            setEvent(prev => ({
                ...prev,
                bookedUsers: [...prev.bookedUsers, newUser]
            }));

        } catch (err) {
            const msg = err.response?.data || 'Bir hata oluştu.';
            setResultMessage(msg);
        }
    };

    if (loading) {
        return (
            <div className="flex justify-center items-center min-h-screen">
                <div className="text-gray-600 dark:text-gray-300 animate-pulse text-lg">Yükleniyor...</div>
            </div>
        );
    }

    if (error) {
        return (
            <div className="flex justify-center items-center min-h-screen">
                <div className="text-red-600 dark:text-red-400 text-lg">{error}</div>
            </div>
        );
    }

    if (!event) {
        return (
            <div className="flex justify-center items-center min-h-screen">
                <div className="text-gray-700 dark:text-gray-300">Etkinlik bulunamadı</div>
            </div>
        );
    }

    return (
        <div className="max-w-5xl mx-auto px-4 py-10 text-gray-800 dark:text-gray-100">
            <h1 className="text-3xl font-bold mb-6 text-center">{event.title}</h1>

            <div className="bg-white dark:bg-gray-800 rounded-xl shadow-md overflow-hidden md:flex">
                <img
                    src={event.imageUrl || 'https://picsum.photos/500/300'}
                    alt={event.title}
                    className="w-full md:w-1/2 object-cover h-64 md:h-auto"
                />

                <div className="p-6 flex flex-col justify-between">
                    <div>
                        <p className="text-lg mb-4">{event.description}</p>
                        <p className="text-sm text-gray-600 dark:text-gray-400 mb-2">
                            <span className="font-semibold">Oluşturan:</span> {event.createdByUser?.name}
                        </p>
                        <p className="text-sm text-gray-600 dark:text-gray-400">
                            <span className="font-semibold">Konum:</span> Gizli
                        </p>

                    </div>
                    <button
                        onClick={handleBookEvent}
                        disabled={event.bookedUsers.some(user => user.id === userId)}
                        className="mb-4 px-6 py-3 bg-gradient-to-r from-green-500 to-emerald-600 text-white rounded-full shadow-md hover:shadow-lg hover:from-green-600 hover:to-emerald-700 transition-all duration-300 text-sm font-semibold tracking-wide disabled:opacity-50 disabled:cursor-not-allowed"
                    >
                        Rezervasyon Yap
                    </button>

                    {resultMessage && (
                        <div className={`mb-6 px-4 py-3 rounded-lg text-sm font-medium shadow-inner transition-all duration-300
    ${resultMessage.toLowerCase().includes('successfully') ? 'bg-green-100 text-green-800 dark:bg-green-900 dark:text-green-200'
                                : 'bg-red-100 text-red-800 dark:bg-red-900 dark:text-red-200'}`}>
                            {resultMessage}
                        </div>
                    )}

                </div>
            </div>

            <div className="mt-10">

                <h2 className="text-xl font-semibold mb-4">Rezervasyon Yapanlar</h2>
                {event.bookedUsers && event.bookedUsers.length > 0 ? (
                    <div className="grid grid-cols-2 sm:grid-cols-3 md:grid-cols-4 gap-4">
                        {event.bookedUsers.map((user, index) => (
                            <div
                                key={index}
                                className="bg-white dark:bg-gray-800 p-4 rounded-lg shadow flex flex-col items-center text-center"
                            >
                                <img
                                    src={user.profilePicture || 'https://picsum.photos/50'}
                                    alt={user.name}
                                    className="w-16 h-16 rounded-full object-cover mb-2"
                                />
                                <p className="text-sm font-medium">{user.name}</p>
                            </div>
                        ))}
                    </div>
                ) : (
                    <p className="text-gray-600 dark:text-gray-400">Henüz rezervasyon yapan yok.</p>
                )}
            </div>
        </div>
    );
}

export default EventDetail;
