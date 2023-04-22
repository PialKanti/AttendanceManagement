<template>
    <h5>Account information</h5>
    <v-container>
        <v-form ref="editInfoForm" v-if="isEditFormShown" validate-on="input" @submit.prevent="submit">
            <v-row class="info-row" align="start">
                <v-col class="column-header-text" cols="4">Name :</v-col>
                <v-col cols="8"><v-row>
                        <v-col>
                            <v-text-field label="First Name" v-model="editedUser.firstName" :rules="rules.firstname"
                                variant="outlined" density="compact" clearable required></v-text-field>
                        </v-col>
                        <v-col>
                            <v-text-field label="Last Name" v-model="editedUser.lastName" :rules="rules.lastname"
                                variant="outlined" density="compact" clearable required></v-text-field>
                        </v-col>
                    </v-row></v-col>
            </v-row>
            <v-row class="info-row" align="start">
                <v-col class="column-header-text" cols="4">Username :</v-col>
                <v-col cols="8">{{ appUser.userName }}</v-col>
            </v-row>
            <v-row class="info-row" align="start">
                <v-col class="column-header-text" cols="4">Email :</v-col>
                <v-col cols="8">
                    <v-text-field class="mt-2" label="Email" type="email" density="compact" v-model="editedUser.email"
                        :rules="rules.email" variant="outlined" clearable required></v-text-field>
                </v-col>
            </v-row>
            <v-row class="mt-8">
                <v-btn type="submit" color="success" :disabled="!isSaveEnabled">Save
                    changes</v-btn>
                <v-btn class="mx-4" type="reset" color="grey" @click="onCancelClicked">Cancel</v-btn>
            </v-row>
        </v-form>
        <div v-else>
            <v-row class="info-row" align="start">
                <v-col class="column-header-text" cols="4">Name :</v-col>
                <v-col cols="8">{{ fullName }}</v-col>

            </v-row>
            <v-row class="info-row" align="start">
                <v-col class="column-header-text" cols="4">Username :</v-col>
                <v-col cols="8">{{ appUser.userName }}</v-col>
            </v-row>
            <v-row class="info-row" align="start">
                <v-col class="column-header-text" cols="4">Email :</v-col>
                <v-col cols="8">{{ appUser.email }}</v-col>
            </v-row>
            <v-row class="mt-8">
                <v-btn color="success" @click="onEditInfoClicked">Edit info</v-btn>
            </v-row>
        </div>
    </v-container>
</template>
<script>
import { useAuthStore } from '@/stores/AuthStore';
import { useAlertStore } from '@/stores/AlertStore';
import { client } from '@/clients/HttpClient';
import { HttpStatusCode } from 'axios';
import { AlertType } from '@/enums/enum';

export default {
    setup() {
        const authStore = useAuthStore();
        const alertStore = useAlertStore();
        return { authStore, alertStore };
    },
    name: 'AccountInfo',
    computed: {
        fullName: function () {
            return `${this.authStore.user.firstName} ${this.authStore.user.lastName}`;
        },
        appUser: function () {
            return this.authStore.user;
        },
        isSaveEnabled: function () {
            return this.isChangesMade() && this.$refs.editInfoForm.errors.length === 0;
        }

    },
    data() {
        return {
            isEditFormShown: false,
            editedUser: {
                firstName: '',
                lastName: '',
                email: ''
            },
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
                email: [
                    value => {
                        if (value) {
                            return true;
                        }
                        return "Email is required";
                    }
                ]
            }
        }
    },
    methods: {
        onEditInfoClicked() {
            this.isEditFormShown = true;
            this.editedUser.firstName = this.authStore.user.firstName;
            this.editedUser.lastName = this.authStore.user.lastName;
            this.editedUser.email = this.authStore.user.email;
        },
        onCancelClicked() {
            this.hideEditForm();
        },
        hideEditForm() {
            this.isEditFormShown = false;
            this.editedUser.firstName = '';
            this.editedUser.lastName = '';
            this.editedUser.email = '';
        },
        isChangesMade: function () {
            if (this.editedUser.firstName !== this.authStore.user.firstName
                || this.editedUser.lastName !== this.authStore.user.lastName
                || this.editedUser.email !== this.authStore.user.email) {
                return true;
            }

            return false;
        },
        async submit() {
            console.log('submit');
            const isValid = this.$refs.editInfoForm.errors.length === 0;
            if (isValid === false)
                return;

            const data = {
                userName: this.authStore.user.userName,
                firstName: this.editedUser.firstName,
                lastName: this.editedUser.lastName,
                email: this.editedUser.email
            };

            console.log(data);

            await client.put(`users/${this.authStore.user.userName}`, data, { withCredentials: true })
                .then(response => {
                    if (response.status === HttpStatusCode.Ok) {
                        this.alertStore.show('Account information updated successful.', AlertType.Success);
                        this.authStore.updateUserInfo(this.editedUser);
                        this.hideEditForm();
                    }
                })
                .catch(error => {
                    const errors = error.response.data.errors;
                    if (errors) {
                        this.alertStore.showError(errors, AlertType.Error);
                    }
                });
        }
    }
}
</script>
<style scoped>
.info-row {
    width: 600px;
}

.column-header-text {
    font-weight: 600;
}

.info-action {
    background-color: red;
    margin-top: 40px;
    margin-bottom: 0px;
}
</style>