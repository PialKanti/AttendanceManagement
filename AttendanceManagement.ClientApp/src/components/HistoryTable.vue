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
                    <td>{{ getTime(item.entryDateTime) }}</td>
                    <td></td>
                    <td></td>
                </tr>
            </tbody>
        </table>
    </div>
    <div v-else>
        <p class="text-center text-muted">No data found.</p>
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
        }
    }
}
</script>