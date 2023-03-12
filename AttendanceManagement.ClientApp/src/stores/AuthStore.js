import { defineStore } from "pinia";

let user = {
    isLoggedIn: false,
    username: '',
}

if (localStorage.getItem('user')) {
    user = JSON.parse(localStorage.getItem('user'));
}

export const useAuthStore = defineStore('authStore', {
    state: () => ({
        user: {
            isLoggedIn: user.isLoggedIn,
            username: user.username,
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
    },
})