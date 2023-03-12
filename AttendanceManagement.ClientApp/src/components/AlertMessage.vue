<template>
    <div class="alert alert-dismissible fade show mx-auto" :class="alertStore.alertClass" role="alert">
        {{ alertStore.message }}
        <button type="button" class="btn-close" data-bs-dismiss="alert" aria-label="Close"
            @click="OnCloseButtonClicked"></button>
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
        }
    }
}
</script>
<style scoped>
.alert {
    width: 500px;
}
</style>