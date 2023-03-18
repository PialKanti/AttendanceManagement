<template>
    <div class="row">
        <div class="col"></div>
        <div class="col">
            <form @submit.prevent="onSubmit">
                <v-text-field label="Username" v-model="username" clearable></v-text-field>
                <v-text-field type="password" label="Password" v-model="password" clearable></v-text-field>
                <v-checkbox label="Remember me"></v-checkbox>
                <v-btn type="submit" color="success">Login</v-btn>
            </form>
        </div>
        <div class="col"></div>
    </div>
</template>

<script>
import { useAuthStore } from '@/stores/AuthStore';
import { useAlertStore } from '@/stores/AlertStore';
import { HttpStatusCode } from 'axios'
import { client } from '@/clients/HttpClient'

export default {
    setup() {
        const authStore = useAuthStore();
        const alertStore = useAlertStore();

        return { authStore, alertStore }
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
            const data = {
                userName: this.username,
                password: this.password
            };

            this.clearForm();
            const response = await client.post('auth/login', data, { withCredentials: true });

            if (response.status === HttpStatusCode.Ok) {
                this.authStore.login(data.userName);
                this.alertStore.show('Login successful', 'success');
                this.$router.push('/dashboard');
            }
        },
        clearForm() {
            this.username = '';
            this.password = '';
        }
    }
}
</script>