<template>
  <NavBar />
  <div class="container">
    <AlertMessage v-if="alertStore.isShown" />
    <DashBoard v-if="authStore.user.isLoggedIn" />
    <LoginForm v-else />
  </div>
</template>

<script>
import NavBar from './components/NavBar.vue';
import AlertMessage from './components/AlertMessage.vue';
import DashBoard from './components/DashBoard.vue';
import LoginForm from './components/LoginForm.vue'
import { useAuthStore } from './stores/AuthStore';
import { useAlertStore } from '@/stores/AlertStore';

export default {
  setup() {
    const authStore = useAuthStore();
    const alertStore = useAlertStore();

    return { authStore, alertStore };
  },
  name: 'App',
  components: {
    NavBar,
    AlertMessage,
    LoginForm,
    DashBoard
  }
}
</script>

<style>
.container {
  margin-top: 60px;
}
</style>
