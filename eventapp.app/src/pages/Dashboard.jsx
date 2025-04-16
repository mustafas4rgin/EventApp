import axios from 'axios';
import React, { useEffect, useState } from 'react';
import { jwtDecode } from 'jwt-decode';
import { Link } from 'react-router-dom';

function Dashboard() {
  const [loading, setLoading] = useState(true);
  const [user, setUser] = useState(null);
  const [userId, setUserId] = useState(null);
  const [bookedEvents, setBookedEvents] = useState([]);

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
    const fetchUserData = async () => {
      try {
        const response = await axios.get(`/api/User/users/${userId}?include=bookedevents`);
        if (response.status === 200) {
          setUser(response.data);
          setBookedEvents(response.data.bookedEvents);
        }
      } catch (err) {
        console.error('Kullanıcı verisi alınamadı:', err);
      } finally {
        setLoading(false);
      }
    };

    if (userId) fetchUserData();
  }, [userId]);

  const handleEventDelete = async (eventId) => {
    try {
      await axios.delete(`/api/UserEventRel/Delete/${userId}/${eventId}`);
      setBookedEvents((prev) => prev.filter((event) => event.id !== eventId));
    } catch (err) {
      console.error('Rezervasyon iptali başarısız:', err);
    }
  };

  if (loading) {
    return <div className="flex justify-center items-center h-screen text-gray-600 dark:text-gray-300 text-xl">Yükleniyor...</div>;
  }

  if (!user) {
    return <div className="text-center text-red-500 dark:text-red-400 mt-10">Kullanıcı bulunamadı.</div>;
  }

  return (
    <div className="min-h-screen bg-gray-50 dark:bg-gray-900 px-4 py-10 transition-colors duration-300">
      <div className="max-w-5xl mx-auto space-y-10">
        <div className="bg-white dark:bg-gray-800 text-gray-800 dark:text-white shadow-md rounded-2xl p-6 flex items-center justify-between">
          <div>
            <h2 className="text-2xl font-bold">👋 Hoş geldin, {user.name}</h2>
            <p className="text-sm text-gray-500 dark:text-gray-300">Email: {user.email}</p>
          </div>
          <span className="bg-blue-100 dark:bg-blue-600 dark:text-white text-blue-600 text-xs px-3 py-1 rounded-full">
            {bookedEvents.length} rezervasyon
          </span>
        </div>

        {bookedEvents.length > 0 ? (
          <div>
            <h3 className="text-xl font-semibold text-gray-700 dark:text-gray-200 mb-4">🎟️ Etkinlik Rezervasyonların</h3>
            <div className="grid grid-cols-1 sm:grid-cols-2 gap-6">
              {bookedEvents.map((event) => (
                <div
                  key={event.id}
                  className="bg-white dark:bg-gray-800 text-gray-800 dark:text-white border border-gray-200 dark:border-gray-700 rounded-xl shadow-sm hover:shadow-md transition p-5 flex flex-col justify-between"
                >
                  <div>
                    <h4 className="text-lg font-semibold mb-1">{event.title}</h4>
                    <p className="text-sm text-gray-600 dark:text-gray-300 line-clamp-3">{event.description || "Açıklama bulunmuyor."}</p>
                  </div>
                  <div className="mt-4 flex justify-between items-center">
                    <Link
                      to={`/event/${event.id}`}
                      className="text-sm text-blue-600 dark:text-blue-400 hover:underline"
                    >
                      Detaylar →
                    </Link>
                    <button
                      onClick={() => handleEventDelete(event.id)}
                      className="text-sm px-3 py-1.5 bg-red-500 text-white rounded-md hover:bg-red-600 transition"
                    >
                      İptal Et
                    </button>
                  </div>
                </div>
              ))}
            </div>
          </div>
        ) : (
          <div className="text-center bg-white dark:bg-gray-800 py-12 px-6 rounded-2xl shadow-md">
            <h3 className="text-xl text-gray-700 dark:text-gray-200 font-medium mb-4">Henüz rezervasyon yapmadın.</h3>
            <p className="text-gray-500 dark:text-gray-400 mb-6">Yeni etkinlikleri kaçırmamak için hemen göz at!</p>
            <Link
              to="/events"
              className="inline-block bg-blue-600 text-white px-6 py-3 rounded-lg text-sm font-medium hover:bg-blue-700 transition"
            >
              Etkinlikleri Keşfet
            </Link>
          </div>
        )}
      </div>
    </div>
  );
}

export default Dashboard;