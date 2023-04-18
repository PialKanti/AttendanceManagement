<template>
    <v-app-bar app :elevation="4">
        <v-toolbar color="teal-lighten-1">
            <v-app-bar-nav-icon @click="onDrawerToggled" v-if="authStore.user.isLoggedIn"></v-app-bar-nav-icon>
            <v-toolbar-title>{{ appName }}</v-toolbar-title>
            <v-toolbar-items v-if="authStore.user.isLoggedIn">
                <AvatarMenu :fullName="userFullName" :username="username" :email="email">
                </AvatarMenu>
            </v-toolbar-items>
            <v-toolbar-items v-else>
                <v-btn to="/login" flat>Login</v-btn>
                <v-btn to="/register" flat>Register</v-btn>
            </v-toolbar-items>
        </v-toolbar>
    </v-app-bar>
    <v-navigation-drawer app expand-on-hover clipped v-model="drawer" v-if="authStore.user.isLoggedIn"
        color="teal-lighten-5">
        <v-list nav>
            <v-list>
                <v-list-item v-for="item in menuItems" :key="item.title" :to="item.path">
                    <template v-slot:prepend>
                        <v-icon :icon="item.icon"></v-icon>
                    </template>
                    <v-list-item-title>{{ item.title }}</v-list-item-title>
                </v-list-item>
            </v-list>
        </v-list>
    </v-navigation-drawer>
</template>

<script>
import { useAuthStore } from '@/stores/AuthStore';
import { useAlertStore } from '@/stores/AlertStore';
import { HttpStatusCode } from 'axios';
import { client } from '@/clients/HttpClient';
import AvatarMenu from './AvatarMenu.vue';

export default {
    setup() {
        const authStore = useAuthStore();
        const alertStore = useAlertStore();
        return { authStore, alertStore };
    },
    name: "NavBar",
    data() {
        return {
            drawer: true,
            menuItems: [
                { title: "Dashboard", path: "/dashboard", icon: "mdi-view-dashboard" },
                { title: "History", path: "/history", icon: "mdi-history" }
            ]
        };
    },
    computed: {
        appName: function () {
            return process.env.VUE_APP_NAME;
        },
        userFullName: function () {
            return this.authStore.user.firstName + ' ' + this.authStore.user.lastName;
        },
        username: function () {
            return this.authStore.user.username;
        },
        email: function () {
            return this.authStore.user.email;
        }
    },
    methods: {
        async onLogout() {
            const response = await client.get("auth/logout", { withCredentials: true });
            if (response.status === HttpStatusCode.Ok) {
                this.authStore.logout();
                this.alertStore.hide();
                this.$router.push("/login");
            }
        },
        onDrawerToggled() {
            this.drawer = !this.drawer;
        }
    },
    components: { AvatarMenu }
}
</script>

<style>
nav.navbar {
    background: #A6B0B7;
}
</style>