import { defineStore } from "pinia";

let user = {
    isLoggedIn: false,
    userName: '',
    firstName: '',
    lastName: '',
    email: ''
}

if (localStorage.getItem('user')) {
    user = JSON.parse(localStorage.getItem('user'));
}

export const useAuthStore = defineStore('authStore', {
    state: () => ({
        user: {
            isLoggedIn: user.isLoggedIn,
            userName: user.userName,
            firstName: user.firstName,
            lastName: user.lastName,
            email: user.email
        }
    }),
    actions: {
        login(user) {
            this.user.isLoggedIn = true;
            this.user.userName = user.userName;
            this.user.firstName = user.firstName;
            this.user.lastName = user.lastName;
            this.user.email = user.email;
        },
        logout() {
            this.resetUser();
        },
        resetUser() {
            this.user.isLoggedIn = false;
            this.user.userName = '';
            this.user.firstName = '';
            this.user.lastName = '';
            this.user.email = '';
        }
    },
})