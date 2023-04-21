<template>
    <h5>Account information</h5>
    <v-container>
        <v-row class="info-row" align="start">
            <v-col class="column-header-text" cols="4">Name :</v-col>
            <v-col cols="6">{{ fullName }}</v-col>
            <v-col cols="2">
                <v-btn prepend-icon="mdi-square-edit-outline" size="x-small" variant="outlined" rounded="xl">Edit</v-btn>
            </v-col>
        </v-row>
        <v-row class="info-row" align="start">
            <v-col class="column-header-text" cols="4">Username :</v-col>
            <v-col cols="6">{{ appUser.userName }}</v-col>
            <v-col cols="2"></v-col>
        </v-row>
        <v-row class="info-row" align="start">
            <v-col class="column-header-text" cols="4">Email :</v-col>
            <v-col cols="6">{{ appUser.email }}</v-col>
            <v-col cols="2">
                <v-btn prepend-icon="mdi-square-edit-outline" size="x-small" variant="outlined" rounded="xl">Edit</v-btn>
            </v-col>
        </v-row>
    </v-container>
</template>
<script>
import { useAuthStore } from '@/stores/AuthStore';

export default {
    setup() {
        const authStore = useAuthStore();
        return { authStore };
    },
    name: 'AccountInfo',
    computed: {
        fullName: function () {
            return `${this.authStore.user.firstName} ${this.authStore.user.lastName}`;
        },
        appUser: function () {
            return this.authStore.user;
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
</style>