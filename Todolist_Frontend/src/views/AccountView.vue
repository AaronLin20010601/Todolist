<template>
    <div class="max-w-4xl mx-auto mt-10 p-4">
        <h1 class="text-3xl font-bold mb-4">Edit Account</h1>
        
        <!-- 編輯 User 表單 -->
        <AccountForm @updated="handleUpdate" @deleted="handleDelete"/>
    </div>
</template>

<script>
import AccountForm from '@/components/AccountForm.vue'

export default {
    components : { AccountForm },
    methods: {
        // 更新後返回 Todo
        handleUpdate() {
            this.$router.push('/todo')
        },
        // 刪除後清除 token 並返回登入頁面
        handleDelete() {
            this.$store.dispatch('logout')
            localStorage.removeItem('token')
            localStorage.removeItem('user')
            this.$router.push('/')
        }
    },
};
</script>