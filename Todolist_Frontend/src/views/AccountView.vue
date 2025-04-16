<template>
    <div class="max-w-4xl mx-auto mt-10 p-4">
        <h1 class="text-3xl font-bold mb-4">Edit Account</h1>
        
        <!-- 編輯 User 表單 -->
        <AccountForm :user="user" @updated="handleUpdate" @deleted="handleDelete" />
    
        <!-- 錯誤消息顯示區域 -->
        <div v-if="errorMessage" class="mt-4 text-red-500"> {{ errorMessage }} </div>
    </div>
</template>

<script>
import AccountForm from '@/components/AccountForm.vue'
import { getAccount } from '@/api/account'

export default {
    components : { AccountForm },
    data() {
        return {
            user: { username: '' },
            errorMessage: '',
        };
    },
    async created() {
        try {
            const response = await getAccount()
            this.user = response
        } catch (error) {
            this.errorMessage = 'Failed to fetch user data.'
        }
    },
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