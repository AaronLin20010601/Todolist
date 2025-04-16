<template>
    <!-- 編輯 User 表單 -->
    <form @submit.prevent="onSubmit" class="space-y-6">
        <div>
            <label for="username" class="block text-sm font-medium text-gray-700">Username</label>
            <input 
                v-model="localUser.username" type="text" id="username" required
                class="mt-1 block w-full px-4 py-2 border border-gray-300 rounded-md" 
            />
        </div>
    
        <div class="flex justify-between">
            <button type="submit" class="bg-blue-500 text-white px-4 py-2 rounded-md hover:bg-blue-600">
            Save Changes
            </button>

            <button @click="onDelete" type="button" class="bg-red-500 text-white px-4 py-2 rounded-md hover:bg-red-700">
            Delete Account
            </button>

            <button @click="$router.push('/todo')" type="button" class="bg-gray-500 text-white px-4 py-2 rounded-md hover:bg-gray-600">
            Back to Todo List
            </button>
        </div>
    </form>
</template>
  
<script>
import { updateAccount, deleteAccount } from '@/api/account'
  
export default {
    props: ['user'],
    data() {
        return {
            localUser: { ...this.user },
        }
    },
    watch: {
        user: {
            handler(newVal) {
                this.localUser = { ...newVal }
            },
            immediate: true,
            deep: true
        }
    },
    methods: {
        // 更新 User
        async onSubmit() {
            // 驗證用戶名是否存在
            if (!this.localUser.username.trim()) {
                alert('Username is required.')
                return
            }
    
            try {
                // 編輯成功
                await updateAccount(this.localUser)
                alert('User updated successfully!')
                this.$emit('updated')
            } catch (error) {
                alert(error.message || 'Update failed.')
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
                alert(error.message || 'Delete failed.')
            }
        }
    }
}
</script>