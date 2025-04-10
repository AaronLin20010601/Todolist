import './assets/tailwind.css'

import { createApp } from 'vue'
import App from './App.vue'
import router from './router'
import store from './store'
import axios from 'axios'

const app = createApp(App)

store.dispatch('initState')

app.use(router)

app.use(store)

app.config.globalProperties.$http = axios;

app.mount('#app')
