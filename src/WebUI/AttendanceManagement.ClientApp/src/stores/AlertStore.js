import { defineStore } from "pinia";

export const useAlertStore = defineStore('alertStore', {
    state: () => ({
        isShown: false,
        message: '',
        errors: [],
        type: ''
    }),
    getters: {
        alertClass: (state) => {
            if (state.type == 'success') {
                return 'alert-success';
            } else {
                return 'alert-danger';
            }
        }
    },
    actions: {
        show(message, type) {
            this.isShown = true;
            this.message = message;
            this.type = type;
        },
        showError(errors, type) {
            this.isShown = true;
            this.errors = errors;
            this.type = type;
        },
        hide() {
            this.isShown = false;
            this.message = '';
            this.errors = [];
            this.type = '';
        }
    }
})