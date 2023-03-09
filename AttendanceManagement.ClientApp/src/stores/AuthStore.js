import { defineStore } from "pinia";

export const useAuthStore = defineStore('authStore', {
    state: () => ({
        user: {
            isLoggedIn: false,
            username: '',
        }
    }),
    actions: {
        login(username) {
            this.user.isLoggedIn = true;
            this.user.username = username;
        },
        logout() {
            this.user.isLoggedIn = false;
            this.user.username = '';
        }
    }
})