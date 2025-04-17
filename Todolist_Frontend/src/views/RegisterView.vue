<template>
    <div class="max-w-md mx-auto mt-10 p-4 border border-gray-300 rounded-lg shadow-lg">
        <h2 class="text-2xl font-bold text-center mb-4">
            {{ step === 1 ? "Send Verification Code" : "Register" }}
        </h2>

        <!-- 發送驗證碼表單 -->
        <VerificationForm
            v-if="step === 1" :email="email" :sendCodeApi="sendVerificationCode"
            @update:email="email = $event" @success="step = 2"
        />

        <!-- 註冊表單 -->
        <RegisterForm v-else :email="email" @success="handleSuccess"/>
    </div>
</template>

<script>
import VerificationForm from "@/components/VerificationForm.vue";
import RegisterForm from "@/components/RegisterForm.vue";
import { sendVerificationCode } from "@/api/register";

export default {
    components: {
        VerificationForm,
        RegisterForm
    },
    data() {
        return {
            step: 1,
            email: "",
        };
    },
    methods: {
        sendVerificationCode,
        // 註冊成功後前往登入頁面
        handleSuccess() {
            this.$router.push('/')
        },
    },
};
</script>