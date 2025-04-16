<!--網頁共用部分-->
<script setup>
import Navbar from './components/shared/Navbar.vue'
import Footer from './components/shared/Footer.vue'
</script>

<template>
    <div class="min-h-screen flex flex-col bg-gray-100 overflow-x-hidden">
        <Navbar />
        <main class="flex-grow p-4">
            <RouterView />
        </main>
        <Footer />
    </div>
</template>

<style>
html {
    overflow-y: scroll;
}
</style>