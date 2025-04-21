<template>
    <!-- 發送驗證碼表單 -->
    <form @submit.prevent="handleSendCode">
        <!-- Email 欄位-->
        <div class="mb-4">
            <label class="block text-sm font-medium text-gray-700">Email</label>
            <input
                v-model="localEmail" type="email" required placeholder="Enter your email"
                class="mt-1 block w-full p-2 border border-gray-300 rounded-md"
            />
        </div>
        
        <!-- 錯誤消息顯示區域 -->
        <div v-if="errorMessage" class="text-red-500 text-sm mb-4">{{ errorMessage }}</div>
    
        <!-- 發送驗證碼 -->
        <button type="submit" class="w-full bg-blue-500 text-white p-2 rounded-md hover:bg-blue-600">
        Send Verification Code
        </button>
    </form>
</template>
  
<script>
import errorService from '@/service/errorService';

export default {
    props: {
        email: String,
        sendCodeApi: {
            type: Function,
            required: true
        }
    },
    emits: ['success', 'update:email'],
    data() {
        return {
            localEmail: this.email || '',
            errorMessage: ''
        };
    },
    methods: {
        async handleSendCode() {
            try {
                // 發送驗證碼
                const response = await this.sendCodeApi(this.localEmail);
                alert(response);
                this.$emit('update:email', this.localEmail);
                this.$emit('success');
            } catch (error) {
                this.errorMessage = errorService.handleError(error) || 'Failed to send verification code.';
            }
        }
    }
};
</script>