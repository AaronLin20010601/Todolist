<template>
    <!-- 篩選和新增按鈕區塊 -->
    <div class="flex justify-between items-center mb-6">
        <div class="flex items-center space-x-4">
            <!-- 狀態篩選下拉選單 -->
            <label for="filter" class="mr-2">Status:</label>
            <select
                v-model="internalFilter" @change="onFilterChange"
                class="p-2 border border-gray-300 rounded-md"
            >
                <option value="all">All</option>
                <option value="completed">Completed</option>
                <option value="incomplete">Incomplete</option>
                <option value="expired">Expired</option>
            </select>

            <!-- 截止時間篩選 -->
            <label class="ml-4">Start Due Date:</label>
            <input type="datetime-local" v-model="startDueDate" @change="onFilterChange" class="p-2 border border-gray-300 rounded-md" />

            <label>End Due Date:</label>
            <input type="datetime-local" v-model="endDueDate" @change="onFilterChange" class="p-2 border border-gray-300 rounded-md" />

            <!-- 前往新增 Todo -->
            <button @click="goToAddTodo" class="ml-2 bg-blue-500 text-white p-2 rounded-md hover:bg-blue-600">
            Add Todo
            </button>
        </div>
    </div>
</template>

<script>
export default {
    props: {
        filter: String,
    },
    emits: ['filter-change'],
    data() {
        return {
            internalFilter: this.filter,
            startDueDate: null,
            endDueDate: null,
        };
    },
    methods: {
        onFilterChange() {
            this.$emit('filter-change', {
                filter: this.internalFilter,
                startDueDate: this.startDueDate,
                endDueDate: this.endDueDate,
            });
        },
        // 前往新增 Todo
        goToAddTodo() {
            this.$router.push('/addtodo');
        },
    },
};
</script>