<template>
    <nav class="navbar">
      <div class="navbar-container">
        <router-link to="/" class="navbar-link">Home</router-link>

        <ul class="navbar-menu">
          <li>
            <router-link to="/login" class="navbar-link">Login</router-link>
          </li>
          <li>
            <router-link to="/register" class="navbar-link">Register</router-link>
          </li>
        </ul>
      </div>
    </nav>
</template>
  
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
  }
</style>
  