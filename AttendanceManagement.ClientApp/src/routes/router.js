import DashBoard from '@/components/DashBoard'
import AttendanceHistory from '@/components/AttendanceHistory'
import LoginForm from '@/components/LoginForm'
import RegisterForm from '@/components/RegisterForm'
import { useAuthStore } from '@/stores/AuthStore'
import * as VueRouter from 'vue-router'

const routes = [
    { path: '/', name: 'Home' },
    { path: '/dashboard', name: 'Dashboard', component: DashBoard },
    { path: '/history', name: 'History', component: AttendanceHistory },
    { path: '/login', name: 'Login', component: LoginForm },
    { path: '/register', name: 'Register', component: RegisterForm },
    { path: '/logout', name: 'Logout' },
];

export const router = VueRouter.createRouter({
    history: VueRouter.createWebHistory(),
    routes
});

router.beforeEach((to) => {
    const authStore = useAuthStore();
    if (to.name === 'Home') {
        if (!authStore.user.isLoggedIn) {
            return { name: 'Login' };
        }
        else {
            return { name: 'Dashboard' }
        }
    }
    if (!authStore.user.isLoggedIn && to.name !== 'Login' && to.name !== 'Register') {
        return { name: 'Login' };
    }
    return true;
});