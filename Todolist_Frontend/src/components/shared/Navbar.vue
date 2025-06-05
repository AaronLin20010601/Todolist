<!-- 網頁導覽列 -->
<template>
    <nav class="bg-blue-600 text-white px-6 py-4 shadow-md">
        <div class="container mx-auto flex justify-between items-center">
            <h1 class="text-xl font-bold">📝 My Todo List</h1>
            <ul class="flex space-x-6">
                <!-- 登入 -->
                <li v-if="!isLoggedIn">
                    <RouterLink to="/login" class="hover:text-yellow-300" active-class="underline">Login</RouterLink>
                </li>
                <!-- 註冊 -->
                <li v-if="!isLoggedIn">
                    <RouterLink to="/register" class="hover:text-yellow-300" active-class="underline">Register</RouterLink>
                </li>
                <!-- 重設密碼 -->
                <li v-if="!isLoggedIn">
                    <RouterLink to="/reset" class="hover:text-yellow-300" active-class="underline">Reset Password</RouterLink>
                </li>
                <!-- Todo 首頁 -->
                <li v-if="isLoggedIn">
                    <RouterLink to="/todo" class="hover:text-yellow-300" active-class="underline">Todolist</RouterLink>
                </li>
                <!-- 帳號編輯 -->
                <li v-if="isLoggedIn">
                    <RouterLink to="/account" class="hover:text-yellow-300" active-class="underline">Account</RouterLink>
                </li>
                <!-- 登出 -->
                <li v-if="isLoggedIn">
                    <button @click="logout" class="hover:text-yellow-300">Logout</button>
                </li>
            </ul>
        </div>
    </nav>
</template>
  
<script>
import { computed } from 'vue'
import { useRouter } from 'vue-router'
import { useStore } from 'vuex'
export default {
    setup() {
        const router = useRouter()
        const store = useStore()
        // 讀取 token，判斷是否已登入
        const isLoggedIn = computed(() => store.getters.isLoggedIn)

        // 登出功能
        const logout = () => {
            // 清除 token
            store.dispatch('logout')
            // 清除 localStorage
            localStorage.removeItem('token');
            localStorage.removeItem('user');
            // 重定向到登入頁
            router.push('/')
        }

        return {
            isLoggedIn,
            logout,
        }
    },
}
</script>