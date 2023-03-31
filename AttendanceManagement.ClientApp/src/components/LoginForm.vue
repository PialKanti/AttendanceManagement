<template>
    <v-card class="w-25 mx-auto" title="Log In" :loading="loading">
        <v-container>
            <v-form ref="loginForm" validate-on="input" @submit.prevent="onSubmit">
                <v-text-field label="Username" v-model="username" :rules="rules.username" variant="outlined"
                    clearable></v-text-field>
                <v-text-field class="mt-2" id="password" :type="showPassword ? 'text' : 'password'" label="Password"
                    v-model="password" :rules="rules.password" :append-icon="appendIcon" variant="outlined" clearable
                    @click:append="toggleAppendIcon"></v-text-field>
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
            showPassword: false,
            loading: false,
            rules: {
                username: [
                    value => {
                        if (value) {
                            return true;
                        }
                        return "Username is required";
                    }
                ],
                password: [
                    value => {
                        if (value) {
                            return true;
                        }
                        return "Password is required";
                    }
                ]
            }
        }
    },
    computed: {
        passwordType: function () {
            return this.showPassword ? 'text' : 'password';
        },
        appendIcon: function () {
            return this.showPassword ? 'mdi-eye' : 'mdi-eye-off';
        }
    },
    methods: {
        async onSubmit() {
            const { valid } = await this.$refs.loginForm.validate();
            if (valid === false)
                return;

            this.loading = true;
            const data = {
                userName: this.username,
                password: this.password
            };

            this.$refs.loginForm.reset();
            const response = await client.post('auth/login', data, { withCredentials: true });

            if (response.status === HttpStatusCode.Ok) {
                this.authStore.login(data.userName);
                this.alertStore.show('Login successful', 'success');
                this.$router.push('/dashboard');
            }

            this.loading = false;
        },
        toggleAppendIcon() {
            console.log('toggle');
            this.showPassword = !this.showPassword;
        }
    }
}
</script>