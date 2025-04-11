<template>
    <div class="max-w-4xl mx-auto mt-10 p-4">
        <h1 class="text-3xl font-bold mb-4">Edit Todo</h1>
        
        <!-- 編輯 Todo 表單 -->
        <form @submit.prevent="editTodo" class="space-y-6">
            <div>
                <label for="title" class="block text-sm font-medium text-gray-700">Title</label>
                <input
                    v-model="todo.title"
                    type="text"
                    id="title"
                    required
                    class="mt-1 block w-full px-4 py-2 border border-gray-300 rounded-md"
                />
            </div>
            
            <div>
                <label for="description" class="block text-sm font-medium text-gray-700">Description</label>
                <textarea
                    v-model="todo.description"
                    id="description"
                    rows="4"
                    class="mt-1 block w-full px-4 py-2 border border-gray-300 rounded-md"
                ></textarea>
            </div>
            
            <div>
                <label for="dueDate" class="block text-sm font-medium text-gray-700">Due Date</label>
                <input
                    v-model="todo.dueDate"
                    type="datetime-local"
                    id="dueDate"
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
            todo: {
                title: '',
                description: '',
                dueDate: '',
            },
            errorMessage: '',
        };
    },
    created() {
        this.fetchTodo();
    },
    methods: {
        // 取得待編輯的 Todo
        async fetchTodo() {
            const todoId = this.$route.params.id;
            try {
                const response = await this.$http.get(`http://localhost:5000/api/todo/${todoId}`, {
                    headers: {
                        Authorization: `Bearer ${localStorage.getItem('token')}`,
                    },
                });

                if (response.status === 200) {
                    const todo = response.data;
                    this.todo.title = todo.title;
                    this.todo.description = todo.description;
                    this.todo.dueDate = todo.dueDate ? new Date(todo.dueDate).toISOString().slice(0, 16) : '';
                }
            } catch (error) {
                this.errorMessage = 'Failed to fetch todo data.';
            }
        },
    
        // 提交編輯後的 Todo
        async editTodo() {
            this.errorMessage = ''; // 清除先前的錯誤消息
    
            // 驗證標題是否存在
            if (!this.todo.title.trim()) {
                this.errorMessage = 'Title is required.';
                return;
            }

            let now = new Date();
            let dueDateTime;

            if (!this.todo.dueDate) {
                // 沒選擇：預設為「現在 + 1 天」
                dueDateTime = new Date(now.getTime() + 24 * 60 * 60 * 1000);
            } else {
                // 有選擇：轉換為 Date 物件
                dueDateTime = new Date(this.todo.dueDate);
            }

            // 檢查選擇的時間是否早於現在
            if (dueDateTime < now) {
                this.errorMessage = 'Due date and time cannot be in the past.';
                return;
            }

            this.todo.dueDate = dueDateTime.toISOString();

            const todoId = this.$route.params.id;

            try {
                const response = await this.$http.patch(`http://localhost:5000/api/todo/${todoId}`, this.todo, {
                    headers: {
                        Authorization: `Bearer ${localStorage.getItem('token')}`,
                    },
                });

                if (response.status === 200) {
                    // 編輯成功，顯示訊息並返回 Todo 頁面
                    alert('Todo updated successfully!');
                    this.$router.push('/todo'); // 假設返回 todo 頁面
                } else {
                    this.errorMessage = response.data.message || 'Failed to update todo.';
                }
            } catch (error) {
                console.error('Error updating todo:', error);
                this.errorMessage = 'An error occurred while updating the todo.';
            }
        },
    
        // 返回 Todo 頁面
        goToTodoList() {
            this.$router.push('/todo'); // 假設返回 todo 頁面
        },
    },
};
</script>