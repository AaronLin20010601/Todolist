<template>
    <div class="max-w-4xl mx-auto mt-10 p-4">
        <h1 class="text-3xl font-bold mb-4">Add New Todo</h1>
        
        <!-- 新增 Todo 表單 -->
        <AddTodoForm @success="handleSuccess" @error="handleError"/>
    
        <!-- 錯誤消息顯示區域 -->
        <div v-if="errorMessage" class="mt-4 text-red-500">{{ errorMessage }}</div>
    </div>
</template>
  
<script>
import AddTodoForm from '@/components/AddTodoForm.vue'

export default {
    components: { AddTodoForm },
    data() {
        return {
            errorMessage: '',
        };
    },
    methods: {
        // 新增後返回 Todo
        handleSuccess() {
            this.errorMessage = '';
            this.$router.push('/todo')
        },
        handleError(message) {
            this.errorMessage = message;
        }
    },
};
</script>