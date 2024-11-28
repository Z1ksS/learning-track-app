<template>
    <div class="block-form">
      <h1>Please login</h1>
  
      <form @submit.prevent="submit">  
        <input v-model="data.email" type="email" class="form-control" placeholder="Email" required>
  
        <input v-model="data.password" type="password" class="form-control" placeholder="Password" required>
  
        <button type="submit">Submit</button>
      </form>
  
      <h3 v-if="errorMessage.value" class="text-error">{{errorMessage.value}}</h3>
    </div>
  </template>
  
  <script>
  import {reactive} from 'vue';
  import {useRouter} from "vue-router";
  
  export default {
    name: "Register",
    setup() {
      const data = reactive({
        name: '',
        email: '',
        password: ''
      });
  
      const router = useRouter();
  
      const errorMessage = reactive({ value: '' });
  
      const submit = async () => {
        try {
          const response = await fetch('https://localhost:7059/api/User/register', {
            method: 'POST',
            headers: {'Content-Type': 'application/json'},
            body: JSON.stringify(data)
          });
  
          if (!response.ok) {
            if (response.status === 400) {
              const errorText = await response.text();
              errorMessage.value = errorText;
            } else {
              errorMessage.value = 'An unexpected error occurred. Please try again later.';
            }
            return;
          }
  
          await router.push('/login');
        } catch (error) {
          errorMessage.value = 'An error occurred while connecting to the server.';
          console.error(error);
        }
      };
  
      return {
        data,
        submit,
        errorMessage
      };
    }
  }
  </script>
  
  <style scoped>
  .block-form { 
    display: flex;
    flex-direction: column;
    align-items: center;
  }
  
  form {
    display: flex;
    flex-direction: column;
    gap: 5px;
  }
  
  .text-error {
    position: relative;
    top: 5px;
    color: #d43c00;
    font-size: 10px;
  }
  </style>