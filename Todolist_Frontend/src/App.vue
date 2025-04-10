<!--網頁共用部分-->
<script setup>
import Navbar from './components/Navbar.vue'
import Footer from './components/Footer.vue'
</script>

<template>
    <div class="min-h-screen flex flex-col bg-gray-100">
        <Navbar />
        <main class="flex-grow p-4">
            <RouterView />
        </main>
        <Footer />
    </div>
</template>