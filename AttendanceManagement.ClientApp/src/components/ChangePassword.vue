<template>
    <v-card class="mx-auto" title="Change password" :loading="loading" width="450">
        <v-container>
            <v-form ref="changePasswordForm" validate-on="input" @submit.prevent="onSubmit">
                <v-text-field id="oldPassword" :type="getPasswordInputType(password.old.show)" label="Current password"
                    v-model="password.old.text" :append-icon="getAppendIcon('old')" @click:append="toggleAppendIcon('old')"
                    :rules="rules.oldPassword" variant="outlined" clearable></v-text-field>

                <v-text-field id="newPassword" class="mt-3" :type="getPasswordInputType(password.new.show)"
                    label="New password" v-model="password.new.text" :append-icon="getAppendIcon('new')"
                    @click:append="toggleAppendIcon('new')" :rules="rules.newPassword" variant="outlined"
                    clearable></v-text-field>

                <v-text-field id="confirmPassword" class="mt-3 mb-3" :type="getPasswordInputType(password.confirm.show)"
                    label="Confirm password" v-model="password.confirm.text" :append-icon="getAppendIcon('confirm')"
                    @click:append="toggleAppendIcon('confirm')" :rules="rules.confirmPassword" variant="outlined"
                    clearable></v-text-field>
                <v-btn type="submit" color="success">Change</v-btn>
            </v-form>
        </v-container>
    </v-card>
</template>
<script>
import { useAuthStore } from '@/stores/AuthStore';
import { useAlertStore } from '@/stores/AlertStore';
import { validatePassword } from '@/utils/passwordUtils';
import { client } from '@/clients/HttpClient';
import { HttpStatusCode } from 'axios';
import { AlertType } from '@/enums/enum';

export default {
    setup() {
        const authStore = useAuthStore();
        const alertStore = useAlertStore();

        return { authStore, alertStore };
    },
    name: 'ChangePassword',
    data() {
        return {
            loading: false,
            password: {
                old: {
                    text: '',
                    show: false,
                },
                new: {
                    text: '',
                    show: false
                },
                confirm: {
                    text: '',
                    show: false
                }
            },
            rules: {
                oldPassword: [
                    value => {
                        if (value) {
                            return true;
                        }
                        return "Old password is required";
                    }
                ],
                newPassword: [
                    value => {
                        if (value) {
                            return true;
                        }
                        return "New password is required";
                    },
                    value => {
                        if (value !== this.password.old.text) {
                            return true;
                        }
                        return "New password must be different from old password";
                    },
                    value => {
                        if (value.length >= 6) {
                            return true;
                        }
                        return "Minimum 6 characters needed";
                    },
                    value => {
                        if (validatePassword(value)) {
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
                        return "Confirm password is required";
                    },
                    value => {
                        if (value === this.password.new.text) {
                            return true;
                        }

                        return "Password and Confirm Password do not match";
                    }
                ]
            }
        }
    },
    methods: {
        async onSubmit() {
            const { valid } = await this.$refs.changePasswordForm.validate();
            if (valid === false)
                return;

            this.loading = true;

            const data = {
                oldPassword: this.password.old.text,
                newPassword: this.password.new.text
            };

            await client.post(`users/${this.authStore.user.username}/password`, data, { withCredentials: true })
                .then(response => {
                    if (response.status === HttpStatusCode.Ok) {
                        this.alertStore.show('Password changed successfully. Please login to continue.', AlertType.Success);
                        this.loading = false;
                        this.$refs.changePasswordForm.reset();
                        this.authStore.logout();
                        this.$router.push('/login');
                    }
                })
                .catch(error => {
                    const errors = error.response.data.errors;
                    console.log(errors);
                    if (errors) {
                        this.loading = false;
                        this.alertStore.showError(errors, AlertType.Error);
                    }
                });

        },
        getPasswordInputType(isShown) {
            return isShown ? 'text' : 'password';
        },
        getAppendIcon(type) {
            let isShown = false;
            if (type === 'old') {
                isShown = this.password.old.show;
            } else if (type === 'new') {
                isShown = this.password.new.show;
            } else if (type === 'confirm') {
                isShown = this.password.confirm.show;
            }
            return isShown ? 'mdi-eye' : 'mdi-eye-off';
        },
        toggleAppendIcon(type) {
            if (type === 'old') {
                this.password.old.show = !this.password.old.show;
            } else if (type === 'new') {
                this.password.new.show = !this.password.new.show;
            } else if (type === 'confirm') {
                this.password.confirm.show = !this.password.confirm.show;
            }
        }
    }
}
</script>