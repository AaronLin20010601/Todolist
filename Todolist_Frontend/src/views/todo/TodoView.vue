<template>
    <div class="max-w-6xl mx-auto mt-10 p-4">
        <h1 class="text-3xl font-bold mb-4">Todo List</h1>
        <p class="mb-4">Here is your todo list.</p>
        
        <!-- 篩選和新增按鈕區塊 -->
        <TodoFilterBar :filter="filter" @filter-change="onFilterChange" />
        <!-- Todo 列表 -->
        <TodoTable :todos="todos" @reload="getTodos" />
        <!-- 分頁按鈕 -->
        <Pagination :currentPage="page" :totalPages="totalPages" @page-change="changePage" />
    </div>
</template>
  
<script>
import TodoFilterBar from '@/components/todo/TodoFilterBar.vue';
import TodoTable from '@/components/todo/TodoTable.vue';
import Pagination from '@/components/todo/Pagination.vue';
import { fetchTodos } from '@/api/todo';

export default {
    components: { TodoFilterBar, TodoTable, Pagination },
    data() {
        return {
            todos: [],
            filter: 'all',
            page: 1,
            pageSize: 10,
            totalPages: 1,
        };
    },
    methods: {
        // 獲取 Todo 列表
        async getTodos() {
            try {
                const data = await fetchTodos(this.filter, this.page, this.pageSize);
                this.todos = data.items.map(todo => ({
                    ...todo,
                    isCompleted: todo.status === 'completed'
                }));

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
        onFilterChange(newFilter) {
            this.filter = newFilter;
            this.page = 1;
            this.getTodos();
        },
    },
    mounted() {
        this.getTodos(); // 頁面加載時自動獲取 Todos
    },
};
</script>