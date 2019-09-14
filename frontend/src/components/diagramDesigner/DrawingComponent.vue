<template>  
  <div>

    <div class="container">
      <div class="row">
        <h2 class="col-sm-offset-3">Canvas:</h2>
      </div>
      <div class="row">
        <div class="col-sm-6 col-sm-offset-3" ref="canvas-wrapper">
            <canvas id="canvas">
            </canvas>
        </div>          
      </div>

      <div class="row">
        <div class="col-sm-6 col-sm-offset-3">
          <div class="button-group">
            <button type="button" v-bind:class="{ primary: currentShapeType === 0, tertiary: currentShapeType !== 0 }" @click="setShape(0)">Nothing</button>
            <button type="button" v-bind:class="{ primary: currentShapeType === 1, tertiary: currentShapeType !== 1 }" @click="setShape(1)">Draw event</button> 
            <button type="button" v-bind:class="{ primary: currentShapeType === 2, tertiary: currentShapeType !== 2 }" @click="setShape(2)">Draw Process</button> 
            <button type="button" v-bind:class="{ primary: currentShapeType === 3, tertiary: currentShapeType !== 3 }" @click="setShape(3)">Draw Line</button> 
          </div>
        </div>
      </div>
    </div>

  </div>
</template>

<script>

import { fabric } from 'fabric-browseronly'
import { lineArrow } from './FabricArrow.js' 

var ShapesEnum = {"None": 0, "Event": 1, "Process": 2, "Line": 3};
Object.freeze(ShapesEnum)
export default {
  name: 'DrawingComponent',
  data() {
    return {
        canvas: null,
        currentShapeType: ShapesEnum.Event,
        currentShape: null
    };
  },
  methods: {      
      addShape(event){
        var pointer = this.canvas.getPointer(event.e);
        
        switch (this.currentShapeType)
        {
          case ShapesEnum.Event:
            this.currentShape = new fabric.Circle({
                left: pointer.x,
                top: pointer.y,
                fill: 'green',
                radius: 0,
                hasRotatingPoint: false,
                originX: 'center', originY: 'center'
            });
            break;
          case ShapesEnum.Process:
            this.currentShape = new fabric.Rect({
                left: pointer.x,
                top: pointer.y,
                fill: 'red',
                width: 0,
                height: 0,
                hasRotatingPoint: false,
                lockUniScaling: true
            });
            break;
          case ShapesEnum.Line:
            var points = [pointer.x, pointer.y, pointer.x, pointer.y];
            this.currentShape = new lineArrow(points, {
              strokeWidth: 5,
              fill: 'black',
              stroke: 'black',
              originX: 'center',
              originY: 'center',
              hasBorders: false,
              hasRotatingPoint: false,
              perPixelTargetFind: true,
              heads: [1, 0]
            });
            
            break;
        }
        
        if (this.currentShape != null)
        {
            this.canvas.add(this.currentShape);
        }
      },

      finishShape(){
        if (this.currentShape == null) {
          return;
        }

        this.canvas.setActiveObject(this.currentShape);
        this.currentShape = null;
        this.canvas.renderAll();
      },

      resizeShape(event){
        if (this.currentShape == null) {
          return;
        }

        var pointer = this.canvas.getPointer(event.e);

        switch (this.currentShapeType)
        {
          case ShapesEnum.Event:
            this.currentShape.set({ radius: Math.abs(this.currentShape.left - pointer.x) });
            break;
          case ShapesEnum.Process:
            this.currentShape.set({ width: Math.abs(this.currentShape.left - pointer.x) });
            this.currentShape.set({ height: Math.abs(this.currentShape.top - pointer.y) });
            break;
          case ShapesEnum.Line:
            this.currentShape.set({ x2: pointer.x });
            this.currentShape.set({ y2: pointer.y });
            break;
        }

        this.currentShape.setCoords();
        this.canvas.renderAll();
      },

      setShape (shapeType){
          this.currentShapeType = shapeType;

          this.canvas.selection = shapeType === 0;
      },

      processKeyboard (e) {
          if (46 === e.keyCode) {
            // 46 is Delete key
            this.canvas.remove(this.canvas.getActiveObject());
          }
      }
  },
  components: {
  },
  mounted: function() {
      var canvasWrapper = this.$refs["canvas-wrapper"];
      canvasWrapper.tabIndex = 1000;
      canvasWrapper.addEventListener("keydown", this.processKeyboard, false);

      this.canvas = new fabric.Canvas('canvas', {
        backgroundColor: "white",
        width: canvasWrapper.offsetWidth,
        height: 500,
        selection: false,
        isDrawingMode: false
      });

      fabric.lineArrow = lineArrow;

      this.canvas.calcOffset();
      this.canvas.on('mouse:down', this.addShape);
      this.canvas.on('mouse:up', this.finishShape);
      this.canvas.on('mouse:move', this.resizeShape);
  }
};

</script>

<style lang="scss" scoped>

.canvas-wrapper {
  min-height: 40vh;
}
</style>
