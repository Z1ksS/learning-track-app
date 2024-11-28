<template>
  <NavBar />

  <main class="form-signin">
    <router-view/>
  </main>
</template>

<script setup>
  import NavBar from './components/NavBar.vue';
</script>

<style scoped>
  .form-signin {
    width: 100%;
    max-width: 330px;
    padding: 15px;
    margin: auto;
  }
</style>
