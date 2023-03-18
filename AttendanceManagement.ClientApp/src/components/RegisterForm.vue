<template>
    <div class="row col-6 mx-auto">
        <form @submit.prevent="onSubmit">
            <div class="row">
                <div class="col">
                    <v-text-field label="First Name" v-model="firstname" clearable></v-text-field>
                </div>
                <div class="col">
                    <v-text-field label="Last Name" v-model="lastname" clearable></v-text-field>
                </div>
            </div>
            <div class="col-md-6">
                <v-text-field label="Username" v-model="username" clearable></v-text-field>
            </div>
            <div class="col-md-6">
                <v-text-field label="Email" type="email" v-model="email" clearable></v-text-field>
            </div>
            <div class="col-md-6">
                <v-text-field type="date" label="Birthday" v-model="birthday" clearable></v-text-field>
            </div>
            <div class="col-md-6">
                <v-text-field type="password" label="Password" v-model="password" clearable></v-text-field>
            </div>
            <div class="col-md-6">
                <v-text-field type="password" label="Confirm Password" v-model="confirmPassword" clearable></v-text-field>
            </div>
            <v-btn type="submit" color="success">Register</v-btn>
        </form>
    </div>
</template>

<script>
import { client } from '@/clients/HttpClient';
import { HttpStatusCode } from 'axios';
import { useAlertStore } from '@/stores/AlertStore';

export default {
    setup() {
        const alertStore = useAlertStore();

        return { alertStore }
    },
    name: 'RegisterForm',
    data() {
        return {
            firstname: '',
            lastname: '',
            username: '',
            email: '',
            birthday: '',
            password: '',
            confirmPassword: ''
        }
    },
    methods: {
        async onSubmit() {
            const data = {
                firstName: this.firstname,
                lastName: this.lastname,
                userName: this.username,
                password: this.password,
                email: this.email,
                birthDate: this.birthday
            };

            this.clearForm();
            const response = await client.post('users', data, { withCredentials: true });

            if (response.status === HttpStatusCode.Ok) {
                this.alertStore.show('Registration successful. Please login to continue.', 'success');
                this.$router.push('/login');
            }
        },
        clearForm() {
            this.firstname = '';
            this.lastname = '';
            this.username = '';
            this.email = '';
            this.birthday = '';
            this.password = '';
            this.confirmPassword = '';
        }
    }
}
</script>