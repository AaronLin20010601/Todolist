<template>
    <!-- 編輯 User 表單 -->
    <form @submit.prevent="onSubmit" class="space-y-6">
        <!-- 使用者名稱欄位 -->
        <div>
            <label for="username" class="block text-sm font-medium text-gray-700">Username</label>
            <input 
                v-model="user.username" type="text" id="username" required
                class="mt-1 block w-full px-4 py-2 border border-gray-300 rounded-md" 
            />
        </div>

        <!-- 錯誤消息顯示區域 -->
        <div v-if="errorMessage" class="mt-4 text-red-500">{{ errorMessage }}</div>
    
        <div class="flex justify-between">
            <!-- 儲存變更 -->
            <button type="submit" class="bg-blue-500 text-white px-4 py-2 rounded-md hover:bg-blue-600">
            Save Changes
            </button>
            
            <!-- 刪除帳號 -->
            <button @click="onDelete" type="button" class="bg-red-500 text-white px-4 py-2 rounded-md hover:bg-red-700">
            Delete Account
            </button>
            
            <!-- 返回 Todo 首頁 -->
            <button @click="$router.push('/todo')" type="button" class="bg-gray-500 text-white px-4 py-2 rounded-md hover:bg-gray-600">
            Back to Todo List
            </button>
        </div>
    </form>
</template>
  
<script>
import { getAccount, updateAccount, deleteAccount } from '@/api/account'
import errorService from '@/service/errorService';
  
export default {
    data() {
        return {
            user: {
                username: '',
            },
            errorMessage: ''
        }
    },
    async created() {
        // 取得 User 資料
        try {
            const response = await getAccount()
            this.user = response
        } catch (error) {
            this.errorMessage = errorService.handleError(error) || 'Failed to fetch user data.'
        }
    },
    methods: {
        // 更新 User
        async onSubmit() {
            try {
                // 編輯成功
                await updateAccount(this.user)
                alert('User updated successfully!')
                this.$emit('updated')
            } catch (error) {
                this.errorMessage = errorService.handleError(error) || 'Failed to update user.';
            }
        },
        // 刪除 User
        async onDelete() {
            if (!confirm('Are you sure you want to delete your account?')) {
                return
            }
    
            try {
                // 刪除成功
                await deleteAccount()
                alert('Account deleted successfully.')
                this.$emit('deleted')
            } catch (error) {
                this.errorMessage = errorService.handleError(error) || 'Delete failed.'
            }
        }
    }
}
</script>