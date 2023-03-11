import DashBoard from '@/components/DashBoard'
import LoginForm from '@/components/LoginForm'

export const routes = [
    { path: '/dashboard', name: 'Dashboard', component: DashBoard },
    { path: '/login', name: 'Login', component: LoginForm },
];