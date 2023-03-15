<template>
    <div v-if="item">
        <table class="table">
            <thead>
                <tr>
                    <th scope="col">#</th>
                    <th scope="col">Date</th>
                    <th scope="col">Day</th>
                    <th scope="col">Entry</th>
                    <th scope="col">Exit</th>
                    <th scope="col">Duration</th>
                </tr>
            </thead>
            <tbody>
                <tr v-for="(item, index) in item.attendances" :key="index">
                    <th scope="row">{{ index + 1 }}</th>
                    <td>{{ getFormattedDate(item.entryDateTime) }}</td>
                    <td>{{ getDayofWeek(item.entryDateTime) }}</td>
                    <td>{{ item.entryDateTime ? getTime(item.entryDateTime) : '' }}</td>
                    <td>{{ item.exitDateTime ? getTime(item.exitDateTime) : '' }}</td>
                    <td>{{ getStayDurationTime(item.entryDateTime, item.exitDateTime) }}</td>
                </tr>
            </tbody>
        </table>
    </div>
    <div v-else>
        <p class="text-center text-muted no-content-text">No data found.</p>
    </div>
</template>
<script>
import moment from 'moment';

export default {
    name: 'HistoryTable',
    props: {
        item: Object
    },
    methods: {
        getFormattedDate(dateString) {
            return moment(new Date(dateString)).format('Do MMMM, YYYY');
        },
        getDayofWeek(dateString) {
            return moment(new Date(dateString)).format('dddd');
        },
        getTime(dateString) {
            return moment(new Date(dateString)).format('LT');
        },
        getStayDurationTime(entryDateStr, exitDateStr) {
            if (!entryDateStr || !exitDateStr)
                return '';

            const entryDate = new Date(entryDateStr);
            const exitDate = new Date(exitDateStr)

            let diffInSeconds = (exitDate.getTime() - entryDate.getTime()) / 1000;
            const hour = Math.floor(diffInSeconds / 3600);
            diffInSeconds %= 3600;
            const minute = Math.floor(diffInSeconds / 60);
            const second = Math.floor(diffInSeconds % 60);

            let duration = '';
            if (hour > 0) {
                duration += hour + ((hour == 1) ? ' hr ' : ' hrs ');
            }
            if (minute > 0) {
                duration += minute + ((minute == 1) ? ' min ' : ' mins ');
            }
            if (second >= 0) {
                duration += second + ((second == 1) ? ' sec ' : ' secs ');
            }

            return duration;
        }
    }
}
</script>
<style scoped>
.no-content-text {
    margin-top: 20px;
}
</style>