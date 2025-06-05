<template>
    <div class="w-full max-w-5xl mx-auto flex flex-col items-center justify-center text-center py-12 px-4">
        <h1 class="text-4xl md:text-5xl font-bold mb-4 text-gray-800">Welcome to Todolist</h1>
        <p class="text-lg text-gray-600 mb-8">
            Your simple task manager. Organize your life, track your tasks, and boost your productivity – all in one place.
        </p>
        
        <div class="flex flex-wrap justify-center gap-4 mb-8">
            <RouterLink to="/login">
                <button class="bg-blue-500 text-white px-6 py-3 rounded-xl shadow hover:bg-blue-600 transition">Login</button>
            </RouterLink>
            <RouterLink to="/register">
                <button class="bg-gray-200 text-gray-800 px-6 py-3 rounded-xl shadow hover:bg-gray-300 transition">Register</button>
            </RouterLink>
        </div>

        <img src="@/assets/accept_task.svg" class="w-64 md:w-80 h-auto object-contain"/>
    </div>
</template>