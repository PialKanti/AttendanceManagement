<template>
    <div class="col-4 mx-auto">
        <v-alert density="compact" variant="tonal" border="start" :type="alertType" :title="alertTitle" :text="alertMessage"
            @click:close="OnCloseButtonClicked" closable></v-alert>
    </div>
</template>
<script>
import { useAlertStore } from '@/stores/AlertStore';

let timer;
export default {
    setup() {
        const alertStore = useAlertStore();

        return { alertStore }
    },
    name: 'AlertMessage',
    computed: {
        alertMessage: function () {
            return this.alertStore.message;
        },
        alertType: function () {
            return this.alertStore.type;
        },
        alertTitle: function () {
            return this.capitalizeFirstLetter(this.alertStore.type);
        }
    },
    mounted() {
        timer = setTimeout(() => {
            this.alertStore.hide();
        }, 6000)
    },
    unmounted() {
        clearTimeout(timer);
    },
    methods: {
        OnCloseButtonClicked() {
            this.alertStore.hide();
        },
        capitalizeFirstLetter(str) {
            return str.charAt(0).toUpperCase() + str.slice(1);
        }
    }
}
</script>
<style scoped>
.alert {
    width: 500px;
}
</style>