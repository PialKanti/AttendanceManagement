<template>
    <v-card class="w-75 mx-auto">
        <v-container>
            <h3>Settings</h3>
            <AccountInfo></AccountInfo>
            <v-divider></v-divider>
            <ChangePassword></ChangePassword>
        </v-container>
    </v-card>
</template>
<script>
import AccountInfo from '@/components/settings/AccountInfo';
import ChangePassword from '@/components/settings/ChangePassword';

export default {
    name: "ApplicationSettings",
    components: {
        AccountInfo,
        ChangePassword
    }
}
</script>