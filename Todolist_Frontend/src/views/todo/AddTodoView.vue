<template>
    <div class="max-w-4xl mx-auto mt-10 p-4">
        <h1 class="text-3xl font-bold mb-4">Add New Todo</h1>
        
        <!-- 新增 Todo 表單 -->
        <AddTodoForm @success="handleSuccess"/>
    </div>
</template>
  
<script>
import AddTodoForm from '@/components/AddTodoForm.vue'

export default {
    components: { AddTodoForm },
    methods: {
        // 新增後返回 Todo
        handleSuccess() {
            this.$router.push('/todo')
        }
    },
};
</script>