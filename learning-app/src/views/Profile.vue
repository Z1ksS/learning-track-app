<template>
    <div class="profile-container">
      <h1>User Profile</h1>
      <div v-if="loading.value" class="loading">Loading...</div>
      <div v-else-if="error.value" class="error">{{ error }}</div>
      <div v-else class="profile-info">
        <p><strong>Name:</strong> {{ user.name }}</p>
        <p><strong>Email:</strong> {{ user.email }}</p>
        <p><strong>Role:</strong> {{ user.role }}</p>
      </div>
    </div>
</template>
  
<script>
  import { reactive, onMounted } from 'vue';
  import { useAuthStore } from '@/store/authStore';
  
  export default {
    name: 'Profile',
    setup() {
      const user = reactive({ name: '', email: '', role: '' });
      const loading = reactive({ value: true });
      const error = reactive({ value: null });
      const authStore = useAuthStore();
  
      const fetchProfile = async () => {
        try {
          const response = await fetch(`https://localhost:7059/api/User/getUserByEmail?email=${authStore.userEmail}`, {
            method: 'GET',
            headers: {
                'Content-Type': 'application/json',
                Authorization: `Bearer ${localStorage.getItem('token')}`,
            },
          });
  
          if (response.ok) {
            const data = await response.json();
            Object.assign(user, data);
            console.log(loading.value);
          } else {
            error.value = 'Failed to load user profile.';
          }
        } catch (e) {
          error.value = 'Error while connecting to the server.';
          console.error(e);
        } finally {
          loading.value = false;
        }
      };
  
      onMounted(() => {
        fetchProfile();
      });
  
      return {
        user,
        loading,
        error,
      };
    },
  };
</script>
  
<style scoped>
  .profile-container {
    max-width: 600px;
    margin: 0 auto;
    padding: 20px;
    display: flex;
    flex-direction: column;
    align-items: center;
  }
  
  .loading {
    text-align: center;
    font-size: 18px;
    color: #999;
  }
  
  .error {
    text-align: center;
    color: #d43c00;
    font-size: 18px;
  }
  
  .profile-info {
    padding: 20px;
    border-radius: 5px;
    box-shadow: 0 0 10px rgba(0, 0, 0, 0.1);
  }
  
  .profile-info p {
    margin: 10px 0;
    position: relative;
    left: 10%;
  }
  
  .profile-info strong {
    color: #333;
  }
</style>
  