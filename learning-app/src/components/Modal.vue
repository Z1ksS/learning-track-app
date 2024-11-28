<template>
    <div class="modal-overlay" v-if="show">
      <div class="modal-content">
        <slot></slot> <!-- елемент для контенту модального вікна -->
        <button @click="close" class="modal-close">Close</button>
      </div>
    </div>
  </template>
  
<script>
  export default {
    props: {
      show: {
        type: Boolean,
        required: true,
      },
    },
    emits: ['update:show'],
    methods: {
      close() {
        this.$emit('update:show', false);
      },
    },
  };
</script>
  
<style scoped>
  .modal-overlay {
    position: fixed;
    top: 0;
    left: 0;
    width: 100%;
    height: 100%;
    background-color: rgba(0, 0, 0, 0.5);
    display: flex;
    justify-content: center;
    align-items: center;
  }

  .modal-content {
    background: white;
    padding: 20px;
    border-radius: 5px;
    max-width: 400px;
    width: 100%;
    text-align: center;
  }

  .modal-close {
    margin-top: 20px;
    background-color: #d43c00;
    color: white;
    padding: 10px 20px;
    border: none;
    cursor: pointer;
    border-radius: 5px;
  }
</style>
  