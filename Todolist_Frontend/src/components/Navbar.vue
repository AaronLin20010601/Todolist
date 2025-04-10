<!--網頁導覽列-->
<template>
    <nav class="bg-blue-600 text-white px-6 py-4 shadow-md">
        <div class="container mx-auto flex justify-between items-center">
        <h1 class="text-xl font-bold">📝 My Todo List</h1>
        <ul class="flex space-x-6">
            <li v-if="!isLoggedIn">
                <RouterLink to="/" class="hover:text-yellow-300" active-class="underline">Login</RouterLink>
            </li>
            <li v-if="!isLoggedIn">
                <RouterLink to="/register" class="hover:text-yellow-300" active-class="underline">Register</RouterLink>
            </li>
            <li v-if="!isLoggedIn">
                <RouterLink to="/reset" class="hover:text-yellow-300" active-class="underline">Reset Password</RouterLink>
            </li>
            <li v-if="isLoggedIn">
                <RouterLink to="/todo" class="hover:text-yellow-300" active-class="underline">Todolist</RouterLink>
            </li>
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
            // 重定向到登入頁
            router.push("/")
        }

        return {
            isLoggedIn,
            logout,
        }
    },
}
</script>