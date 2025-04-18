import axios from 'axios'

const API_URL = 'http://localhost:5000/api/todo'
const getAuthHeaders = () => ({
    headers: {
        Authorization: `Bearer ${localStorage.getItem('token')}`,
    }
})

// 取得 Todo 列表
export const fetchTodos = async (filter, page, pageSize) => {
    const response = await axios.get(`${API_URL}?filter=${filter}&page=${page}&pageSize=${pageSize}`, getAuthHeaders())
    return response.data
}

// 更新 Todo 完成狀態
export const toggleTodoComplete = async (id, isCompleted) => {
    const response = await axios.patch(`${API_URL}/${id}/complete`, { isCompleted }, getAuthHeaders())
    return response.data
}

// 建立新的 Todo
export const createTodo = async (todo) => {
    try {
        const response = await axios.post(API_URL, todo, getAuthHeaders())
        return response.data
    }
    catch (error) {
        throw error
    }
}

// 取得待編輯的 Todo
export const getEditTodo = async (id) => {
    const response = await axios.get(`${API_URL}/${id}`, getAuthHeaders())
    return response.data
}

// 更新 Todo
export const updateTodo = async (id, todo) => {
    try {
        const response = await axios.patch(`${API_URL}/${id}`, todo, getAuthHeaders())
        return response.data
    }
    catch (error) {
        throw error
    }
}

// 刪除 Todo
export const deleteTodo = async (id) => {
    const response = await axios.delete(`${API_URL}/${id}`, getAuthHeaders())
    return response.data
}