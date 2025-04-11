<template>
    <div class="max-w-4xl mx-auto mt-10 p-4">
        <h1 class="text-3xl font-bold mb-4">Add New Todo</h1>
        
        <!-- 新增 Todo 表單 -->
        <form @submit.prevent="createTodo" class="space-y-6">
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
                Create Todo
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
            isCompleted: false,
            },
            errorMessage: '',
        };
    },
    methods: {
        // 提交新增 Todo
        async createTodo() {
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

                // 若只選日期（時間為 00:00:00），補上現在的時間
                if (
                dueDateTime.getHours() === 0 &&
                dueDateTime.getMinutes() === 0 &&
                dueDateTime.getSeconds() === 0
                ) {
                dueDateTime.setHours(now.getHours());
                dueDateTime.setMinutes(now.getMinutes());
                dueDateTime.setSeconds(now.getSeconds());
                }
            }

            // 檢查選擇的時間是否早於現在
            if (dueDateTime < now) {
                this.errorMessage = 'Due date and time cannot be in the past.';
                return;
            }

            this.todo.dueDate = dueDateTime.toISOString();

            try {
            const response = await this.$http.post('http://localhost:5000/api/todo', this.todo, {
                headers: {
                    Authorization: `Bearer ${localStorage.getItem('token')}`,
                },
            });
    
            if (response.status === 200) {
                // 創建成功，顯示訊息並返回 Todo 頁面
                alert('Todo created successfully!');
                this.$router.push('/todo'); // 假設返回 todo 頁面
            } else {
                this.errorMessage = response.data.message || 'Failed to create todo.';
            }
            } catch (error) {
                console.error('Error creating todo:', error);
                this.errorMessage = 'An error occurred while creating the todo.';
            }
        },
    
        // 返回 Todo 頁面
        goToTodoList() {
            this.$router.push('/todo'); // 假設返回 todo 頁面
        },
    },
};
</script>