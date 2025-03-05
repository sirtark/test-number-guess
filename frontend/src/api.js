import axios from 'axios';

const api = axios.create({
    baseURL: 'https://localhost:7074',  // Asegurar que es HTTP
    headers: {
        'Content-Type': 'application/json'
    }
});

export default api;
