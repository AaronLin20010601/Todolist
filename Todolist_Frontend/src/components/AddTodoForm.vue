<template>
    <!-- 新增 Todo 表單 -->
    <form @submit.prevent="onSubmit" class="space-y-6">
        <!-- 標題欄位 -->
        <div>
            <label for="title" class="block text-sm font-medium text-gray-700">Title</label>
            <input 
                v-model="todo.title" type="text" id="title" required
                class="mt-1 block w-full px-4 py-2 border border-gray-300 rounded-md"
            />
        </div>
        
        <!-- 描述欄位 -->
        <div>
            <label for="description" class="block text-sm font-medium text-gray-700">Description</label>
            <textarea 
                v-model="todo.description" id="description" rows="4"
                class="mt-1 block w-full px-4 py-2 border border-gray-300 rounded-md"
            ></textarea>
        </div>
    
        <!-- 截止時間欄位 -->
        <div>
            <label for="dueDate" class="block text-sm font-medium text-gray-700">Due Date</label>
            <input 
                v-model="todo.dueDate" type="datetime-local" id="dueDate"
                class="mt-1 block w-full px-4 py-2 border border-gray-300 rounded-md"
            />
        </div>

        <!-- 錯誤消息顯示區域 -->
        <div v-if="errorMessage" class="mt-4 text-red-500">{{ errorMessage }}</div>
    
        <div class="flex justify-between">
            <!-- 新增 Todo -->
            <button type="submit" class="bg-blue-500 text-white px-4 py-2 rounded-md hover:bg-blue-600">
            Add Todo
            </button>
            
            <!-- 返回 Todo 首頁 -->
            <button @click="$router.push('/todo')" type="button" class="bg-gray-500 text-white px-4 py-2 rounded-md hover:bg-gray-600">
            Back to Todo List
            </button>
        </div>
    </form>
</template>
  
<script>
import { createTodo } from '@/api/todo'
import errorService from '@/service/errorService';
  
export default {
    data() {
        return {
            todo: {
                title: '',
                description: '',
                dueDate: ''
            },
            errorMessage: ''
        }
    },
    methods: {
        // 新增 Todo
        async onSubmit() {
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
    
            try {
                // 新增成功
                await createTodo(this.todo)
                alert('Todo added successfully!')
                this.$emit('success')
            } catch (error) {
                this.errorMessage = errorService.handleError(error) || 'Add todo failed.'
            }
        }
    }
}
</script>