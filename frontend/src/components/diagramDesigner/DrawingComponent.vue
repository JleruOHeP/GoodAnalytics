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

//import { fabric } from 'fabric-browseronly'
import { lineArrow } from './FabricArrow.js' 
import { getObjectByCoordinates, getNextId,  getObjectById } from './CanvasHelper.js' 

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
        
        var newId = getNextId(this.canvas);
        switch (this.currentShapeType)
        {
          case ShapesEnum.Event:
            this.currentShape = new fabric.Circle({
                left: pointer.x,
                top: pointer.y,
                fill: 'green',
                radius: 0,
                hasRotatingPoint: false,
                originX: 'center', originY: 'center',
                incomingArrows: [],
                outgoingArrows: [],
                shapeType: ShapesEnum.Event,
                id: newId
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
                lockUniScaling: true,
                incomingArrows: [],
                outgoingArrows: [],
                shapeType: ShapesEnum.Process,
                id: newId
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
              heads: [1, 0],
              shapeType: ShapesEnum.Line,
              lockMovementX: true,
              lockMovementY: true,
              id: newId
            });

            if (event.target && 
               (event.target.shapeType === ShapesEnum.Event || event.target.shapeType === ShapesEnum.Process)){
                  var bound = event.target.getBoundingRect();
                  let centerX = bound.left + bound.width / 2;
                  let centerY = bound.top + bound.height / 2;
                  
                  this.currentShape.set({ 'x1': centerX, 'y1': centerY });
                  event.target.outgoingArrows.push(this.currentShape.id);
                  
            }
            
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

        if (this.currentShapeType === ShapesEnum.Line) {
          var pointer = this.canvas.getPointer(event.e);
          var endShape = getObjectByCoordinates(this.canvas, pointer.x, pointer.y);

          if (endShape &&
             (endShape.shapeType === ShapesEnum.Event || endShape.shapeType === ShapesEnum.Process)){
            var bound = endShape.getBoundingRect();
            var centerX = bound.left + bound.width / 2;
            var centerY = bound.top + bound.height / 2;

            endShape.incomingArrows.push(this.currentShape.id);
                        
            this.currentShape.set({ 'x2': centerX, 'y2': centerY });
            this.currentShape.setCoords();
          }
        }

        this.canvas.setActiveObject(this.currentShape);
        this.currentShape = null;
        this.canvas.renderAll();

        this.setShape(ShapesEnum.None);
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

      updateArrows(event) {
        var p = event.target;

        var bound = p.getBoundingRect();
        var centerX = bound.left + bound.width / 2;
        var centerY = bound.top + bound.height / 2;


        if (p.outgoingArrows) {
            for (var i = 0; i < p.outgoingArrows.length; i++) {
                var line = getObjectById(this.canvas, p.outgoingArrows[i]);
                line.set({ 'x1': centerX, 'y1': centerY});
                line.setCoords();
            }
        }

        if (p.incomingArrows) {
            for (var i = 0; i < p.incomingArrows.length; i++) {
                var line = getObjectById(this.canvas, p.incomingArrows[i]);
                line.set({ 'x2': centerX, 'y2': centerY });
                line.setCoords();
            }
        }

        this.canvas.renderAll();
      },

      setShape (shapeType){
          this.currentShapeType = shapeType;

          let draggable = shapeType === 0;
          this.canvas.selection = draggable;
          this.canvas.forEachObject(function (obj) {
              if (obj.shapeType !== ShapesEnum.Line) {
                  obj.lockMovementX = !draggable;
                  obj.lockMovementY = !draggable;
              }
          });
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
      this.canvas.on('object:moving', this.updateArrows);
  }
};

</script>

<style lang="scss" scoped>

.canvas-wrapper {
  min-height: 40vh;
}
</style>
