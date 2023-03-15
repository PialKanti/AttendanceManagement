import axios, { HttpStatusCode } from 'axios';
import { useAuthStore } from '@/stores/AuthStore';
import { router } from '@/routes/router'

const config = {
    baseURL: process.env.VUE_APP_DEV_API_ENDPOINT
}
export const client = axios.create(config);

client.interceptors.response.use(function (response) {
    return response
}, function (error) {
    const authStore = useAuthStore();

    if (error.response.status == HttpStatusCode.Unauthorized) {
        authStore.logout();
        console.log('Unauthorized');
        router.push('/login');
    }
    return Promise.reject(error);
});