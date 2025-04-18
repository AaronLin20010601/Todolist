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
        
        <!-- 錯誤消息顯示區域 -->
        <div v-if="errorMessage" class="text-red-500 text-sm mt-4">{{ errorMessage }}</div>

        <!-- 登入按鈕 -->
        <button type="submit" class="w-full bg-blue-500 text-white p-2 rounded-md hover:bg-blue-600">
        Login
        </button>
    </form>
</template>
  
<script>
import { login } from '@/api/login'
import errorService from '@/service/errorService';
  
export default {
    data() {
        return {
            email: '',
            password: '',
            errorMessage: ''
        }
    },
    methods: {
        async loginUser() {   
            try {
                // 發送登入請求到後端，並獲取 JWT
                const { token, user, message } = await login(this.email, this.password)
        
                if (token && user) {
                    // 登入成功
                    this.$emit('success', { token, user })
                }
            } catch (error) {
                this.errorMessage = errorService.handleError(error) || 'Login failed.';
            }
        }
    }
}
</script>