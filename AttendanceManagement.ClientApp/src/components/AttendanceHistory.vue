<template>
    <div>
        <v-card class="w-75 mx-auto">
            <v-card-text>
                <v-tabs bg-color="primary" v-model="tab" align-tabs="center">
                    <v-tab v-for="(item, index) in months" :key="index" :value="index" @click="onTabItemClicked(index)">
                        {{ item }}
                    </v-tab>
                </v-tabs>

                <v-window v-model="tab">
                    <v-window-item v-for="(item, index) in months" :key="index" :value="index">
                        <HistoryTable :item="activeMonthAttendances" />
                    </v-window-item>
                </v-window>
            </v-card-text>
        </v-card>
    </div>
</template>
<script>
import HistoryTable from './HistoryTable.vue';
import { client } from '@/clients/HttpClient'
import { useAuthStore } from '@/stores/AuthStore';

export default {
    setup() {
        const authStore = useAuthStore();

        return { authStore };
    },
    name: 'AttendanceHistory',
    components: {
        HistoryTable
    },
    computed: {
        activeMonthAttendances: function () {
            return this.yearlyAttendaces.find(attendance => (attendance.month - 1) == this.tab);
        }
    },
    data() {
        return {
            tab: 0,
            activeTab: 0,
            months: ["January", "February", "March", "April", "May", "June", "July",
                "August", "September", "October", "November", "December"],
            yearlyAttendaces: []
        }
    },
    methods: {
        getNavItemClass(index) {
            return (this.activeTab === index) ? 'active' : '';
        },
        async getYearlyAttendances(year) {
            const uri = 'users/' + this.authStore.user.userName + '/attendances?year=' + year;
            const response = await client.get(uri, { withCredentials: true });

            return response.data;
        },
        updateActiveTab(index) {
            this.activeTab = index;
        },
        onTabItemClicked(index) {
            this.updateActiveTab(index);
        }
    },
    async created() {
        const todayDate = new Date();
        this.activeTab = todayDate.getMonth();
        this.tab = todayDate.getMonth();

        this.yearlyAttendaces = await this.getYearlyAttendances(todayDate.getFullYear());
    },
}
</script>