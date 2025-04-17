import axios from 'axios'

const API_URL = 'http://localhost:5000/api/register'

// 發送驗證碼
export const sendVerificationCode = async (email) => {
    const response = await axios.post(`${API_URL}/send-verification-code`, { email })
    return response.data
}

// 送出註冊資料
export const register = async (payload) => {
    const response = await axios.post(`${API_URL}/register`, payload)
    return response.data
}