<template>
    <v-card class="w-25 mx-auto" title="User Registration">
        <v-container>
            <v-form @submit.prevent="onSubmit">
                <v-row>
                    <v-col>
                        <v-text-field label="First Name" v-model="firstname" variant="outlined" clearable></v-text-field>
                    </v-col>
                    <v-col>
                        <v-text-field label="Last Name" v-model="lastname" variant="outlined" clearable></v-text-field>
                    </v-col>
                </v-row>
                <v-text-field label="Username" v-model="username" variant="outlined" clearable></v-text-field>
                <v-text-field label="Email" type="email" v-model="email" variant="outlined" clearable></v-text-field>
                <v-text-field type="date" label="Birthday" v-model="birthday" variant="outlined" clearable></v-text-field>
                <v-text-field type="password" label="Password" v-model="password" variant="outlined"
                    clearable></v-text-field>
                <v-text-field type="password" label="Confirm Password" v-model="confirmPassword" variant="outlined"
                    clearable></v-text-field>
                <v-btn type="submit" color="success">Register</v-btn>
            </v-form>
        </v-container>
    </v-card>
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