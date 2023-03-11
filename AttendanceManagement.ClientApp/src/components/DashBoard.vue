<template>
    <div class="content">
        <div class="attendance-given" v-if="isAttendanceFound">
            <p class="text-center"><strong>Today's Entry time</strong> = {{
                getFormattedDateTime(new Date(attendance.entryDateTime)) }}</p>
            <div class="text-center report-button">
                <button type="button" class="btn btn-danger" @click="reportExit">Report exit time</button>
            </div>
        </div>
        <div class="no-attendance" v-else>
            <p class="text-center info-text">No data found for today.</p>
            <div class="text-center report-button">
                <button type="button" class="btn btn-success" @click="reportEntry">Report entry time</button>
            </div>
        </div>

    </div>
</template>
<script>
import { useAuthStore } from '@/stores/AuthStore';
import axios, { HttpStatusCode } from 'axios'

export default {
    setup() {
        const authStore = useAuthStore();

        return { authStore };
    },
    name: 'DashBoard',
    data() {
        return {
            isAttendanceFound: false,
            attendance: {}
        }
    },
    methods: {
        async reportEntry() {
            const todayDate = new Date();
            const data = {
                username: this.authStore.user.username,
                entryDateTime: todayDate.toISOString()
            }

            const uri = process.env.VUE_APP_DEV_API_ENDPOINT + 'attendances';
            const response = await axios.post(uri, data, { withCredentials: true });

            if (response.status === HttpStatusCode.Created) {
                this.attendance = response.data;
                this.isAttendanceFound = true;
            }
        },
        async fetchAttendances(month, year) {
            const uri = process.env.VUE_APP_DEV_API_ENDPOINT + 'users/' + this.authStore.user.username + '/attendances?month=' + month + '&year=' + year;
            const response = await axios.get(uri, { withCredentials: true });

            return response.data;
        },
        getFormattedDateTime(date) {
            return date.toLocaleString("en-US");
        }
    },
    async created() {
        const todayDate = new Date();
        const attendances = await this.fetchAttendances(todayDate.getUTCMonth() + 1, todayDate.getUTCFullYear());

        if (attendances.length > 0) {
            const lastEntry = attendances[0];
            const lastEntryDate = new Date(lastEntry.entryDateTime);

            if (lastEntryDate.getUTCDate() === todayDate.getUTCDate() && lastEntryDate.getUTCMonth() === todayDate.getUTCMonth() && lastEntryDate.getUTCFullYear() === todayDate.getUTCFullYear()) {
                this.attendance = lastEntry;
                console.log(this.attendance);
                this.isAttendanceFound = true;
            }
        }
    }
}
</script> 
<style scoped> .content {
     margin-top: 60px;
 }

 .info-text {
     font-size: 13pt;
     color: grey;
 }

 .report-button {
     margin-top: 20px;
 }
</style>