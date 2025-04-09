import { createRouter, createWebHistory } from 'vue-router'
import LoginView from '../views/LoginView.vue' // 引入登入頁面
import RegisterView from '../views/RegisterView.vue';  // 引入註冊頁面
import TodoView from '@/views/TodoView.vue'; // 引入todo首頁

const router = createRouter({
  history: createWebHistory(import.meta.env.BASE_URL),
  routes: [
    {
      path: '/',
      name: 'login',
      component: LoginView,
    },
    {
      path: '/register',
      name: 'register',
      component: RegisterView,
    },
    {
      path: '/todo',
      name: 'todo',
      component: TodoView,
    }
  ],
})

export default router
