<template>
    <form @submit.prevent="loginUser">
        <!-- Email 欄位 -->
        <div class="mb-4">
            <label for="email" class="block text-sm font-medium text-gray-700">Email</label>
            <input
                type="email" id="email" v-model="email" required placeholder="Enter your email"
                class="mt-1 block w-full p-2 border border-gray-300 rounded-md"
            />
        </div>
    
        <!-- Password 欄位 -->
        <div class="mb-4">
            <label for="password" class="block text-sm font-medium text-gray-700">Password</label>
            <input
                type="password" id="password" v-model="password" required placeholder="Enter your password"
                class="mt-1 block w-full p-2 border border-gray-300 rounded-md"
            />
        </div>
    
        <!-- 登入按鈕 -->
        <button type="submit" class="w-full bg-blue-500 text-white p-2 rounded-md hover:bg-blue-600">
        Login
        </button>
    </form>
</template>
  
<script>
import { login } from '@/api/login'
  
export default {
    data() {
        return {
            email: '',
            password: ''
        }
    },
    methods: {
        async loginUser() {
            // 檢查 email 和密碼的有效性
            if (!this.email || !this.password) {
                this.$emit('error', 'Both fields are required.')
                return
            }
    
            try {
                // 發送登入請求到後端，並獲取 JWT
                const { token, user, message } = await login(this.email, this.password)
        
                if (token && user) {
                    // 登入成功
                    this.$emit('success', { token, user })
                } else {
                    this.$emit('error', message || 'Login failed.')
                }
            } catch (error) {
                console.error('Login error:', error)
                this.$emit('error', error.response?.data || 'Login failed.')
            }
        }
    }
}
</script>