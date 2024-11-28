import './assets/base.css'

import { createApp } from 'vue'
import { createPinia } from 'pinia'
import App from './App.vue'
import router from './router'
import { useAuthStore } from './store/authStore'

const app = createApp(App);

app.use(createPinia());
app.use(router);

const authStore = useAuthStore();
authStore.initialize();

app.mount('#app')
