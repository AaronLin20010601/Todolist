import axios from 'axios'

const API_URL = 'http://localhost:5000/api/register'

// 發送驗證碼
export const sendVerificationCode = async (email) => {
    try {
        const response = await axios.post(`${API_URL}/verification-code`, { email })
        return response.data
    }
    catch (error) {
        throw error
    }
}

// 送出註冊資料
export const register = async (payload) => {
    try {
        const response = await axios.post(API_URL, payload)
        return response.data
    }
    catch (error) {
        throw error
    }
}