<template>
    <div class="max-w-md mx-auto mt-10 p-4 border border-gray-300 rounded-lg shadow-lg">
        <h2 class="text-2xl font-bold text-center mb-4">
            {{ step === 1 ? "Send Verification Code" : "Reset Password" }}
        </h2>
    
        <!-- 發送驗證碼表單 -->
        <VerificationForm
            v-if="step === 1" :email="email" :sendCodeApi="sendVerificationCode"
            @update:email="email = $event" @success="step = 2"
        />

        <!-- 重設密碼表單 -->
        <ResetForm v-else :email="email" @success="handleSuccess"/>
    </div>
</template>
  
<script>
import VerificationForm from '@/components/VerificationForm.vue';
import ResetForm from '@/components/ResetForm.vue';
import { sendVerificationCode } from '@/api/reset';

export default {
    components: {
        VerificationForm,
        ResetForm
    },
    data() {
        return {
            step: 1,
            email: '',
        };
    },
    methods: {
        sendVerificationCode,
        // 重設成功後前往登入頁面
        handleSuccess() {
            this.$router.push('/')
        },
    },
};
</script>