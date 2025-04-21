<template>
    <div>
        <!-- Todo 列表 當沒有任何 Todo 時顯示 "No Todos" -->
        <div v-if="todos.length === 0" class="text-center text-gray-500 p-8 border border-gray-200 rounded-lg">
            No Todos of this status available.
        </div>
        
        <!-- Todo 列表表格 僅在有 Todo 時顯示 -->
        <table v-else class="w-full table-auto">
            <thead>
                <tr class="border-b">
                    <th class="px-4 py-2 text-left w-12"></th>
                    <th class="px-4 py-2 text-left w-[200px]">Title</th>
                    <th class="px-4 py-2 text-left w-[300px]">Description</th>
                    <th class="px-4 py-2 text-left w-[100px]">Status</th>
                    <th class="px-4 py-2 text-left w-[160px]">Created At</th>
                    <th class="px-4 py-2 text-left w-[160px]">Due Date</th>
                    <th class="px-4 py-2 text-left">Actions</th>
                </tr>
            </thead>
            <tbody>
                <tr v-for="todo in todos" :key="todo.id" class="border-b">
                    <!-- 變更完成狀態勾選欄 -->
                    <td class="px-4 py-2 w-12">
                        <input
                            type="checkbox" :checked="todo.isCompleted" @change="toggleStatus(todo)"
                            :disabled="new Date(todo.dueDate).getTime() < Date.now()"
                            class="w-5 h-5 text-green-600 bg-gray-100 border-gray-300 rounded focus:ring-green-500"
                        />
                    </td>
                    
                    <!-- Todo 標題 -->
                    <td class="px-4 py-2">{{ todo.title }}</td>
                    <!-- Todo 描述 -->
                    <td class="px-4 py-2">{{ todo.description }}</td>
                    <!-- 完成狀態 -->
                    <td class="px-4 py-2" :class="statusClass(todo.status)">{{ todo.status }}</td>
                    <!-- 建立時間 -->
                    <td class="px-4 py-2">{{ formatDate(todo.createdAt) }}</td>
                    <!-- 截止時間 -->
                    <td class="px-4 py-2">{{ formatDate(todo.dueDate) }}</td>
                    
                    <!-- 操作欄 -->
                    <td class="px-4 py-2 flex space-x-2">
                        <!-- 前往編輯 Todo -->
                        <button @click="editTodo(todo)" class="bg-yellow-500 text-white px-3 py-1 rounded-md hover:bg-yellow-600">
                            Edit
                        </button>
                        
                        <!-- 刪除 Todo -->
                        <button @click="removeTodo(todo)" class="bg-red-500 text-white px-3 py-1 rounded-md hover:bg-red-700">
                            Delete
                        </button>
                    </td>
                </tr>
            </tbody>
        </table>
    </div>
</template>
  
<script>
import { deleteTodo, toggleTodoComplete } from '@/api/todo';
  
export default {
    props: {
        todos: Array,
    },
    emits: ['reload'],
    methods: {
        // 根據 Todo 狀態給予不同的顏色
        statusClass(status) {
            switch (status) {
                case 'completed': return 'text-green-600 font-bold';
                case 'incomplete': return 'text-orange-500 font-bold';
                case 'expired': return 'text-red-500 font-bold';
                default: return '';
            }
        },
        // 格式化日期
        formatDate(date) {
            return new Date(date).toLocaleString();
        },
        // 編輯 Todo
        editTodo(todo) {
            this.$router.push(`/edittodo/${todo.id}`);
        },
        // 刪除 Todo
        async removeTodo(todo) {
            if (!confirm('Are you sure you want to delete this todo?')) return;
            await deleteTodo(todo.id);
            this.$emit('reload');
        },
        // 更新 Todo 完成狀態
        async toggleStatus(todo) {
            await toggleTodoComplete(todo.id, !todo.isCompleted);
            this.$emit('reload');
        },
    },
};
</script>