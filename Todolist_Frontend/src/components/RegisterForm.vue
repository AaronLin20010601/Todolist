<template>
    <!-- 註冊表單 -->
    <form @submit.prevent="handleRegister">
        <div class="mb-4">
            <label class="block text-sm font-medium text-gray-700">Username</label>
            <input
                v-model="username" type="text" required placeholder="Enter your username"
                class="mt-1 block w-full p-2 border border-gray-300 rounded-md"
            />
        </div>
    
        <div class="mb-4">
            <label class="block text-sm font-medium text-gray-700">Email</label>
            <input
                type="email" :value="email" disabled
                class="mt-1 block w-full p-2 border border-gray-300 rounded-md bg-gray-100 cursor-not-allowed"
            />
        </div>
    
        <div class="mb-4">
            <label class="block text-sm font-medium text-gray-700">Password</label>
            <input
                v-model="password" type="password" required placeholder="Enter your password"
                class="mt-1 block w-full p-2 border border-gray-300 rounded-md"
            />
        </div>
    
        <div class="mb-4">
            <label class="block text-sm font-medium text-gray-700">Confirm Password</label>
            <input
                v-model="confirmPassword" type="password" required placeholder="Re-enter your password"
                class="mt-1 block w-full p-2 border border-gray-300 rounded-md"
            />
        </div>
    
        <div class="mb-4">
            <label class="block text-sm font-medium text-gray-700">Verification Code</label>
            <input
                v-model="verificationCode" type="text" required placeholder="Enter the code sent to your email"
                class="mt-1 block w-full p-2 border border-gray-300 rounded-md"
            />
        </div>
        
        <!-- 錯誤消息顯示區域 -->
        <div v-if="errorMessage" class="text-red-500 text-sm mb-4">{{ errorMessage }}</div>
    
        <button type="submit" class="w-full bg-blue-500 text-white p-2 rounded-md hover:bg-blue-600">
        Register
        </button>
    </form>
</template>
  
<script>
import { register } from '@/api/register';
import errorService from '@/service/errorService';
  
export default {
    props: {
        email: {
            type: String,
            required: true
        }
    },
    emits: ['success'],
    data() {
        return {
            username: '',
            password: '',
            confirmPassword: '',
            verificationCode: '',
            errorMessage: ''
        };
    },
    methods: {
        async handleRegister() {   
            try {
                // 送出註冊資料
                const response = await register({
                    username: this.username,
                    email: this.email,
                    password: this.password,
                    confirmPassword: this.confirmPassword,
                    verificationCode: this.verificationCode
                });
    
                alert(response);
                this.$emit('success');
            } catch (error) {
                this.errorMessage = errorService.handleError(error) || 'Failed to register.';
            }
        }
    }
};
</script>