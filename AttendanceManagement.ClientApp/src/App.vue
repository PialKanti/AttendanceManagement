<template>
  <NavBar />
  <div class="container">
    <AlertMessage v-if="alertStore.isShown" />
    <router-view></router-view>
  </div>
</template>

<script>
import NavBar from './components/NavBar.vue';
import AlertMessage from './components/AlertMessage.vue';
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
    AlertMessage
  }
}
</script>

<style>
.container {
  margin-top: 60px;
  margin-bottom: 50px;
}
</style>
