<template>
  <NavBar />
  <div class="container">
    <LoginForm />
  </div>
</template>

<script>
import NavBar from './components/NavBar.vue';
import LoginForm from './components/LoginForm.vue'

export default {
  name: 'App',
  components: {
    NavBar,
    LoginForm
  }
}
</script>

<style>
.container {
  margin-top: 60px;
}
</style>
