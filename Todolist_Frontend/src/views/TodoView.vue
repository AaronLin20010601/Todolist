<template>
    <div class="max-w-4xl mx-auto mt-10 p-4">
        <h1 class="text-3xl font-bold mb-4">Todo List</h1>
        <p class="mb-4">這裡是你的待辦清單。</p>
    
        <!-- 範例待辦事項列表 -->
        <ul class="list-disc pl-5">
            <li class="mb-2">完成 Vue 前端開發</li>
            <li class="mb-2">學習如何處理 JWT 認證</li>
            <li class="mb-2">寫單元測試</li>
        </ul>
    
        <!-- 加入新待辦事項 -->
        <div class="mt-6">
            <input
            v-model="newTodo"
            type="text"
            class="p-2 border border-gray-300 rounded-md"
            placeholder="新增待辦事項"
            />
            <button
            @click="addTodo"
            class="ml-2 bg-blue-500 text-white p-2 rounded-md hover:bg-blue-600"
            >
            加入待辦
            </button>
        </div>
    </div>
</template>
  
<script>
export default {
    data() {
        return {
            newTodo: "", // 用戶輸入的新待辦事項
        };
    },
    methods: {
        addTodo() {
            if (this.newTodo.trim() === "") {
                alert("請輸入待辦事項！");
                return;
            }
            alert(`已加入待辦事項：${this.newTodo}`);
            this.newTodo = ""; // 清空輸入框
        },
    },
};
</script>
  