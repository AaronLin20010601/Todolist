<template>
    <div class="max-w-md mx-auto mt-10 p-4 border border-gray-300 rounded-lg shadow-lg">
      <h2 class="text-2xl font-bold text-center mb-4">Login</h2>
      
      <form @submit.prevent="loginUser">
        <!-- Email 欄位 -->
        <div class="mb-4">
          <label for="email" class="block text-sm font-medium text-gray-700">Email</label>
          <input
            type="email"
            id="email"
            v-model="email"
            required
            class="mt-1 block w-full p-2 border border-gray-300 rounded-md"
            placeholder="Enter your email"
          />
        </div>
  
        <!-- Password 欄位 -->
        <div class="mb-4">
          <label for="password" class="block text-sm font-medium text-gray-700">Password</label>
          <input
            type="password"
            id="password"
            v-model="password"
            required
            class="mt-1 block w-full p-2 border border-gray-300 rounded-md"
            placeholder="Enter your password"
          />
        </div>
  
        <!-- Error 顯示 -->
        <div v-if="errorMessage" class="text-red-500 text-sm mb-4">{{ errorMessage }}</div>
  
        <!-- 登入按鈕 -->
        <button
          type="submit"
          class="w-full bg-blue-500 text-white p-2 rounded-md hover:bg-blue-600"
        >
          Login
        </button>
      </form>
    </div>
  </template>
  
  <script>
  export default {
    data() {
      return {
        email: "", // 用戶輸入的 email
        password: "", // 用戶輸入的密碼
        errorMessage: "", // 錯誤訊息
      };
    },
    methods: {
      async loginUser() {
        // 檢查 email 和密碼的有效性
        if (!this.email || !this.password) {
          this.errorMessage = "Both fields are required.";
          return;
        }
  
        try {
          // 模擬向後端發送登入請求
          const response = await this.$http.post("http://localhost:5000/api/login", {
            email: this.email,
            password: this.password,
          });
  
          // 假設後端回應有 success 和 message
          if (response.data.success) {
            // 登入成功，將用戶資料存儲到 localStorage 或 sessionStorage
            localStorage.setItem("user", JSON.stringify(response.data.user));
            // 進行頁面跳轉
            this.$router.push("/"); // 導向首頁
          } else {
            // 顯示錯誤訊息
            this.errorMessage = response.data.message || "Login failed.";
          }
        } catch (error) {
          console.error("Login error:", error);
          this.errorMessage = "An error occurred. Please try again.";
        }
      },
    },
  };
  </script>
  