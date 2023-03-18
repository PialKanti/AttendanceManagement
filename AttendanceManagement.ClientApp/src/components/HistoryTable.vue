<template>
    <div v-if="isAttendanceFound">
        <v-data-table :headers="headers" :items="attendances" items-per-page="5"></v-data-table>
    </div>
    <div v-else>
        <p class="text-center text-muted no-content-text">No data found.</p>
    </div>
</template>
<script>
import moment from 'moment';
import { VDataTable } from 'vuetify/labs/VDataTable'

export default {
    name: 'HistoryTable',
    components: {
        VDataTable
    },
    props: {
        item: Object
    },
    data() {
        return {
            headers: [
                { title: '#', align: 'left', key: 'index', sortable: false },
                { title: 'Date', align: 'left', key: 'date', sortable: false },
                { title: 'Day', align: 'left', key: 'day', sortable: false },
                { title: 'Entry', align: 'left', key: 'entry', sortable: false },
                { title: 'Exit', align: 'left', key: 'exit', sortable: false },
                { title: 'Duration', align: 'left', key: 'duration', sortable: false }
            ],
            attendances: []
        }
    },
    computed: {
        isAttendanceFound: function () {
            if (this.item && this.item.attendances) {
                return this.item.attendances.length > 0;
            }

            return 0;
        }
    },
    watch: {
        item(newItem) {
            this.attendances = this.prepareAttendanceData(newItem);
        }
    },
    methods: {
        getFormattedDate(dateString) {
            return moment(new Date(dateString)).format('Do MMMM, YYYY');
        },
        getDayofWeek(dateString) {
            return moment(new Date(dateString)).format('dddd');
        },
        getTime(dateString) {
            if (dateString)
                return moment(new Date(dateString)).format('LT');
            return '';
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
        },
        prepareAttendanceData(attendanceObj) {
            let result = [];

            if (attendanceObj && attendanceObj.attendances) {
                const itemArray = attendanceObj.attendances;

                for (let i = 0; i < itemArray.length; i++) {
                    result.push({
                        index: i + 1,
                        date: this.getFormattedDate(itemArray[i].entryDateTime),
                        day: this.getDayofWeek(itemArray[i].entryDateTime),
                        entry: this.getTime(itemArray[i].entryDateTime),
                        exit: this.getTime(itemArray[i].exitDateTime),
                        duration: this.getStayDurationTime(itemArray[i].entryDateTime, itemArray[i].exitDateTime)
                    });
                }
            }
            return result;
        }
    }
}
</script>
<style scoped>
.no-content-text {
    margin-top: 20px;
}
</style>