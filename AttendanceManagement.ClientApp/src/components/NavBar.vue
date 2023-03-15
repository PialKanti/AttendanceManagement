<template>
    <nav class="navbar navbar-expand-lg navbar-dark">
        <div class="container-fluid">
            <a class="navbar-brand" href="#">Attendance</a>
            <button class="navbar-toggler" type="button" data-bs-toggle="collapse" data-bs-target="#navbarSupportedContent"
                aria-controls="navbarSupportedContent" aria-expanded="false" aria-label="Toggle navigation">
                <span class="navbar-toggler-icon"></span>
            </button>
            <div class="collapse navbar-collapse" id="navbarLoggedUser" v-if="authStore.user.isLoggedIn">
                <ul class="navbar-nav mb-2 mb-lg-0">
                    <li class="nav-item">
                        <router-link to="/dashboard" active-class="active" class="nav-link"
                            aria-current="page">Home</router-link>
                    </li>
                    <li class="nav-item">
                        <router-link to="/history" active-class="active" class="nav-link"
                            aria-current="page">History</router-link>
                    </li>
                </ul>
                <ul class="navbar-nav ms-auto mb-2 mb-lg-0">
                    <li class="nav-item">
                        <router-link to="/logout" active-class="active" class="nav-link" aria-current="page"
                            @click="onLogout">Logout</router-link>
                    </li>
                </ul>
            </div>
            <div class="collapse navbar-collapse" id="navbarDefault" v-else>
                <ul class="navbar-nav ms-auto mb-2 mb-lg-0">
                    <li class="nav-item">
                        <router-link to="/login" active-class="active" class="nav-link"
                            aria-current="page">Login</router-link>
                    </li>
                    <li class="nav-item">
                        <router-link to="/register" active-class="active" class="nav-link"
                            aria-current="page">Register</router-link>
                    </li>
                </ul>
            </div>
        </div>
    </nav>
</template>

<script>
import { useAuthStore } from '@/stores/AuthStore';
import { useAlertStore } from '@/stores/AlertStore';
import { HttpStatusCode } from 'axios';
import { client } from '@/clients/HttpClient';

export default {
    setup() {
        const authStore = useAuthStore();
        const alertStore = useAlertStore();

        return { authStore, alertStore };
    },
    name: 'NavBar',
    methods: {
        async onLogout() {
            const response = await client.get('auth/logout', { withCredentials: true });

            if (response.status === HttpStatusCode.Ok) {
                this.authStore.logout();
                this.alertStore.hide();
                this.$router.push('/login');
            }
        }
    }
}
</script>

<style>
nav.navbar {
    background: #A6B0B7;
}
</style>