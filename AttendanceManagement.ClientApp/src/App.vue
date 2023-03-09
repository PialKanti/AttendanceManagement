<template>
  <NavBar />
  <div class="container">
    <DashBoard v-if="authStore.user.isLoggedIn" />
    <LoginForm v-else />
  </div>
</template>

<script>
import NavBar from './components/NavBar.vue';
import DashBoard from './components/DashBoard.vue';
import LoginForm from './components/LoginForm.vue'
import { useAuthStore } from './stores/AuthStore';

export default {
  setup() {
    const authStore = useAuthStore();

    return { authStore };
  },
  name: 'App',
  components: {
    NavBar,
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
