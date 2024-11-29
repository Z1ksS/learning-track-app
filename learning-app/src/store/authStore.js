import { defineStore } from 'pinia';
import { jwtDecode } from 'jwt-decode'; 

export const useAuthStore = defineStore('auth', {
  state: () => ({
    isAuthenticated: false,
    userRole: null,
    userEmail: null
  }),
  actions: {
    login(token) {
      this.isAuthenticated = true;
      localStorage.setItem('token', token); 

      // Розпаковуємо роль з токена
      const decodedToken = jwtDecode(token);
      this.userRole = decodedToken["http://schemas.microsoft.com/ws/2008/06/identity/claims/role"];
      this.userEmail = decodedToken["http://schemas.xmlsoap.org/ws/2005/05/identity/claims/name"];
    },
    logout() {
      this.isAuthenticated = false;
      this.userRole = null;
      this.userEmail = null;
      localStorage.removeItem('token');
    },
    initialize() {
      const token = localStorage.getItem('token');
      if (token) {
        this.isAuthenticated = true;

        const decodedToken = jwtDecode(token);
        this.userRole = decodedToken["http://schemas.microsoft.com/ws/2008/06/identity/claims/role"];
        this.userEmail = decodedToken["http://schemas.xmlsoap.org/ws/2005/05/identity/claims/name"];
      }
    }
  },
});
