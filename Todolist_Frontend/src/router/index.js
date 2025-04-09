import { createRouter, createWebHistory } from 'vue-router'
import LoginView from '../views/LoginView.vue'
import RegisterView from '../views/RegisterView.vue';  // 引入註冊頁面

const router = createRouter({
  history: createWebHistory(import.meta.env.BASE_URL),
  routes: [
    {
      path: '/',
      name: 'login',
      component: LoginView,
    },
    {
      path: '/register', // 新增的註冊頁面路由
      name: 'register',
      component: RegisterView,  // 註冊頁面的組件
    },
  ],
})

export default router
