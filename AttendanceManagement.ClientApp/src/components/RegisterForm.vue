<template>
    <div class="row col-6 mx-auto">
        <form @submit.prevent="onSubmit">
            <div class="mb-3">
                <div class="row">
                    <div class="col">
                        <label for="FirstName" class="form-label">First Name</label>
                        <input type="text" class="form-control" v-model="firstname" placeholder="First name" id="FirstName"
                            aria-label="FirstName">
                    </div>
                    <div class="col">
                        <label for="LastName" class="form-label">Last Name</label>
                        <input type="text" class="form-control" v-model="lastname" placeholder="Last name" id="LastName"
                            aria-label="LastName">
                    </div>
                </div>
            </div>
            <div class="col-md-6 mb-3">
                <label for="Username" class="form-label">Username</label>
                <input type="text" class="form-control" v-model="username" placeholder="Username" id="Username">
            </div>
            <div class="col-md-6 mb-3">
                <label for="Username" class="form-label">Email</label>
                <input type="email" class="form-control" v-model="email" placeholder="Email" id="email">
            </div>
            <div class="col-md-6 mb-3">
                <label for="birthday" class="form-label">Birthday</label>
                <input type="date" v-model="birthday" class="form-control" placeholder="Birthday" id="birthday">
            </div>
            <div class="col-md-6 mb-3">
                <label for="password" class="form-label">Password</label>
                <input type="password" v-model="password" class="form-control" placeholder="Password" id="password">
            </div>
            <div class="col-md-6 mb-3">
                <label for="confirm-password" class="form-label">Confirm Password</label>
                <input type="password" v-model="confirmPassword" class="form-control" placeholder="Retype Password"
                    id="confirm-password">
            </div>
            <button type="submit" class="btn btn-primary">Register</button>
        </form>
    </div>
</template>

<script>
import axios from 'axios'

export default {
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
            const response = await axios.post(process.env.VUE_APP_DEV_API_ENDPOINT + 'users', {
                firstName: this.firstname,
                lastName: this.lastname,
                userName: this.username,
                password: this.password,
                email: this.email,
                birthDate: this.birthday
            }, { withCredentials: true });

            if (response.status === 200) {
                this.alertStore.show('Registration successful. Please login to continue.', 'success');
                this.$router.push('/login');
            }
        }
    }
}
</script>