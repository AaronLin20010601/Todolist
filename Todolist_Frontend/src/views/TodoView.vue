<template>
    <div class="max-w-6xl mx-auto mt-10 p-4">
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
                @change="onFilterChange"
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
        <div v-if="todos.length === 0" class="text-center text-gray-500 p-8 border border-gray-200 rounded-lg">
            No Todos of this status available.
        </div>
    
        <!-- Todo 列表表格，僅在有 Todo 時顯示 -->
        <table v-if="todos.length > 0" class="w-full table-auto">
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
                <tr
                    v-for="todo in todos"
                    :key="todo.id"
                    class="border-b"
                >
                    <td class="px-4 py-2 w-12">
                        <input
                            type="checkbox"
                            :checked="todo.isCompleted"
                            @change="updateTodoStatus(todo)"
                            :disabled="new Date(todo.dueDate).getTime() < Date.now()"
                            class="w-5 h-5 text-green-600 bg-gray-100 border-gray-300 rounded focus:ring-green-500"
                        />
                    </td>
                    <td class="px-4 py-2 w-[200px]">{{ todo.title }}</td>
                    <td class="px-4 py-2 w-[300px]">{{ todo.description }}</td>
                    <td class="px-4 py-2 w-[100px]" :class="statusClass(todo.status)">
                    {{ todo.status }}
                    </td>
                    <td class="px-4 py-2 w-[160px]">{{ formatDate(todo.createdAt) }}</td>
                    <td class="px-4 py-2 w-[160px]">{{ formatDate(todo.dueDate) }}</td>
                    <td class="px-4 py-2 flex space-x-2">
                        <button
                            @click="editTodo(todo)"
                            class="bg-yellow-500 text-white px-3 py-1 rounded-md hover:bg-yellow-600"
                        >
                        Edit
                        </button>
                        <button
                            @click="deleteTodo(todo)"
                            class="bg-red-500 text-white px-3 py-1 rounded-md hover:bg-red-700"
                        >
                        Delete
                        </button>
                    </td>
                </tr>
            </tbody>
        </table>

        <!-- 分頁按鈕 -->
        <div v-if="totalPages > 1" class="flex justify-center mt-6 space-x-1 items-center">
            <!-- 第一頁與上一頁 -->
            <button
                @click="changePage(1)"
                :disabled="page === 1"
                class="px-2 py-1 border rounded-md"
            >
                «
            </button>
            <button
                @click="changePage(page - 1)"
                :disabled="page === 1"
                class="px-2 py-1 border rounded-md"
            >
                ‹
            </button>

            <!-- 中間幾個頁碼 -->
            <button
                v-for="pageNum in visiblePages"
                :key="pageNum"
                @click="changePage(pageNum)"
                :class="[
                    'px-3 py-1 border rounded-md',
                    pageNum === page ? 'bg-blue-500 text-white' : 'bg-white text-black border-gray-300 hover:bg-gray-100'
                ]"
            >
                {{ pageNum }}
            </button>

            <!-- 下一頁與最後一頁 -->
            <button
                @click="changePage(page + 1)"
                :disabled="page === totalPages"
                class="px-2 py-1 border rounded-md"
            >
                ›
            </button>
            <button
                @click="changePage(totalPages)"
                :disabled="page === totalPages"
                class="px-2 py-1 border rounded-md"
            >
                »
            </button>
        </div>
    </div>
</template>
  
<script>
export default {
    data() {
        return {
            todos: [],
            filter: 'all',
            page: 1,
            pageSize: 10,
            totalPages: 1,
        };
    },
    computed: {
        visiblePages() {
            const range = 2; // 顯示當前頁左右各幾頁
            let start = Math.max(1, this.page - range);
            let end = Math.min(this.totalPages, this.page + range);
            const pages = [];

            for (let i = start; i <= end; i++) {
                pages.push(i);
            }
            return pages;
        }
    },
    methods: {
        // 獲取 Todo 列表
        async getTodos() {
            try {
                // 從 Vuex store 獲取 token
                const token = this.$store.getters.getToken;

                // 檢查 token 是否存在，防止發送無效請求
                if (!token) {
                    console.error("No token found");
                    return;
                }

                const response = await this.$http.get(
                    `http://localhost:5000/api/todo?filter=${this.filter}&page=${this.page}&pageSize=${this.pageSize}`, {
                    headers: {
                        Authorization: `Bearer ${token}` // 確保在請求中加入 Authorization header
                    }
                });
                // 根據 status 來設置 isCompleted 屬性
                const data = response.data;
                this.todos = data.items.map(todo => {
                    todo.isCompleted = todo.status === 'completed';
                    return todo;
                });

                this.totalPages = data.totalPages;
                this.page = data.currentPage;
            } catch (error) {
                console.error('Error fetching todos:', error);
            }
        },

        // 換頁
        changePage(pageNum) {
            if (pageNum !== this.page) {
                this.page = pageNum;
                this.getTodos(); // 換頁時重新取得資料
            }
        },

        // 重置頁面
        onFilterChange() {
            this.page = 1;
            this.getTodos();
        },
    
        // 新增 Todo 跳轉到新增頁面
        goToAddTodo() {
            this.$router.push('/addtodo');
        },
    
        // 編輯 Todo
        editTodo(todo) {
            this.$router.push(`/edittodo/${todo.id}`);
        },
        
        // 刪除 Todo
        async deleteTodo(todo) {
            const confirmDelete = confirm('Are you sure you want to delete this todo?');
            if (!confirmDelete) return;

            try {
                // 從 Vuex store 獲取 token
                const token = this.$store.getters.getToken;

                // 檢查 token 是否存在，防止發送無效請求
                if (!token) {
                    console.error("No token found");
                    return;
                }

                const response = await this.$http.delete(`http://localhost:5000/api/todo/${todo.id}`, {
                    headers: {
                        Authorization: `Bearer ${token}`
                    }
                });

                // 根據後端的回應進行處理
                if (response.status === 200) {
                    alert('Todo deleted successfully!');
                    this.getTodos(); // 重新取得列表
                } else {
                    const errorData = response.data;
                    alert(`Failed to delete todo: ${errorData.message || 'Unknown error'}`);
                }
            } catch (error) {
                console.error('Error deleting todo:', error);
                alert('An error occurred while deleting the todo.');
            }
        },

        // 更新 Todo 完成狀態
        async updateTodoStatus(todo) {
            try {
                // 從 Vuex store 獲取 token
                const token = this.$store.getters.getToken;

                // 檢查 token 是否存在，防止發送無效請求
                if (!token) {
                    console.error("No token found");
                    return;
                }

                // 更新前，先更改前端的狀態
                todo.isCompleted = !todo.isCompleted;

                const response = await this.$http.patch(`http://localhost:5000/api/todo/${todo.id}/complete`, 
                { isCompleted: todo.isCompleted },
                {
                    headers: {
                        Authorization: `Bearer ${token}`,
                    },
                });

                if (response.status === 204) {
                    console.log('Todo status updated');
                    todo.status = todo.isCompleted ? "completed" : "incomplete";
                } else {
                    console.error('Response status:', response.status);
                    alert('Failed to update todo status');
                }
            } catch (error) {
                console.error('Error updating todo status:', error);
                todo.isCompleted = !todo.isCompleted;
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
            return d.toLocaleString();
        },
    },
    mounted() {
        this.getTodos(); // 頁面加載時自動獲取 Todos
    },
};
</script>