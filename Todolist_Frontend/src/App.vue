<!--網頁共用部分-->
<script setup>
import Navbar from './components/Navbar.vue'
</script>

<template>
  <div class="min-h-screen bg-gray-100">
    <Navbar />
    <main class="p-4">
      <RouterView />
    </main>
  </div>
</template>