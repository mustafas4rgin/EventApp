import React from 'react';
import { Link } from 'react-router-dom';
import { motion } from 'framer-motion';

const NotFound = () => {
  return (
    <div className="min-h-screen flex items-center justify-center bg-gray-100 dark:bg-gray-900 px-4">
      <motion.div
        initial={{ opacity: 0, scale: 0.95 }}
        animate={{ opacity: 1, scale: 1 }}
        transition={{ duration: 0.4 }}
        className="max-w-xl text-center bg-white dark:bg-gray-800 p-10 rounded-xl shadow-lg"
      >
        <h1 className="text-6xl font-extrabold text-blue-600 dark:text-blue-400 mb-4">404</h1>
        <p className="text-xl text-gray-800 dark:text-white font-semibold mb-2">Sayfa Bulunamadı</p>
        <p className="text-sm text-gray-600 dark:text-gray-300 mb-6">
          Aradığınız sayfa silinmiş veya hiç var olmamış olabilir.
        </p>

        <Link
          to="/"
          className="inline-block px-6 py-3 bg-blue-600 hover:bg-blue-700 text-white text-sm rounded-lg transition"
        >
          Ana Sayfaya Dön
        </Link>
      </motion.div>
    </div>
  );
};

export default NotFound;
