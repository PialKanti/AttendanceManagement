<template>
    <v-menu rounded open-on-hover>
        <template v-slot:activator="{ props }">
            <v-btn icon v-bind="props">
                <v-avatar color="teal-lighten-4">
                    <v-icon icon="mdi-account-circle"></v-icon>
                </v-avatar>
            </v-btn>
        </template>
        <v-card>
            <v-card-text>
                <div class="mx-auto text-center">
                    <v-avatar color="teal-lighten-4">
                        <v-icon icon="mdi-account-circle"></v-icon>
                    </v-avatar>
                    <h5>{{ fullName }}</h5>
                    <p class="text-medium-emphasis">
                        {{ email }}
                    </p>
                    <v-divider class="my-3"></v-divider>
                    <v-btn rounded variant="text" color="danger" to="/logout" @click="onLogout">
                        Logout
                    </v-btn>
                </div>
            </v-card-text>
        </v-card>
    </v-menu>
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
    name: 'AvatarMenu',
    props: {
        fullName: String,
        email: String,
        username: String
    },
    methods: {
        async onLogout() {
            const response = await client.get("auth/logout", { withCredentials: true });
            if (response.status === HttpStatusCode.Ok) {
                this.authStore.logout();
                this.alertStore.hide();
                this.$router.push("/login");
            }
        }
    }
}
</script>