import DashBoard from '@/components/DashBoard'
import LoginForm from '@/components/LoginForm'
import RegisterForm from '@/components/RegisterForm'

export const routes = [
    { path: '/dashboard', name: 'Dashboard', component: DashBoard },
    { path: '/login', name: 'Login', component: LoginForm },
    { path: '/register', name: 'Register', component: RegisterForm },
];