import { createRouter, createWebHistory } from 'vue-router'
import LoginView from '@/views/LoginView.vue' // 引入登入頁面
import RegisterView from '../views/RegisterView.vue';  // 引入註冊頁面
import ResetView from '../views/ResetView.vue'; // 引入重設密碼頁面
import TodoView from '@/views/todo/TodoView.vue'; // 引入todo首頁
import AddTodoView from '@/views/todo/AddTodoView.vue'; // 引入新增todo頁面
import EditTodoView from '@/views/todo/EditTodoView.vue'; // 引入編輯todo頁面
import AccountView from '@/views/AccountView.vue'; // 引入帳號編輯頁面

// 檢查是否存在 JWT Token
const requireAuth = (to, from, next) => {
    const token = localStorage.getItem('token');
    token ? next() : next('/');
};

// 網頁路徑
const routes = [
    { path: '/', name: 'login', component: LoginView },
    { path: '/register', name: 'register', component: RegisterView },
    { path: '/reset', name: 'reset', component: ResetView },
  
    { path: '/todo', name: 'todo', component: TodoView, beforeEnter: requireAuth },
    { path: '/addtodo', name: 'addtodo', component: AddTodoView, beforeEnter: requireAuth },
    { path: '/edittodo/:id', name: 'edittodo', component: EditTodoView, beforeEnter: requireAuth },
    { path: '/account', name: 'account', component: AccountView, beforeEnter: requireAuth },
];

const router = createRouter({
    history: createWebHistory(import.meta.env.BASE_URL),
    routes,
})

export default router