<template>
  <div class="min-h-screen flex items-center justify-center bg-gray-50">
    <div class="max-w-md w-full bg-white p-6 rounded-lg shadow-lg">
      <!-- 發送驗證碼表單 -->
      <div v-if="step === 1">
        <h2 class="text-2xl font-semibold text-center mb-6">Send Verification Code</h2>
        <form @submit.prevent="sendVerificationCode">
          <div class="mb-4">
            <label for="email" class="block text-sm font-medium text-gray-700">Email:</label>
            <input
              type="email"
              v-model="email"
              required
              class="mt-1 p-2 w-full border border-gray-300 rounded-md focus:outline-none focus:ring-2 focus:ring-blue-500"
            />
          </div>
          <button
            type="submit"
            class="w-full bg-blue-600 text-white p-2 rounded-md hover:bg-blue-700 focus:outline-none"
          >
            Send Verification Code
          </button>
        </form>
      </div>

      <!-- 註冊用戶表單 -->
      <div v-if="step === 2">
        <h2 class="text-2xl font-semibold text-center mb-6">Register</h2>
        <form @submit.prevent="registerUser">
          <div class="mb-4">
            <label for="username" class="block text-sm font-medium text-gray-700">Username:</label>
            <input
              type="text"
              v-model="username"
              required
              class="mt-1 p-2 w-full border border-gray-300 rounded-md focus:outline-none focus:ring-2 focus:ring-blue-500"
            />
          </div>
          <div class="mb-4">
            <label for="email" class="block text-sm font-medium text-gray-700">Email:</label>
            <input
              type="email"
              v-model="email"
              required
              disabled
              class="mt-1 p-2 w-full border border-gray-300 rounded-md bg-gray-100 cursor-not-allowed"
            />
          </div>
          <div class="mb-4">
            <label for="password" class="block text-sm font-medium text-gray-700">Password:</label>
            <input
              type="password"
              v-model="password"
              required
              class="mt-1 p-2 w-full border border-gray-300 rounded-md focus:outline-none focus:ring-2 focus:ring-blue-500"
            />
          </div>
          <div class="mb-4">
            <label for="confirmPassword" class="block text-sm font-medium text-gray-700">Confirm Password:</label>
            <input
              type="password"
              v-model="confirmPassword"
              required
              class="mt-1 p-2 w-full border border-gray-300 rounded-md focus:outline-none focus:ring-2 focus:ring-blue-500"
            />
          </div>
          <div class="mb-4">
            <label for="verificationCode" class="block text-sm font-medium text-gray-700">Verification Code:</label>
            <input
              type="text"
              v-model="verificationCode"
              required
              class="mt-1 p-2 w-full border border-gray-300 rounded-md focus:outline-none focus:ring-2 focus:ring-blue-500"
            />
          </div>
          <button
            type="submit"
            class="w-full bg-blue-600 text-white p-2 rounded-md hover:bg-blue-700 focus:outline-none"
          >
            Register
          </button>
        </form>
      </div>
    </div>
  </div>
</template>

<script>
export default {
  data() {
    return {
      step: 1, // 控制步驟 1 或 2
      email: "",
      username: "",
      password: "",
      confirmPassword: "",
      verificationCode: "",
    };
  },
  methods: {
    async sendVerificationCode() {
      try {
        const response = await this.$http.post(
          "http://localhost:5000/api/register/send-verification-code",
          { email: this.email },
          {
            headers: {
              "Content-Type": "application/json", // 設置 Content-Type 為 application/json
            },
          }
        );
        alert(response.data); // 顯示訊息，例如 "Verification code sent."
        this.step = 2; // 進入註冊步驟
      } catch (error) {
        console.error("Error sending verification code:", error);
        if (error.response && error.response.data) {
          alert(error.response.data);
        } else {
          alert("Failed to send verification code.");
        }
      }
    },

    async registerUser() {
      // 檢查密碼和確認密碼是否一致
      if (this.password !== this.confirmPassword) {
        alert("Passwords do not match.");
        return;
      }

      try {
        const payload = {
          email: this.email,
          username: this.username,
          password: this.password,
          confirmPassword: this.confirmPassword,
          verificationCode: this.verificationCode,
        };

        const response = await this.$http.post(
          "http://localhost:5000/api/register/register",
          payload
        );
        alert(response.data); // 顯示註冊成功訊息
        this.$router.push("/"); // 註冊成功後跳轉到登入頁面
      } catch (error) {
        console.error("Error registering user:", error);
        if (error.response && error.response.data) {
          alert(error.response.data);
        } else {
          alert("Failed to register user.");
        }
      }
    },
  },
};
</script>