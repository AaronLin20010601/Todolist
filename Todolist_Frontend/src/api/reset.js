import axios from 'axios'

const API_URL = 'http://localhost:5000/api/reset'

// 發送驗證碼
export const sendVerificationCode = async (email) => {
    const response = await axios.post(`${API_URL}/send-verification-code`, { email })
    return response.data
}

// 送出重設密碼
export const reset = async (payload) => {
    const response = await axios.post(`${API_URL}/reset-password`, payload)
    return response.data
}