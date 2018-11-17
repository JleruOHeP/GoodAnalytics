<template>  
  <div>
    <home-hero />
    
    <shared-header />

    <home-content />

    <home-subscribe />
  </div>
</template>

<script>

import SharedHeader from '../shared/SharedHeader.vue'
import HomeContent from './HomeContent.vue'
import HomeSubscribe from './HomeSubscribe.vue'
import HomeHero from './HomeHero.vue'

export default {
  name: 'Home',
  components: {
    SharedHeader,
    HomeContent,
    HomeSubscribe,
    HomeHero
  }
};

</script>