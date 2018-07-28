<template>  
  <div>
    <shared-header />

    <div class="container">      
      <div class="row">
        <h3>Not Found!</h3>
      </div>      
    </div>
  </div>
</template>

<script>
import SharedHeader from './shared/SharedHeader.vue'

export default {
  name: 'NotFound',
  components: {
    SharedHeader
  }
};
</script>