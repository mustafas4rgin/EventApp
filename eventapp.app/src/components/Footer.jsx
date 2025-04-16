import React from 'react';

function Footer() {
  return (
    <footer className="bg-white dark:bg-gray-900 text-gray-600 dark:text-gray-300 py-6 mt-10 border-t border-gray-200 dark:border-gray-700 transition-colors">
      <div className="container mx-auto px-4 text-center">
        <p className="text-sm">&copy; {new Date().getFullYear()} EventApp. Tüm hakları saklıdır.</p>
      </div>
    </footer>
  );
}

export default Footer;
