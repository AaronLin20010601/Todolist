<template>
    <nav class="bg-blue-600 text-white px-6 py-4 shadow-md">
      <div class="container mx-auto flex justify-between items-center">
        <h1 class="text-xl font-bold">📝 My Todo List</h1>
        <ul class="flex space-x-6">
          <li>
            <RouterLink to="/" class="hover:text-yellow-300" active-class="underline">登入</RouterLink>
          </li>
          <li>
            <RouterLink to="/register" class="hover:text-yellow-300" active-class="underline">註冊</RouterLink>
          </li>
        </ul>
      </div>
    </nav>
  </template>
  
  <script setup>
  import { RouterLink } from 'vue-router'
  </script>