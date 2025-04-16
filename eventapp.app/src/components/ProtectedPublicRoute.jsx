import React from 'react'
import { Navigate } from 'react-router-dom'

const ProtectedPublicRoute = ({ children }) => {
  const token = localStorage.getItem('token')

  if (token) {
    return <Navigate to="/dashboard" replace />
  }

  return children
}

export default ProtectedPublicRoute
