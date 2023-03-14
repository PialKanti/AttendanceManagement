import DashBoard from '@/components/DashBoard'
import AttendanceHistory from '@/components/AttendanceHistory'
import LoginForm from '@/components/LoginForm'
import RegisterForm from '@/components/RegisterForm'

export const routes = [
    { path: '/', name: 'Home' },
    { path: '/dashboard', name: 'Dashboard', component: DashBoard },
    { path: '/history', name: 'History', component: AttendanceHistory },
    { path: '/login', name: 'Login', component: LoginForm },
    { path: '/register', name: 'Register', component: RegisterForm },
    { path: '/logout', name: 'Logout' },
];