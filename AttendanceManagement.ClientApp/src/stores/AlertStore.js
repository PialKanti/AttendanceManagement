import { defineStore } from "pinia";

export const useAlertStore = defineStore('alertStore', {
    state: () => ({
        isShown: false,
        message: '',
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
        hide() {
            this.isShown = false;
            this.message = '';
            this.type = '';
        }
    }
})