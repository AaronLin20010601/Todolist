<template>
    <div class="max-w-md mx-auto mt-10 p-4 border border-gray-300 rounded-lg shadow-lg">
        <h2 class="text-2xl font-bold text-center mb-4">
            {{ step === 1 ? "Send Verification Code" : "Reset Password" }}
        </h2>
    
        <!-- 步驟一：輸入Email送出驗證碼 -->
        <form v-if="step === 1" @submit.prevent="sendVerificationCode">
            <div class="mb-4">
            <label for="email" class="block text-sm font-medium text-gray-700">Email</label>
            <input
                type="email"
                id="email"
                v-model="email"
                required
                class="mt-1 block w-full p-2 border border-gray-300 rounded-md"
                placeholder="Enter your registered email"
            />
            </div>
    
            <div v-if="errorMessage" class="text-red-500 text-sm mb-4">{{ errorMessage }}</div>
    
            <button
                type="submit"
                class="w-full bg-blue-500 text-white p-2 rounded-md hover:bg-blue-600"
            >
            Send Verification Code
            </button>
        </form>
    
        <!-- 步驟二：輸入驗證碼與新密碼 -->
        <form v-else @submit.prevent="resetPassword">
            <div class="mb-4">
                <label for="password" class="block text-sm font-medium text-gray-700">New Password</label>
                <input
                    type="password"
                    id="password"
                    v-model="password"
                    required
                    class="mt-1 block w-full p-2 border border-gray-300 rounded-md"
                    placeholder="Enter your new password"
                />
            </div>
    
            <div class="mb-4">
                <label for="confirmPassword" class="block text-sm font-medium text-gray-700">Confirm Password</label>
                <input
                    type="password"
                    id="confirmPassword"
                    v-model="confirmPassword"
                    required
                    class="mt-1 block w-full p-2 border border-gray-300 rounded-md"
                    placeholder="Re-enter your new password"
                />
            </div>
    
            <div class="mb-4">
                <label for="verificationCode" class="block text-sm font-medium text-gray-700">Verification Code</label>
                <input
                    type="text"
                    id="verificationCode"
                    v-model="verificationCode"
                    required
                    class="mt-1 block w-full p-2 border border-gray-300 rounded-md"
                    placeholder="Enter the code sent to your email"
                />
            </div>
    
            <div v-if="errorMessage" class="text-red-500 text-sm mb-4">{{ errorMessage }}</div>
    
            <button
                type="submit"
                class="w-full bg-green-500 text-white p-2 rounded-md hover:bg-green-600"
            >
            Reset Password
            </button>
        </form>
    </div>
</template>
  
<script>
export default {
    data() {
        return {
            step: 1,
            email: "",
            password: "",
            confirmPassword: "",
            verificationCode: "",
            errorMessage: "",
        };
    },
    methods: {
        async sendVerificationCode() {
            this.errorMessage = "";
            try {
                const response = await this.$http.post(
                    "http://localhost:5000/api/reset/send-verification-code",
                    { email: this.email }
                );
                alert(response.data);
                this.step = 2;
            } catch (error) {
                console.error("Error sending verification code:", error);
                this.errorMessage = error.response?.data || "Failed to send verification code.";
            }
        },
    
        async resetPassword() {
            this.errorMessage = "";
    
            if (this.password !== this.confirmPassword) {
                this.errorMessage = "Passwords do not match.";
                return;
            }
    
            try {
                const payload = {
                    email: this.email,
                    password: this.password,
                    confirmPassword: this.confirmPassword,
                    verificationCode: this.verificationCode,
                };
    
                const response = await this.$http.post(
                    "http://localhost:5000/api/reset/reset-password",
                    payload
            );
                alert(response.data);
                this.$router.push("/"); // 或其他你想導向的頁面
            } catch (error) {
                console.error("Error resetting password:", error);
                this.errorMessage = error.response?.data || "Failed to reset password.";
            }
        },
    },
};
</script>