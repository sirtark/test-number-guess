import React, { useState } from 'react'
import api from '../api'
import { useNavigate } from 'react-router-dom'

const Login = () => {
    const navigate = useNavigate()
    const [error, setError] = useState('')

    const handleLogin = async () => {
        try {
            const response = await api.get('/login')
            if (typeof response.data === 'string' && response.data.trim() !== '') {
                localStorage.setItem('sessionToken', response.data)
                navigate('/game')
            } else {
                setError('Error: invalid answer')
            }
        } catch (err) {
            setError('Error: login error: ' + err.message)
        }
    }

    return (
        <div style={{ textAlign: 'center', marginTop: '50px' }}>
            <h1>Login</h1>
            <button onClick={handleLogin}>Login</button>
            {error && <p style={{ color: 'red' }}>{error}</p>}
        </div>
    )
}

export default Login