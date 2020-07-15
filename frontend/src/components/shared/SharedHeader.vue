<template>
    <header class="sticky">
      <a href="#" class="logo"><img src="https://s3-ap-southeast-2.amazonaws.com/good-analytics.org/assets/2.png" /></a>
      <a href="/" class="button">Home</a>
      <a href="#chart" class="button" v-on:click="onNavigate('chart')">Statistical analysis</a>
      <a href="#diagram" class="button" v-on:click="onNavigate('diagram')">Process Simulation</a>
      <a href="#contactus" class="button" v-on:click="onNavigate('contactus')">Contact Us</a>
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