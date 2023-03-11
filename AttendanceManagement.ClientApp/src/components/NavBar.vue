<template>
    <nav class="navbar navbar-expand-lg navbar-dark">
        <div class="container-fluid">
            <a class="navbar-brand" href="#">Navbar</a>
            <button class="navbar-toggler" type="button" data-bs-toggle="collapse" data-bs-target="#navbarSupportedContent"
                aria-controls="navbarSupportedContent" aria-expanded="false" aria-label="Toggle navigation">
                <span class="navbar-toggler-icon"></span>
            </button>
            <div class="collapse navbar-collapse" id="navbarSupportedContent">
                <ul class="navbar-nav me-auto mb-2 mb-lg-0" v-if="authStore.user.isLoggedIn">
                    <li class="nav-item">
                        <a class="nav-link active" aria-current="page" href="#">Home</a>
                    </li>
                </ul>
                <ul class="navbar-nav ms-auto mb-2 mb-lg-0" v-else>
                    <li class="nav-item">
                        <router-link to="/login" class="nav-link" aria-current="page">Login</router-link>
                    </li>
                    <li class="nav-item">
                        <router-link to="/register" class="nav-link" aria-current="page">Register</router-link>
                    </li>
                </ul>
            </div>
        </div>
    </nav>
</template>

<script>
import { useAuthStore } from '@/stores/AuthStore';

export default {
    setup() {
        const authStore = useAuthStore()

        return { authStore }
    },
    name: 'NavBar'
}
</script>

<style>
nav.navbar {
    background: #A6B0B7;
}
</style>