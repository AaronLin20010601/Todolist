import axios from 'axios'

const API_URL = 'http://localhost:5000/api/login'

// 發送登入請求到後端，並獲取 JWT
export const login = async (email, password) => {
    const response = await axios.post(API_URL, { email, password })
    return response.data
}