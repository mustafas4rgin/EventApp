import React from 'react';
import { useNavigate } from 'react-router-dom';
import { UserPlus } from 'lucide-react';

const RegisterButton = () => {
  const navigate = useNavigate();

  const handleRegisterRedirect = () => {
    navigate('/register');
  };

  return (
    <button
      onClick={handleRegisterRedirect}
      className="flex items-center gap-2 px-4 py-2 bg-gray-700 hover:bg-gray-800 text-white text-sm rounded-lg transition"
    >
      <UserPlus size={18} /> Kayıt Ol
    </button>
  );
};

export default RegisterButton;
