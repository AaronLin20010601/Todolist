<template>
    <!-- 重設密碼表單 -->
    <form @submit.prevent="handleReset">
        <!-- 密碼欄位 -->
        <div class="mb-4">
            <label class="block text-sm font-medium text-gray-700">New Password</label>
            <input
                v-model="password" type="password" required placeholder="Enter your new password"
                class="mt-1 block w-full p-2 border border-gray-300 rounded-md"
            />
        </div>
    
        <!-- 確認密碼欄位 -->
        <div class="mb-4">
            <label class="block text-sm font-medium text-gray-700">Confirm Password</label>
            <input
                v-model="confirmPassword" type="password" required placeholder="Re-enter your new password"
                class="mt-1 block w-full p-2 border border-gray-300 rounded-md"
            />
        </div>
    
        <!-- 驗證碼欄位 -->
        <div class="mb-4">
            <label class="block text-sm font-medium text-gray-700">Verification Code</label>
            <input
                v-model="verificationCode" type="text" required placeholder="Enter the code sent to your email"
                class="mt-1 block w-full p-2 border border-gray-300 rounded-md"
            />
        </div>
        
        <!-- 錯誤消息顯示區域 -->
        <div v-if="errorMessage" class="text-red-500 text-sm mb-4">{{ errorMessage }}</div>
        
        <!-- 重設密碼 -->
        <button type="submit" class="w-full bg-green-500 text-white p-2 rounded-md hover:bg-green-600">
        Reset Password
        </button>
    </form>
</template>

<script>
import { reset } from '@/api/reset';
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
            password: '',
            confirmPassword: '',
            verificationCode: '',
            errorMessage: ''
        };
    },
    methods: {
        async handleReset() {    
            try {
                // 送出重設資料
                const response = await reset({
                    email: this.email,
                    password: this.password,
                    confirmPassword: this.confirmPassword,
                    verificationCode: this.verificationCode
                });
    
                alert(response);
                this.$emit('success');
            } catch (error) {
                this.errorMessage = errorService.handleError(error) || 'Failed to reset.';
            }
        }
    }
};
</script>