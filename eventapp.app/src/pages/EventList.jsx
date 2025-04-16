import React, { useEffect, useState } from 'react';
import { motion, AnimatePresence } from 'framer-motion';
import { useNavigate } from 'react-router-dom';
import { jwtDecode } from 'jwt-decode';

const EventList = () => {
    const navigate = useNavigate();
    const [userId, setUserId] = useState(null);
    const [events, setEvents] = useState([]);
    const [loading, setLoading] = useState(true);
    const [error, setError] = useState(null);
    const [selectedEvent, setSelectedEvent] = useState(null);

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
        fetch('http://localhost:5148/api/Event/get-events?include=bookedusers')
            .then((response) => response.json())
            .then((data) => {
                setEvents(data);
                setLoading(false);
            })
            .catch(() => {
                setError('Bir hata oluştu. Lütfen tekrar deneyin.');
                setLoading(false);
            });
    }, []);

    const closeModal = () => setSelectedEvent(null);

    useEffect(() => {
        const handleKeyDown = (e) => {
            if (e.key === 'Escape') closeModal();
        };
        window.addEventListener('keydown', handleKeyDown);
        return () => window.removeEventListener('keydown', handleKeyDown);
    }, []);

    if (loading) {
        return (
            <div className="flex items-center justify-center h-screen text-gray-600 dark:text-gray-300 text-lg">
                Yükleniyor...
            </div>
        );
    }

    if (error) {
        return (
            <div className="flex items-center justify-center h-screen text-red-600 dark:text-red-400 text-lg">
                {error}
            </div>
        );
    }

    return (
        <div className="min-h-screen bg-gray-50 dark:bg-gray-900 px-4 py-12 transition-colors duration-300">
            <h1 className="text-4xl font-bold text-center text-gray-800 dark:text-white mb-12">
                Etkinlikler
            </h1>

            <div className="grid grid-cols-1 sm:grid-cols-2 md:grid-cols-3 gap-8">
                {events.map((event) => (
                    <motion.div
                        key={event.id}
                        onClick={() => setSelectedEvent(event)}
                        whileHover={{ scale: 1.02 }}
                        whileTap={{ scale: 0.98 }}
                        transition={{ type: 'spring', stiffness: 250 }}
                        className="bg-white dark:bg-gray-800 text-gray-800 dark:text-white rounded-2xl shadow-md hover:shadow-xl transition-all duration-300 cursor-pointer overflow-hidden"
                    >
                        <img
                            src={event.imageUrl || 'https://picsum.photos/400/250'}
                            alt={event.title}
                            className="w-full h-48 object-cover"
                        />
                        <div className="p-5">
                            <h3 className="text-xl font-bold mb-2">
                                {event.title}
                                {event.bookedUsers?.some(user => user.id == userId) && (
                                    <span className="block text-sm text-green-600 dark:text-green-400 mt-1">
                                        Bu etkinliğe kayıtlısınız ✔️
                                    </span>
                                )}
                            </h3>

                            <p className="text-sm text-gray-600 dark:text-gray-300 mb-4">
                                {event.description?.length > 80
                                    ? `${event.description.slice(0, 80)}...`
                                    : event.description || 'Açıklama bulunmuyor.'}
                            </p>
                            <button
                                onClick={(e) => {
                                    e.stopPropagation();
                                    navigate(`/event/${event.id}`);
                                }}
                                className="mt-4 px-5 py-2 rounded-full bg-gradient-to-r from-blue-600 to-purple-600 hover:from-blue-700 hover:to-purple-700 text-white text-sm font-medium shadow-md hover:shadow-lg transform hover:scale-105 transition-all duration-300"
                            >
                                Detaya Git
                            </button>

                        </div>
                    </motion.div>
                ))}
            </div>

            <AnimatePresence>
                {selectedEvent && (
                    <motion.div
                        key="overlay"
                        initial={{ opacity: 0 }}
                        animate={{ opacity: 1 }}
                        exit={{ opacity: 0 }}
                        onClick={closeModal}
                        className="fixed inset-0 bg-black/60 backdrop-blur-sm flex items-center justify-center z-50"
                    >
                        <motion.div
                            key="modal"
                            onClick={(e) => e.stopPropagation()}
                            initial={{ opacity: 0, y: 40, scale: 0.95 }}
                            animate={{ opacity: 1, y: 0, scale: 1 }}
                            exit={{ opacity: 0, y: 40, scale: 0.95 }}
                            transition={{ duration: 0.25 }}
                            className="bg-white dark:bg-gray-800 text-gray-900 dark:text-white w-full max-w-2xl rounded-xl p-6 shadow-xl relative"
                        >
                            <button
                                onClick={closeModal}
                                className="absolute top-4 right-4 text-gray-400 dark:text-gray-300 hover:text-red-500 text-2xl"
                            >
                                &times;
                            </button>
                            <img
                                src={selectedEvent.imageUrl || 'https://picsum.photos/600/300'}
                                alt={selectedEvent.title}
                                className="rounded-lg mb-4 w-full h-64 object-cover"
                            />
                            <h2 className="text-2xl font-semibold mb-2">{selectedEvent.title}</h2>
                            <p className="text-sm">{selectedEvent.description || 'Açıklama bulunmuyor.'}</p>
                        </motion.div>
                    </motion.div>
                )}
            </AnimatePresence>
        </div>
    );
};

export default EventList;
