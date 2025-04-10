import { createRouter, createWebHistory } from 'vue-router'
import LoginView from '@/views/LoginView.vue' // 引入登入頁面
import RegisterView from '../views/RegisterView.vue';  // 引入註冊頁面
import ResetView from '../views/ResetView.vue'; // 引入重設密碼頁面
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
            path: '/reset',
            name: 'reset',
            component: ResetView,
        },
        {
            path: '/todo',
            name: 'todo',
            component: TodoView,
            beforeEnter: (to, from, next) => {
                // 檢查是否存在 JWT Token
                const token = localStorage.getItem('token');
                if (token) {
                    next(); // 用戶已經登錄，允許進入 Todo 頁面
                } else {
                    next('/'); // 用戶未登錄，重定向到登入頁面
                }
            },
        }
    ],
})

export default router