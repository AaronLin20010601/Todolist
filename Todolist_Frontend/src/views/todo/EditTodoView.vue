<template>
    <div class="max-w-4xl mx-auto mt-10 p-4">
        <h1 class="text-3xl font-bold mb-4">Edit Todo</h1>
        
        <!-- 編輯 Todo 表單 -->
        <EditTodoForm :todoId="todoId" @success="handleSuccess"/>
    </div>
</template>

<script>
import EditTodoForm from '@/components/EditTodoForm.vue'

export default {
    components: { EditTodoForm },
    computed: {
        todoId() {
            return this.$route.params.id
        }
    },
    methods: {
        // 編輯後返回 Todo
        handleSuccess() {
            this.$router.push('/todo')
        }
    },
};
</script>