import axios from 'axios'

const API_URL = 'http://localhost:5000/api/account'
const getAuthHeaders = () => ({
    headers: {
        Authorization: `Bearer ${localStorage.getItem('token')}`,
    }
})

// 取得待編輯的 User
export const getAccount = async () => {
    const response = await axios.get(API_URL, getAuthHeaders())
    return response.data
}

// 提交編輯後的 User
export const updateAccount = async (user) => {
    try {
        const response = await axios.patch(API_URL, user, getAuthHeaders())
        return response.data
    }
    catch (error) {
        throw error
    }
}

// 刪除帳號
export const deleteAccount = async () => {
    try {
        const response = await axios.delete(API_URL, getAuthHeaders())
        return response.data
    }
    catch (error) {
        throw error
    }
}