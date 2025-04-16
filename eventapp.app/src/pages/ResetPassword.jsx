import React, { useState } from 'react';
import { useSearchParams, useNavigate } from 'react-router-dom';
import axios from 'axios';

const ResetPassword = () => {
  const [searchParams] = useSearchParams();
  const navigate = useNavigate();

  const token = searchParams.get('token');
  if (!token) {
    navigate('/404', { replace: true });
  }
  const [password, setPassword] = useState('');
  const [confirmPassword, setConfirmPassword] = useState('');
  const [error, setError] = useState('');
  const [success, setSuccess] = useState(false);

  const validatePassword = () => {
    if (password.length < 8) return 'Şifre en az 8 karakter olmalı.';
    if (!/[A-Z]/.test(password)) return 'Şifre en az bir büyük harf içermeli.';
    if (!/[a-z]/.test(password)) return 'Şifre en az bir küçük harf içermeli.';
    if (!/\d/.test(password)) return 'Şifre en az bir rakam içermeli.';
    if (!/[\!\@\#\$\%\^\&\*\(\)\-\+\=]/.test(password)) return 'Şifre en az bir özel karakter içermeli.';
    if (password !== confirmPassword) return 'Şifreler eşleşmiyor.';
    return null;
  };

  const handleSubmit = async (e) => {
    e.preventDefault();
    setError('');

    const validationError = validatePassword();
    if (validationError) {
      setError(validationError);
      return;
    }

    try {
      const res = await axios.post('/api/Auth/reset-password', {
        token,
        newPassword: password,
      });

      if (res.status === 200) {
        setSuccess(true);
        setTimeout(() => navigate('/login'), 3000);
      }
    } catch (err) {
      setError('Şifre sıfırlanırken hata oluştu.');
    }
  };
  return (
    <div className="min-h-screen flex items-center justify-center bg-gray-100 dark:bg-gray-900 px-4">
      <div className="w-full max-w-md bg-white dark:bg-gray-800 p-8 rounded-lg shadow">
        <h2 className="text-2xl font-bold mb-6 text-center text-gray-800 dark:text-white">
          Yeni Şifre Belirle
        </h2>

        {success ? (
          <div className="text-green-600 text-sm text-center">
            Şifre başarıyla güncellendi. Giriş sayfasına yönlendiriliyorsunuz...
          </div>
        ) : (
          <form onSubmit={handleSubmit} className="space-y-5">
            <input
              type="password"
              placeholder="Yeni şifre"
              value={password}
              onChange={(e) => setPassword(e.target.value)}
              required
              className="w-full px-4 py-2 border rounded dark:bg-gray-700 dark:text-white"
            />

            <input
              type="password"
              placeholder="Şifreyi tekrar girin"
              value={confirmPassword}
              onChange={(e) => setConfirmPassword(e.target.value)}
              required
              className="w-full px-4 py-2 border rounded dark:bg-gray-700 dark:text-white"
            />

            {error && <div className="text-sm text-red-500">{error}</div>}

            <button
              type="submit"
              className="w-full bg-blue-600 hover:bg-blue-700 text-white py-2 rounded transition"
            >
              Şifreyi Sıfırla
            </button>
          </form>
        )}
      </div>
    </div>
  );
};

export default ResetPassword;
