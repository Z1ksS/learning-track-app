<template>
    <div class="users-container">
      <h1>Users List</h1>
        <table class="users-table">
            <thead>
            <tr>
                <th>Name</th>
                <th>Email</th>
                <th>Role</th>
                <th>Actions</th>
            </tr>
            </thead>
            <tbody>
            <tr v-for="user in users" :key="user.email">
                <td>{{ user.name }}</td>
                <td>{{ user.email }}</td>
                <td>{{ user.role }}</td>
                <td>
                    <button @click="editUser(user)">Edit</button>
                    <button @click="confirmDelete(user)">Delete</button>
                </td>
            </tr>
            </tbody>
        </table>

        <!-- Модальне вікно для редагування -->
        <Modal :show="editModalVisible.value" @update:show="editModalVisible.value = $event">
            <h2>Edit User</h2>
            <input v-model="editedUser.name" placeholder="Name" />
            <input v-model="editedUser.email" placeholder="Email" />
            <select v-model="editedUser.role">
                <option value="User">User</option>
                <option value="Admin">Admin</option>
            </select>
            <h3 v-if="errorMessage.value" class="text-error">{{errorMessage.value}}</h3>
            <button @click="updateUser">Save Changes</button>
        </Modal>

        <!-- Модальне вікно для підтвердження видалення -->
        <Modal :show="deleteModalVisible.value" @update:show="deleteModalVisible.value = $event">
            <h2>Are you sure you want to delete this user?</h2>
            <h3 v-if="errorMessage.value" class="text-error">{{errorMessage.value}}</h3>
            <button @click="deleteUser">Yes, Delete</button>
        </Modal>
    </div>
</template>
  
<script>
import { reactive, onMounted } from 'vue';
import Modal from '@/components/Modal.vue';
  
export default {
    name: 'UsersList',
    components: {
        Modal,
    },
    setup() {
        const users = reactive([]);

        const editedUser = reactive({ name: '', email: '', role: '' });
        const userToDelete = reactive({});

        const editModalVisible = reactive({ value: false });
        const deleteModalVisible = reactive({ value: false });

        const errorMessage = reactive({ value: '' });

        const fetchUsers = async () => {
            try {
                const response = await fetch('https://localhost:7059/api/User/getUsers', {
                    method: 'GET',
                    headers: {
                        'Content-Type': 'application/json',
                        Authorization: `Bearer ${localStorage.getItem('token')}`, // Додаємо токен
                    },
                });

                if (response.ok) {
                    const data = await response.json();
                    users.push(...data);
                } else {
                    console.error('Failed to fetch users');
                }
            } catch (error) {
                console.error('Error:', error);
            }
        };
  
        onMounted(() => {
            fetchUsers(); // Завантажуємо дані під час завантаження компонента
        });

        const editUser = (user) => {
            // Копіюємо дані користувача в редаговану форму
            Object.assign(editedUser, user);
            editModalVisible.value = true;
        };

        const updateUser = async () => {
            try {
                // Выполняем PATCH запрос
                const response = await fetch(`https://localhost:7059/api/User/updateUser?email=${editedUser.email}`, {
                    method: 'PATCH',
                    headers: {
                        'Content-Type': 'application/json',
                        Authorization: `Bearer ${localStorage.getItem('token')}`,
                    },
                    body: JSON.stringify({
                        name: editedUser.name,
                        email: editedUser.email,
                        role: editedUser.role,
                    }),
                });

                if (response.ok) {
                    const updatedUser = await response.json();

                    const userIndex = users.findIndex((user) => user.email === updatedUser.email);

                    if (userIndex !== -1) {
                        users[userIndex] = updatedUser;
                    }

                    editModalVisible.value = false;
                } else {
                    console.error('Failed to update user:', await response.text());
                }
            } catch (error) {
                console.error('Error during user update:', error);
            }
        };

        const confirmDelete = (user) => {
            userToDelete.value = user;
            deleteModalVisible.value = true;
        };

        const deleteUser = async () => {
            try {
                const response = await fetch(`https://localhost:7059/api/User/deleteUser?email=${userToDelete.value.email}`, {
                    method: 'DELETE',
                    headers: {
                        Authorization: `Bearer ${localStorage.getItem('token')}`,
                    },
                });

                if (response.ok) {
                    // Видаляємо користувача з локального списка
                    const index = users.findIndex((user) => user.email === userToDelete.value.email);

                    if (index !== -1) {
                        users.splice(index, 1);
                    }

                    deleteModalVisible.value = false;
                } else {
                    console.error('Failed to delete user:', await response.text());
                }
            } catch (error) {
                console.error('Error during user deletion:', error);
            }
        };
    
        return {
            users,
            editModalVisible,
            deleteModalVisible,
            editedUser,
            userToDelete,
            editUser,
            updateUser,
            confirmDelete,
            deleteUser,
            errorMessage
        };
    },
};
</script>
  
<style scoped>
    .users-container {
        max-width: 800px;
        margin: 0 auto;
        padding: 20px;
        display: flex;
        flex-direction: column;
        align-items: center;
    }

    .users-table {
        width: 100%;
        border-collapse: collapse;
        margin-top: 20px;
    }

    .users-table th,
    .users-table td {
        border: 1px solid #ddd;
        padding: 10px;
        text-align: left;
    }

    .users-table th {
        font-weight: bold;
    }

    button {
        color: white;
        background-color: transparent;
        padding: 5px 10px;
        border: none;
        cursor: pointer;
        border-radius: 5px;
        margin-right: 5px;
        transition: color 0.3s ease;
    }

    button:hover {
        color: #00bcd4;
    }

    .text-error {
        position: relative;
        top: 5px;
        color: #d43c00;
        font-size: 10px;
    }
</style>
  