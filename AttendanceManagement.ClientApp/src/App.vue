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
#app {
  font-family: Avenir, Helvetica, Arial, sans-serif;
  -webkit-font-smoothing: antialiased;
  -moz-osx-font-smoothing: grayscale;
  color: #2c3e50;
}

.container {
  margin-top: 60px;
}
</style>
