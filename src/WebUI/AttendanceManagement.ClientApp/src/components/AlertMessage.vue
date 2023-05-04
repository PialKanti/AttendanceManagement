<template>
    <div class="col-4 mx-auto">
        <v-alert density="compact" variant="tonal" border="start" :type="alertType" :title="alertTitle"
            @click:close="OnCloseButtonClicked" closable>
            <div v-if="isErrorType">
                <ul>
                    <li v-for="(item, index) in alertErrors" :key="index">
                        {{ item.title }}
                    </li>
                </ul>
            </div>
            <div v-else>{{ alertMessage }}</div>
        </v-alert>
    </div>
</template>
<script>
import { useAlertStore } from '@/stores/AlertStore';
import { AlertType } from '@/enums/enum';

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
        alertErrors: function () {
            return this.alertStore.errors;
        },
        alertType: function () {
            return this.alertStore.type;
        },
        alertTitle: function () {
            return this.capitalizeFirstLetter(this.alertStore.type);
        },
        isErrorType: function () {
            return this.alertStore.type === AlertType.Error;
        }
    },
    mounted() {
        if (this.alertStore.type === AlertType.Success) {
            timer = setTimeout(() => {
                this.alertStore.hide();
            }, 6000);
        }
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