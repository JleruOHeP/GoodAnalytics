<template>
    <header class="sticky">
      <a href="#" class="logo">Logo</a>
      <a href="/" class="button">Home</a>
      <a href="#chart" class="button" v-on:click="onNavigate('chart')">Chart</a>
    </header>
</template>

<script>
import routing from '../../routing/route';

export default {
  name: 'SharedHeader',
  methods: {
    onNavigate (route) {      
      routing.setCurrentRoute(route);
    }
  }
};
</script>