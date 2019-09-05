<template>  
  <div>

    <div class="container">
      <div class="row">
        <h2 class="col-sm-offset-3">Canvas:</h2>
      </div>
      <div class="row">
          <div class="col-sm-9 col-sm-offset-3">
              <canvas id="canvas" />
          </div>
      </div>
      <div class="row">
        <button type="button" @click="drawRectangle">Draw</button> 
      </div>
    </div>

  </div>
</template>

<script>

import { fabric } from 'fabric-browseronly'


export default {
  name: 'DrawingComponent',
  data() {
    return {
      canvas
    };
  },
  methods: {
    drawRectangle (){

        var rect = new fabric.Rect({
            left: 100,
            top: 100,
            fill: 'red',
            width: 20,
            height: 20,
            angle: 45
        });

        this.canvas.add(rect);
    }
  },
  components: {
  },
  mounted() {
      this.canvas = new fabric.Canvas('canvas')
  }
};

</script>