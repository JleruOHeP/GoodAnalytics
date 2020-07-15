<template>  
  <div>
    <home-hero />
    
    <shared-header />

    <home-content />
  </div>
</template>

<script>

import SharedHeader from '../shared/SharedHeader.vue'
import HomeContent from './HomeContent.vue'
import HomeHero from './HomeHero.vue'

export default {
  name: 'Home',
  components: {
    SharedHeader,
    HomeContent,
    HomeHero
  }
};

</script>