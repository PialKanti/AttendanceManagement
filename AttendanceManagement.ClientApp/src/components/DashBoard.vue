<template>
    <v-card class="mx-auto" prepend-icon="mdi-calendar-badge" title="Today's Attendance" :subtitle="currentDate"
        width="450">
        <v-card-text class="card-content">
            <div v-if="isAttendanceFound">
                <p>
                    <span class="text-green">
                        <v-icon icon="mdi-clock-in"></v-icon>
                        In time:
                    </span>
                    {{ getFormattedTime(new Date(attendance.entryDateTime)) }}
                </p>
                <div v-if="isExitTimeFound">
                    <p>
                        <span class="text-red">
                            <v-icon icon="mdi-clock-out"></v-icon>
                            Out time:
                        </span>
                        {{ getFormattedTime(new Date(attendance.exitDateTime)) }}
                    </p>
                </div>
                <p class="text-muted">You have been inside for {{ stayDuration }}</p>
            </div>
            <div class="no-attendance" v-else>
                <p class="text-center text-muted">No data found for today.</p>
            </div>
        </v-card-text>
        <v-divider v-if="isAttendanceNotCompleted"></v-divider>
        <v-card-actions v-if="isAttendanceNotCompleted">
            <v-btn class="mx-auto" color="error" @click="reportExit" v-if="isAttendanceFound" variant="outlined">Report
                exit
                time</v-btn>
            <v-btn class="mx-auto" color="success" @click="reportEntry" v-else variant="outlined">Report entry time</v-btn>
        </v-card-actions>
    </v-card>
</template>
<script>
import { useAuthStore } from '@/stores/AuthStore';
import { HttpStatusCode } from 'axios';
import { client } from '@/clients/HttpClient'
import moment from 'moment';

let durationTimer;
export default {
    setup() {
        const authStore = useAuthStore();

        return { authStore };
    },
    name: 'DashBoard',
    data() {
        return {
            attendance: {},
            stayDuration: ''
        }
    },
    computed: {
        isReportExitButtonShown: function () {
            return this.attendance.entryDateTime && !this.attendance.exitDateTime;
        },
        isAttendanceFound: function () {
            return this.attendance.entryDateTime;
        },
        isExitTimeFound: function () {
            return this.attendance.exitDateTime;
        },
        currentDate: function () {
            return this.getFormattedDate(new Date());
        },
        isAttendanceNotCompleted: function () {
            return this.attendance.entryDateTime == null || this.attendance.exitDateTime == null;
        }
    },
    methods: {
        async reportEntry() {
            const todayDate = new Date();
            const data = {
                username: this.authStore.user.username,
                entryDateTime: todayDate.toISOString()
            }

            const uri = 'attendances';
            const response = await client.post(uri, data, { withCredentials: true });

            if (response.status === HttpStatusCode.Created) {
                this.attendance = response.data;
                this.isAttendanceFound = true;
                this.showLiveDurationTimer();
            }
        },
        async reportExit() {
            const todayDate = new Date();
            const data = {
                id: this.attendance.id,
                exitDateTime: todayDate.toISOString()
            }

            const uri = 'attendances/' + this.attendance.id;
            const response = await client.put(uri, data, { withCredentials: true });

            if (response.status === HttpStatusCode.NoContent) {
                this.attendance.exitDateTime = data.exitDateTime;
                this.stopLiveDurationTimer();
                this.updateStayTime(new Date(this.attendance.exitDateTime));
            }
        },
        async fetchAttendances(month, year) {
            const uri = 'users/' + this.authStore.user.username + '/attendances?month=' + month + '&year=' + year;
            const response = await client.get(uri, { withCredentials: true });

            return response.data;
        },
        getFormattedDate(date) {
            return moment(date).format('dddd, Do MMMM, YYYY');
        },
        getFormattedTime(date) {
            return moment(date).format('LT');
        },
        getStayDurationTime(entryDate, currentDate) {
            let diffInSeconds = (currentDate.getTime() - entryDate.getTime()) / 1000;
            const hour = Math.floor(diffInSeconds / 3600);
            diffInSeconds %= 3600;
            const minute = Math.floor(diffInSeconds / 60);
            const second = Math.floor(diffInSeconds % 60);

            let duration = '';
            if (hour > 0) {
                duration += hour + ((hour == 1) ? ' hour ' : ' hours ');
            }
            if (minute > 0) {
                duration += minute + ((minute == 1) ? ' minute ' : ' minutes ');
            }
            if (second >= 0) {
                duration += second + ((second == 1) ? ' second. ' : ' seconds. ');
            }

            return duration;
        },
        updateStayTime(exitDateTime = new Date()) {
            this.stayDuration = this.getStayDurationTime(new Date(this.attendance.entryDateTime), exitDateTime);
        },
        showLiveDurationTimer() {
            this.updateStayTime();
            durationTimer = setTimeout(this.showLiveDurationTimer, 1000);
        },
        stopLiveDurationTimer() {
            clearTimeout(durationTimer);
        }
    },
    async mounted() {
        const todayDate = new Date();
        const attendances = await this.fetchAttendances(todayDate.getUTCMonth() + 1, todayDate.getUTCFullYear());

        if (attendances.length > 0) {
            const lastEntry = attendances[0];
            const lastEntryDate = new Date(lastEntry.entryDateTime);

            if (lastEntryDate.getDate() === todayDate.getDate() && lastEntryDate.getMonth() === todayDate.getUTCMonth() && lastEntryDate.getFullYear() === todayDate.getFullYear()) {
                this.attendance = lastEntry;
                this.isAttendanceFound = true;

                if (this.attendance.entryDateTime && this.attendance.exitDateTime) {
                    this.updateStayTime(new Date(this.attendance.exitDateTime));
                } else {
                    this.showLiveDurationTimer();
                }
            }
        }
    },
    unmounted() {
        this.stopLiveDurationTimer();
    }
}
</script> 
<style scoped>
.card-content {
    margin-top: 20px;
}
</style>