<template>
    <div class="content">
        <p class="text-center info-text">No data found for today.</p>
        <div class="text-center report-button">
            <button type="button" class="btn btn-success" @click="reportEntry">Report entry time</button>
        </div>
    </div>
</template>
<script>
export default {
    name: 'DashBoard',
    methods: {
        reportEntry() {
            const currentDate = Date.UTC();
            console.log(currentDate);
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