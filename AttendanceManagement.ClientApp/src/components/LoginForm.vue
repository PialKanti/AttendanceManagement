<template>
    <v-card class="w-25 mx-auto" title="Log In" :loading="loading">
        <v-container>
            <v-form @submit.prevent="onSubmit">
                <v-text-field label="Username" v-model="username" variant="outlined" clearable></v-text-field>
                <v-text-field type="password" label="Password" variant="outlined" v-model="password"
                    clearable></v-text-field>
                <v-checkbox label="Remember me"></v-checkbox>
                <v-btn type="submit" color="success">Login</v-btn>
            </v-form>
        </v-container>
    </v-card>
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
            password: '',
            loading: false
        }
    },
    methods: {
        async onSubmit() {
            this.loading = true;

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

            this.loading = false;
        },
        clearForm() {
            this.username = '';
            this.password = '';
        }
    }
}
</script>