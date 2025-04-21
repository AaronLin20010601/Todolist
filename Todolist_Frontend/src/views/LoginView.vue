<template>
    <div class="max-w-md mx-auto mt-10 p-4 border border-gray-300 rounded-lg shadow-lg">
        <h2 class="text-2xl font-bold text-center mb-4">Login</h2>
        
        <!-- 登入表單 -->
        <LoginForm @success="handleSuccess"/>
    </div>
</template>

<script>
import LoginForm from '@/components/LoginForm.vue'

export default {
    components: { LoginForm },
    methods: {
        // 登入成功後進入 Todo 首頁
        handleSuccess({ token, user }) {
            this.$store.dispatch('login', { token, user })
            this.$router.push('/todo')
        }
    },
};
</script>