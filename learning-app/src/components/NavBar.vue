<template>
    <nav class="navbar">
      <div class="navbar-container">
        <router-link to="/" class="navbar-link">Home</router-link>

        <ul class="navbar-menu">
          <li v-if="!authStore.isAuthenticated">
          <router-link to="/login" class="navbar-link">Login</router-link>
          </li>
          <li v-if="!authStore.isAuthenticated">
            <router-link to="/register" class="navbar-link">Register</router-link>
          </li>
          <li v-if="authStore.userRole === 'Admin'">
            <router-link to="/users" class="navbar-link">View Users</router-link>
          </li>
          <li v-if="authStore.isAuthenticated">
            <a @click="logout" class="navbar-link">Log Out</a>
          </li>
        </ul>
      </div>
    </nav>
</template>

<script>
import { useRouter } from "vue-router";
import { useAuthStore } from '../store/authStore';

export default {
  setup() {
    const router = useRouter();
    const authStore = useAuthStore();

    const logout = () => {
      authStore.logout();
      router.push('/');
    };

    return {
      authStore,
      logout,
    };
  },
};
</script>
  
<style scoped>
  .navbar {
    display: flex;
    justify-content: space-between;
    align-items: center;
    background-color: #333;
    padding: 0.5rem 1rem;
    color: white;
  }
  
  .navbar-container {
    display: flex;
    justify-content: space-between;
    width: 100%;
  }
  
  .navbar-menu {
    display: flex;
    list-style: none;
    margin: 0;
    padding: 0;
  }
  
  .navbar-menu li {
    margin-left: 1rem;
  }
  
  .navbar-link {
    font-size: 1.5rem;
    color: white;
    text-decoration: none;
    transition: color 0.3s ease;
  }
  
  .navbar-link:hover {
    color: #00bcd4;
    cursor: pointer;
  }
</style>
  