<template>
    <!-- 編輯 Todo 表單 -->
    <form @submit.prevent="onSubmit" class="space-y-6">
        <div>
            <label for="title" class="block text-sm font-medium text-gray-700">Title</label>
            <input
                v-model="todo.title" type="text" id="title" required
                class="mt-1 block w-full px-4 py-2 border border-gray-300 rounded-md" 
            />
        </div>
    
        <div>
            <label for="description" class="block text-sm font-medium text-gray-700">Description</label>
            <textarea 
                v-model="todo.description" id="description" rows="4"
                class="mt-1 block w-full px-4 py-2 border border-gray-300 rounded-md"
            ></textarea>
        </div>
    
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
            <button type="submit" class="bg-blue-500 text-white px-4 py-2 rounded-md hover:bg-blue-600">
            Save Changes
            </button>
    
            <button @click="$router.push('/todo')" type="button" class="bg-gray-500 text-white px-4 py-2 rounded-md hover:bg-gray-600">
            Back to Todo List
            </button>
        </div>
    </form>
</template>
  
<script>
import { getEditTodo, updateTodo } from '@/api/todo'
import errorService from '@/service/errorService';
  
export default {
    props: ['todoId'],
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
    async created() {
        try {
            const result = await getEditTodo(this.todoId)
            this.todo = {
                title: result.title,
                description: result.description,
                dueDate: result.dueDate ? this.formatLocalDateTime(result.dueDate) : '',
            }
        } catch (error) {
            this.errorMessage = errorService.handleError(error) || 'Failed to fetch todo data.'
        }
    },
    methods: {
        // 將時間轉換成時區時間
        formatLocalDateTime(isoString) {
            const date = new Date(isoString);
            const year = date.getFullYear();
            const month = String(date.getMonth() + 1).padStart(2, '0');
            const day = String(date.getDate()).padStart(2, '0');
            const hours = String(date.getHours()).padStart(2, '0');
            const minutes = String(date.getMinutes()).padStart(2, '0');
            return `${year}-${month}-${day}T${hours}:${minutes}`;
        },
        // 提交編輯後的 Todo
        async onSubmit() {   
            let now = new Date()
            let dueDateTime = this.todo.dueDate ? new Date(this.todo.dueDate) : new Date(now.getTime() + 86400000)

            // 檢查選擇的時間是否早於現在
            if (dueDateTime < now) {
                this.errorMessage = 'Due date and time cannot be in the past.';
                return;
            }
            this.todo.dueDate = dueDateTime.toISOString()
    
            try {
                // 更新成功
                await updateTodo(this.todoId, this.todo)
                alert('Todo updated successfully!')
                this.$emit('success')
            } catch (error) {
                this.errorMessage = errorService.handleError(error) || 'An error occurred while updating the todo.'
            }
        }
    }
}
</script>