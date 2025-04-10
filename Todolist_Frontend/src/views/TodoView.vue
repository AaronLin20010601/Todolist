<template>
    <div class="max-w-4xl mx-auto mt-10 p-4">
        <h1 class="text-3xl font-bold mb-4">Todo List</h1>
        <p class="mb-4">Here is your todo list.</p>
    
        <!-- 篩選和新增按鈕區塊 -->
        <div class="flex justify-between items-center mb-6">
        <!-- 篩選狀態下拉選單 和 新增 Todo 按鈕並排 -->
        <div class="flex items-center space-x-4">
            <!-- 篩選下拉選單 -->
            <label for="filter" class="mr-2">Filter Status:</label>
            <select
            v-model="filter"
            @change="getTodos"
            class="p-2 border border-gray-300 rounded-md"
            >
                <option value="all">All</option>
                <option value="completed">Completed</option>
                <option value="incomplete">Incomplete</option>
                <option value="expired">Expired</option>
            </select>
    
            <!-- 新增 Todo 按鈕 -->
            <button
                @click="goToAddTodo"
                class="ml-2 bg-blue-500 text-white p-2 rounded-md hover:bg-blue-600"
            >
            Add Todo
            </button>
        </div>
        </div>
    
        <!-- Todo 列表，當沒有任何 Todo 時顯示 "No Todos" -->
        <div v-if="todos.length === 0" class="text-center text-gray-500">
            No Todos available. Start by adding a new todo!
        </div>
    
        <!-- Todo 列表表格，僅在有 Todo 時顯示 -->
        <table v-if="todos.length > 0" class="w-full table-auto">
            <thead>
                <tr class="border-b">
                    <th class="px-4 py-2 text-left">Title</th>
                    <th class="px-4 py-2 text-left">Description</th>
                    <th class="px-4 py-2 text-left">Status</th>
                    <th class="px-4 py-2 text-left">Created At</th>
                    <th class="px-4 py-2 text-left">Due Date</th>
                    <th class="px-4 py-2 text-left">Actions</th>
                </tr>
            </thead>
            <tbody>
                <tr
                    v-for="todo in todos"
                    :key="todo.id"
                    class="border-b"
                >
                    <td class="px-4 py-2">{{ todo.title }}</td>
                    <td class="px-4 py-2">{{ todo.description }}</td>
                    <td class="px-4 py-2" :class="statusClass(todo.status)">
                    {{ todo.status }}
                    </td>
                    <td class="px-4 py-2">{{ formatDate(todo.createdAt) }}</td>
                    <td class="px-4 py-2">{{ formatDate(todo.dueDate) }}</td>
                    <td class="px-4 py-2">
                        <button
                            @click="editTodo(todo)"
                            class="bg-yellow-500 text-white px-3 py-1 rounded-md hover:bg-yellow-600"
                        >
                        Edit
                        </button>
                        <button
                            @click="deleteTodo(todo.id)"
                            class="bg-red-500 text-white px-3 py-1 rounded-md hover:bg-red-600"
                        >
                        Delete
                        </button>
                    </td>
                </tr>
            </tbody>
        </table>
    </div>
</template>
  
<script>
    export default {
        data() {
            return {
                todos: [],
                filter: 'all',
            };
        },
        methods: {
            // 獲取 Todo 列表
            async getTodos() {
                try {
                const response = await fetch(`/api/todo?filter=${this.filter}`, {
                    headers: {
                        Authorization: `Bearer ${localStorage.getItem('token')}`, // 使用 JWT 進行認證
                    },
                });
                const data = await response.json();
                this.todos = data;
                } catch (error) {
                    console.error('Error fetching todos:', error);
                }
            },
        
            // 新增 Todo 跳轉到新增頁面
            goToAddTodo() {
                this.$router.push('/add-todo'); // 這假設您有一個新增 Todo 的頁面
            },
        
            // 編輯 Todo
            editTodo(todo) {
                // 編輯邏輯，可以跳轉到編輯頁面或顯示編輯表單
                console.log('Editing todo:', todo);
            },
        
            // 刪除 Todo
            async deleteTodo(todoId) {
                try {
                const response = await fetch(`/api/todo/${todoId}`, {
                    method: 'DELETE',
                    headers: {
                        Authorization: `Bearer ${localStorage.getItem('token')}`,
                    },
                });
        
                if (response.ok) {
                    this.getTodos(); // 更新列表
                }
                } catch (error) {
                    console.error('Error deleting todo:', error);
                }
            },
        
            // 根據 Todo 狀態給予不同的顏色
            statusClass(status) {
                switch (status) {
                case 'completed':
                    return 'text-green-500';
                case 'incomplete':
                    return 'text-yellow-500';
                case 'expired':
                    return 'text-red-500';
                default:
                    return 'text-gray-500';
                }
            },
        
            // 格式化日期
            formatDate(date) {
                if (!date) return '';
                const d = new Date(date);
                return d.toLocaleDateString();
            },
        },
        mounted() {
            this.getTodos(); // 頁面加載時自動獲取 Todos
        },
    };
</script>