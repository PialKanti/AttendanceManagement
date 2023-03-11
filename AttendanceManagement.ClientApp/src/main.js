import { createApp } from 'vue'
import { createPinia } from 'pinia'
import * as VueRouter from 'vue-router'
import App from './App.vue'
import { routes } from './routes/router'
import './assets/main.css'
import { useAuthStore } from './stores/AuthStore'

const router = VueRouter.createRouter({
    history: VueRouter.createWebHistory(),
    routes
})

router.beforeEach((to) => {
    const authStore = useAuthStore();
    if (!authStore.user.isLoggedIn && to.name !== 'Login' && to.name !== 'Register') {
        return { name: 'Login' };
    }
    return true;
})

createApp(App).use(createPinia()).use(router).mount('#app')
