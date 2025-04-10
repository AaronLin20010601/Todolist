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
                // 發送登入請求到後端，並獲取 JWT
                const response = await this.$http.post("http://localhost:5000/api/login", {
                    email: this.email,
                    password: this.password,
                });

                const token = response.data.token;
                const user = response.data.user;

                // 假設後端回應包含 token
                if (token && user) {
                    // 透過 login mutation 更新 Vuex 狀態與 localStorage
                    this.$store.commit("login", { token, user });

                    // 導向首頁
                    this.$router.push("/todo");
                } else {
                    this.errorMessage = response.data.message || "Login failed.";
                }
            } catch (error) {
                console.error("Login error:", error);
                this.errorMessage = error.response.data;
            }
        },
    },
};
</script>