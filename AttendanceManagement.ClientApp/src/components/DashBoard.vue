<template>
    <div class="content">
        <div class="text-center attendance-given" v-if="isAttendanceFound">
            <p class="today-date">{{ getFormattedDate(new Date(attendance.entryDateTime)) }}</p>
            <p><span class="in-time font-13pt">In time: </span>
                {{ getFormattedTime(new Date(attendance.entryDateTime)) }}
            </p>
            <p v-if="isExitTimeFound"><span class="out-time font-13pt">Out time: </span>
                {{ getFormattedTime(new Date(attendance.exitDateTime)) }}
            </p>
            <p class="text-muted">You have been inside for {{ stayDuration }}</p>
            <div class="text-center report-button" v-if="isReportExitButtonShown">
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

            if (lastEntryDate.getUTCDate() === todayDate.getUTCDate() && lastEntryDate.getUTCMonth() === todayDate.getUTCMonth() && lastEntryDate.getUTCFullYear() === todayDate.getUTCFullYear()) {
                this.attendance = lastEntry;
                this.isAttendanceFound = true;
                if (this.attendance.entryDate && !this.attendance.exitDateTime) {
                    this.showLiveDurationTimer();
                } else {
                    this.updateStayTime();
                }
            }
        }
    },
    unmounted() {
        this.stopLiveDurationTimer();
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

 .today-date {
     font-weight: 700;
     font-style: oblique;
     font-size: 15pt;
 }

 .in-time {
     color: green;
     font-weight: 700;
 }

 .out-time {
     color: red;
     font-weight: 700;
 }

 .font-13pt {
     font-size: 13pt;
 }
</style>