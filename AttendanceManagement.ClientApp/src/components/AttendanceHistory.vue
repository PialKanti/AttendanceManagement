<template>
    <div>
        <ul class="nav nav-tabs">
            <li class="nav-item" v-for="(item, index) in months" :key="index">
                <a class="nav-link" aria-current="page" href="#" :class="getNavItemClass(index)"
                    @click="onTabItemClicked(index)">{{ item }}</a>
            </li>
        </ul>
        <HistoryTable :item="yearlyAttendaces[activeTab]" />
    </div>
</template>
<script>
import HistoryTable from './HistoryTable.vue';
import axios from 'axios';
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
    data() {
        return {
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
            const uri = process.env.VUE_APP_DEV_API_ENDPOINT + 'users/' + this.authStore.user.username + '/attendances?year=' + year;
            const response = await axios.get(uri, { withCredentials: true });

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

        this.yearlyAttendaces = await this.getYearlyAttendances(todayDate.getFullYear());
    },
}
</script>