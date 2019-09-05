<template>  
  <div>
    <shared-header />

    <drawing-component />
  </div>
</template>

<script>
import SharedHeader from '../shared/SharedHeader.vue'
import DrawingComponent from './DrawingComponent.vue'

export default {
  name: 'DiagramDesigner',
  components: {
    SharedHeader,
    DrawingComponent
  }
};

</script>