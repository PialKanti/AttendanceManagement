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
import { HttpStatusCode } from 'axios';
import { client } from '@/clients/HttpClient';
import { AlertType } from '@/enums/enum';

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

            await client.post('auth/login', data, { withCredentials: true })
                .then(response => {
                    if (response.status === HttpStatusCode.Ok) {
                        this.authStore.login(response.data.user);
                        this.alertStore.show('Login successful', AlertType.Success);
                        this.loading = false;
                        this.$refs.loginForm.reset();
                        this.$router.push('/dashboard');
                    }
                })
                .catch(error => {
                    const errors = error.response.data.errors;
                    if (errors) {
                        this.loading = false;
                        this.alertStore.showError(errors, AlertType.Error);
                    }
                });
        },
        toggleAppendIcon() {
            console.log('toggle');
            this.showPassword = !this.showPassword;
        }
    }
}
</script>