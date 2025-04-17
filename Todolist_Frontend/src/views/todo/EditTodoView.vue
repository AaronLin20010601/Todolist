<template>
    <div class="max-w-4xl mx-auto mt-10 p-4">
        <h1 class="text-3xl font-bold mb-4">Edit Todo</h1>
        
        <!-- 編輯 Todo 表單 -->
        <EditTodoForm :todoId="todoId" @success="handleSuccess" @error="handleError" />
    
        <!-- 錯誤消息顯示區域 -->
        <div v-if="errorMessage" class="mt-4 text-red-500">{{ errorMessage }}</div>
    </div>
</template>

<script>
import EditTodoForm from '@/components/EditTodoForm.vue'

export default {
    components: { EditTodoForm },
    data() {
        return {
            errorMessage: '',
        };
    },
    computed: {
        todoId() {
            return this.$route.params.id
        }
    },
    methods: {
        handleSuccess() {
            this.errorMessage = ''
            this.$router.push('/todo')
        },
        handleError(message) {
            this.errorMessage = message
        }
    },
};
</script>