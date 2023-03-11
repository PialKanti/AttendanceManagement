import { createApp } from 'vue'
import { createPinia } from 'pinia'
import * as VueRouter from 'vue-router'
import App from './App.vue'
import { routes } from './routes/router'
import './assets/main.css'

const router = VueRouter.createRouter({
    history: VueRouter.createWebHistory(),
    routes
})

createApp(App).use(createPinia()).use(router).mount('#app')
