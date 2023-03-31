<template>
    <v-card class="w-25 mx-auto" title="User Registration" :loading="loading">
        <v-container>
            <v-form ref="registerForm" validate-on="input" @submit.prevent="onSubmit">
                <v-row>
                    <v-col>
                        <v-text-field label="First Name" v-model="firstname" :rules="rules.firstname" variant="outlined"
                            clearable></v-text-field>
                    </v-col>
                    <v-col>
                        <v-text-field label="Last Name" v-model="lastname" :rules="rules.lastname" variant="outlined"
                            clearable></v-text-field>
                    </v-col>
                </v-row>
                <v-text-field class="mt-2" label="Username" v-model="username" :rules="rules.username" variant="outlined"
                    clearable></v-text-field>
                <v-text-field class="mt-2" label="Email" type="email" v-model="email" :rules="rules.email"
                    variant="outlined" clearable></v-text-field>
                <v-text-field class="mt-2" type="date" label="Birthday" v-model="birthday" variant="outlined"
                    clearable></v-text-field>
                <v-text-field class="mt-2" :type="passwordType" label="Password" v-model="password" :rules="rules.password"
                    variant="outlined" :append-icon="passwordAppendIcon" clearable
                    @click:append="toggleShowPassword"></v-text-field>
                <v-text-field class="mt-2" :type="confirmPasswordType" label="Confirm Password" v-model="confirmPassword"
                    :rules="rules.confirmPassword" :append-icon="confirmPasswordAppendIcon" variant="outlined" clearable
                    @click:append="toggleShowConfirmPassword"></v-text-field>
                <v-btn class="mt-2" type="submit" color="success">Register</v-btn>
            </v-form>
        </v-container>
    </v-card>
</template>

<script>
import { client } from '@/clients/HttpClient';
import { HttpStatusCode } from 'axios';
import { useAlertStore } from '@/stores/AlertStore';
import { containsUpperCase, containsLowerCase, containsNonAlphanumeric, containsDigit, containsUniqueCharacters } from '@/utils/StringUtils';

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
            confirmPassword: '',
            loading: false,
            showPassword: false,
            showConfirmPassword: false,
            rules: {
                firstname: [
                    value => {
                        if (value) {
                            return true;
                        }
                        return "First Name is required";
                    }
                ],
                lastname: [
                    value => {
                        if (value) {
                            return true;
                        }
                        return "Last Name is required";
                    }
                ],
                username: [
                    value => {
                        if (value) {
                            return true;
                        }
                        return "Username is required";
                    }
                ],
                email: [
                    value => {
                        if (value) {
                            return true;
                        }
                        return "Email is required";
                    }
                ],
                password: [
                    value => {
                        if (value) {
                            return true;
                        }
                        return "Password is required";
                    },
                    value => {
                        if (value.length >= 6) {
                            return true;
                        }
                        return "Minimum 6 characters needed";
                    },
                    value => {
                        if (this.validatePassword(value)) {
                            return true;
                        }
                        return 'Password must contain a upper case, lower case, digit and special character. All characters must be unique.';
                    }
                ],
                confirmPassword: [
                    value => {
                        if (value) {
                            return true;
                        }
                        return "Confirm Password is required";
                    },
                    value => {
                        if (value === this.password) {
                            return true;
                        }

                        return "Password and Confirm Password do not match";
                    }
                ]
            }
        }
    },
    computed: {
        passwordType: function () {
            return this.getInputType(this.showPassword);
        },
        confirmPasswordType: function () {
            return this.getInputType(this.showConfirmPassword);
        },
        passwordAppendIcon: function () {
            return this.getAppendIcon(this.showPassword);
        },
        confirmPasswordAppendIcon: function () {
            return this.getAppendIcon(this.showConfirmPassword);
        }
    },
    methods: {
        async onSubmit() {
            const { valid } = await this.$refs.registerForm.validate();
            if (valid === false)
                return;

            this.loading = true;

            const data = {
                firstName: this.firstname,
                lastName: this.lastname,
                userName: this.username,
                password: this.password,
                email: this.email,
                birthDate: this.birthday
            };

            this.$refs.registerForm.reset();
            const response = await client.post('users', data, { withCredentials: true });

            if (response.status === HttpStatusCode.Ok) {
                this.alertStore.show('Registration successful. Please login to continue.', 'success');
                this.$router.push('/login');
            }

            this.loading = false;
        },
        validatePassword(password) {
            if (containsUpperCase(password) === false) {
                return false;
            }
            if (containsLowerCase(password) === false) {
                return false;
            }
            if (containsDigit(password) === false) {
                return false;
            }
            if (containsNonAlphanumeric(password) === false) {
                return false;
            }
            if (containsUniqueCharacters(password) === false) {
                return false;
            }

            return true;
        },
        toggleShowPassword() {
            this.showPassword = !this.showPassword;
        },
        toggleShowConfirmPassword() {
            this.showConfirmPassword = !this.showConfirmPassword;
        },
        getInputType(value) {
            return value ? 'text' : 'password';
        },
        getAppendIcon(value) {
            return value ? 'mdi-eye' : 'mdi-eye-off';
        }
    }
}
</script>