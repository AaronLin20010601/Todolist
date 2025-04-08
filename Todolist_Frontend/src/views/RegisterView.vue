<template>
    <div>
      <!-- 發送驗證碼表單 -->
      <div v-if="step === 1">
        <h2>Send Verification Code</h2>
        <form @submit.prevent="sendVerificationCode">
          <div>
            <label for="email">Email:</label>
            <input type="email" v-model="email" required />
          </div>
          <button type="submit">Send Verification Code</button>
        </form>
      </div>
  
      <!-- 註冊用戶表單 -->
      <div v-if="step === 2">
        <h2>Register</h2>
        <form @submit.prevent="registerUser">
          <div>
            <label for="username">Username:</label>
            <input type="text" v-model="username" required />
          </div>
          <div>
            <label for="email">Email:</label>
            <input type="email" v-model="email" required disabled />
          </div>
          <div>
            <label for="password">Password:</label>
            <input type="password" v-model="password" required />
          </div>
          <div>
            <label for="confirmPassword">Confirm Password:</label>
            <input type="password" v-model="confirmPassword" required />
          </div>
          <div>
            <label for="verificationCode">Verification Code:</label>
            <input type="text" v-model="verificationCode" required />
          </div>
          <button type="submit">Register</button>
        </form>
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
          // 發送驗證碼請求到後端
          const response = await this.$http.post("http://localhost:5000/api/register/send-verification-code", {email: this.email},{
            headers: {
                'Content-Type': 'application/json'  // 設置 Content-Type 為 application/json
            }});
          alert(response.data); // 顯示訊息，例如 "Verification code sent."
          this.step = 2; // 進入註冊步驟
        } catch (error) {
          console.error("Error sending verification code:", error);
          alert("Failed to send verification code.");
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
  
          // 發送註冊請求到後端
          const response = await this.$http.post("http://localhost:5000/api/register/register", payload);
          alert(response.data); // 顯示註冊成功訊息
          // 可以重定向到其他頁面或顯示註冊成功的提示
        } catch (error) {
          console.error("Error registering user:", error);
          alert("Failed to register user.");
        }
      },
    },
  };
  </script>