import { jwtDecode } from 'jwt-decode'
import React from 'react'
import { Navigate } from 'react-router-dom'

function ProtectedAdminRoute({ children }) {
    const token = localStorage.getItem('token')

    if (!token) {
        return <Navigate to="/login" replace />
    }
    const decoded = jwtDecode(token)
    const role = decoded.role
    if (role != 'Admin') {
        return <Navigate to="/*" replace />
    }
  return children
}

export default ProtectedAdminRoute