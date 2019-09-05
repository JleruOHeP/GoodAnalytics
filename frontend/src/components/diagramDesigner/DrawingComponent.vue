<template>  
  <div>

    <div class="container">
      <div class="row">
        <h2 class="col-sm-offset-3">Canvas:</h2>
      </div>
      <div class="row">
        <div class="col-sm-6 col-sm-offset-3">
            <canvas id="canvas"/>
        </div>          
      </div>
      <div class="row">
          <button type="button" @click="setShape(0)">Nothing</button>
          <button type="button" @click="setShape(1)">Draw event</button> 
          <button type="button" @click="setShape(2)">Draw Process</button> 
          <button type="button" @click="setShape(3)">Draw Line</button> 
      </div>
    </div>

  </div>
</template>

<script>

import { fabric } from 'fabric-browseronly'

var ShapesEnum = {"None": 0, "Event": 1, "Process": 2, "Line": 3};
Object.freeze(ShapesEnum)

export default {
  name: 'DrawingComponent',
  data() {
    return {
        canvas: null,
        currentShape: ShapesEnum.Event
    };
  },
  methods: {
      addShape(event){
        var pointer = this.canvas.getPointer(event.e);
        var posX = pointer.x;
        var posY = pointer.y;

        var shape = 0;

console.log('current shape: ' + this.currentShape);
        switch (this.currentShape)
        {
          case ShapesEnum.Event:
              console.log('adding event');
            shape = new fabric.Rect({
                left: posX,
                top: posY,
                fill: 'green',
                width: 20,
                height: 20,
                angle: 45
            });
            break;
          case ShapesEnum.Process:
              console.log('adding process');
            shape = new fabric.Rect({
                left: posX,
                top: posY,
                fill: 'red',
                width: 40,
                height: 20
            });
            break;
          case ShapesEnum.Line:
            break;
        }
        
        if (shape != 0)
        {
            this.canvas.add(shape);
        }
        
      },

      setShape (shapeType){
          this.currentShape = shapeType;
          console.log('current shape: ' + this.currentShape);
      }
  },
  components: {
  },
  mounted: function() {
      this.canvas = new fabric.Canvas('canvas', {
        backgroundColor: "white"
      });
      
      this.canvas.on('mouse:up', this.addShape);
  }
};

</script>