<template>
    <div class="max-w-4xl mx-auto mt-10 p-4">
        <h1 class="text-3xl font-bold mb-4">Edit Account</h1>
        
        <!-- 編輯 User 表單 -->
        <form @submit.prevent="updateAccount" class="space-y-6">
            <div>
                <label for="username" class="block text-sm font-medium text-gray-700">Username</label>
                <input
                    v-model="user.username"
                    type="text"
                    id="username"
                    required
                    class="mt-1 block w-full px-4 py-2 border border-gray-300 rounded-md"
                />
            </div>
            
            <div class="flex justify-between">
                <button
                    type="submit"
                    class="bg-blue-500 text-white px-4 py-2 rounded-md hover:bg-blue-600"
                >
                Save Changes
                </button>
                <button
                    type="button"
                    @click="deleteAccount"
                    class="bg-red-500 text-white px-4 py-2 rounded-md hover:bg-red-700"
                >
                Delete Account
                </button>
                <button
                    @click="goToTodoList"
                    type="button"
                    class="bg-gray-500 text-white px-4 py-2 rounded-md hover:bg-gray-600"
                >
                Back to Todo List
                </button>
            </div>
        </form>
    
        <!-- 錯誤消息顯示區域 -->
        <div v-if="errorMessage" class="mt-4 text-red-500">
            {{ errorMessage }}
        </div>
    </div>
</template>

<script>
export default {
    data() {
        return {
            user: {
                username: '',
            },
            errorMessage: '',
        };
    },
    created() {
        this.fetchUser();
    },
    methods: {
        // 取得待編輯的 User
        async fetchUser() {
            try {
                const response = await this.$http.get(`http://localhost:5000/api/account/`, {
                    headers: {
                        Authorization: `Bearer ${localStorage.getItem('token')}`,
                    },
                });

                if (response.status === 200) {
                    const user = response.data;
                    this.user.username = user.username;
                }
            } catch (error) {
                this.errorMessage = 'Failed to fetch user data.';
            }
        },
    
        // 提交編輯後的 Username
        async updateAccount() {
            this.errorMessage = ''; // 清除先前的錯誤消息
    
            // 驗證用戶名是否存在
            if (!this.user.username.trim()) {
                this.errorMessage = 'Username is required.';
                return;
            }

            try {
                const response = await this.$http.patch(`http://localhost:5000/api/account`, this.user, {
                    headers: {
                        Authorization: `Bearer ${localStorage.getItem('token')}`,
                    },
                });

                if (response.status === 200) {
                    // 編輯成功，顯示訊息並返回 Todo 頁面
                    alert('User updated successfully!');
                    this.$router.push('/todo'); // 返回 todo 頁面
                } else {
                    this.errorMessage = response.data.message || 'Failed to update user.';
                }
            } catch (error) {
                console.error('Error updating user:', error);
                this.errorMessage = 'An error occurred while updating user.';
            }
        },

        // 刪除帳號
        async deleteAccount() {
            const confirmDelete = confirm('Are you sure you want to delete your account? This action cannot be undone.');
            if (!confirmDelete) return;

            try {
                const response = await this.$http.delete('http://localhost:5000/api/account', {
                    headers: {
                        Authorization: `Bearer ${localStorage.getItem('token')}`,
                    },
                });

                if (response.status === 200) {
                    // 呼叫 Vuex 的 logout 來同步更新登入狀態
                    this.$store.dispatch('logout');
                    localStorage.removeItem('token');
                    localStorage.removeItem('user');
                    
                    alert('Account deleted successfully.');
                    this.$router.push('/');
                } else {
                    this.errorMessage = response.data.message || 'Failed to delete account.';
                }
            } catch (error) {
                this.errorMessage = 'An error occurred while deleting account.';
            }
        },
    
        // 返回 Todo 頁面
        goToTodoList() {
            this.$router.push('/todo'); // 返回 todo 頁面
        },
    },
};
</script>