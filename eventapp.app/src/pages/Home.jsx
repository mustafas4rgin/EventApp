import React from 'react'
import { Link } from 'react-router-dom'
import { motion } from 'framer-motion'

const Home = () => {
  return (
    <div className="min-h-screen flex items-center justify-center bg-gray-50 dark:bg-gray-900 px-4 transition-colors duration-300">
      <motion.div
        initial={{ opacity: 0, y: 20 }}
        animate={{ opacity: 1, y: 0 }}
        transition={{ duration: 0.4 }}
        className="text-center max-w-2xl"
      >
        <h1 className="text-4xl sm:text-5xl font-extrabold text-gray-800 dark:text-white mb-6">
          Etkinlik Platformuna Hoş Geldin 🎉
        </h1>

        <p className="text-gray-600 dark:text-gray-300 text-lg mb-8 leading-relaxed">
          Yakındaki etkinlikleri keşfet, katıl ve topluluğun bir parçası ol. 
          Kolayca hesap oluştur ve giriş yaparak etkinlikleri yönet!
        </p>

        <div className="flex justify-center gap-4 flex-wrap mb-6">
          <Link
            to="/events"
            className="bg-blue-600 text-white px-6 py-3 rounded-lg text-sm font-medium hover:bg-blue-700 transition"
          >
            Etkinlikleri Gör
          </Link>
          <Link
            to="/register"
            className="bg-gray-200 text-gray-700 dark:bg-gray-700 dark:text-gray-200 px-6 py-3 rounded-lg text-sm font-medium hover:bg-gray-300 dark:hover:bg-gray-600 transition"
          >
            Kayıt Ol
          </Link>
        </div>

        <p className="text-sm text-gray-600 dark:text-gray-400">
          Zaten bir hesabın var mı?{' '}
          <Link to="/login" className="text-blue-600 dark:text-blue-400 font-medium hover:underline">
            Giriş Yap
          </Link>
        </p>
      </motion.div>
    </div>
  )
}

export default Home
