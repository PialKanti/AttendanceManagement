<template>
    <div class="row">
        <div class="col"></div>
        <div class="col">
            <form @submit.prevent="onSubmit">
                <div class="mb-3">
                    <label for="username" class="form-label">Username</label>
                    <input type="text" v-model="username" class="form-control" id="username"
                        aria-describedby="usernameHelp">
                </div>
                <div class="mb-3">
                    <label for="password" class="form-label">Password</label>
                    <input type="password" v-model="password" class="form-control" id="password">
                </div>
                <div class="mb-3 form-check">
                    <input type="checkbox" class="form-check-input" id="rememberCheckbox">
                    <label class="form-check-label" for="rememberCheckbox">Remember me</label>
                </div>
                <button type="submit" class="btn btn-primary">Login</button>
            </form>
        </div>
        <div class="col"></div>
    </div>
</template>

<script>
import { useAuthStore } from '@/stores/AuthStore';
import axios from 'axios'

export default {
    setup() {
        const authStore = useAuthStore()

        return { authStore }
    },
    name: 'LoginForm',
    data() {
        return {
            username: '',
            password: ''
        }
    },
    methods: {
        async onSubmit() {
            const response = await axios.post(process.env.VUE_APP_DEV_API_ENDPOINT + 'users/authenticate', {
                userName: this.username,
                password: this.password
            }, { withCredentials: true });

            console.log(response.data);
            if (response.status === 200) {
                this.authStore.login();
            }
        }
    }
}
</script>

<style scoped></style>