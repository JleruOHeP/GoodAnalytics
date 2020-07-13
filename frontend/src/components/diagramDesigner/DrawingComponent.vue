<template>  

  <div class="container">
    <div class="row">
      <h2 class="col-sm-offset-2">Canvas:</h2>
    </div>
    <div class="row">
      <div class="col-sm-1 col-sm-offset-1">
          <button type="button" @click="calculateModel()">Calculate</button>
          <br/>
          <button type="button" @click="saveFile()">Save</button>
          <br/>
          <label class="button">
            Load <input type="file" style="display: none;" v-on:change="loadFile">
          </label>          
          <br/>
          <button type="button" @click="clearCanvas()">Clear</button>
      </div>
      <div class="col-sm-6" ref="canvas-wrapper">
          <canvas id="canvas">
          </canvas>
      </div>
      <div class="col-sm-3">
        <button type="button" @click="deleteShape()">Delete</button>
        <br />
        <div v-if="selectedObject !== null">
          <h2>Properties</h2>

          <div class="input-group" v-if="selectedObject.type !== 'lineArrow'">
            <label>Description:</label>
            <input type="text" v-model="selectedObject.label"/>
          </div>

          <div v-if="selectedObject.type === 'event'">
            <h3>Probability:</h3>
            <input type="number" min="0" max="100" v-model="selectedObject.probability"/>
          </div>

          <div v-if="selectedObject.type === 'process'">
            <h3>Actions:</h3>
            <div class="input-group" v-for="(item, index) in selectedObject.actions">
              <input type="text" v-model="item.text"/>
              <button type="button" class="small" @click="selectedObject.actions.splice(index, 1)">X</button>
            </div>
            <button type="button" @click="selectedObject.actions.push({text: ''})">Add new action</button>
            
          </div>

          
        </div>
      </div>
    </div>

    <div class="row">
      <div class="col-sm-6 col-sm-offset-2">
        <div class="button-group">
          <button type="button" v-bind:class="{ primary: currentShapeType === 0, tertiary: currentShapeType !== 0 }" @click="setShape(0)">Nothing</button>
          <button type="button" v-bind:class="{ primary: currentShapeType === 1, tertiary: currentShapeType !== 1 }" @click="setShape(1)">Draw event</button> 
          <button type="button" v-bind:class="{ primary: currentShapeType === 2, tertiary: currentShapeType !== 2 }" @click="setShape(2)">Draw Process</button> 
          <button type="button" v-bind:class="{ primary: currentShapeType === 3, tertiary: currentShapeType !== 3 }" @click="setShape(3)">Draw Line</button> 
        </div>
      </div>
    </div>
  </div>

</template>

<script>

import { initCustomClasses } from './FabricClasses.js' 
import { getObjectByCoordinates, getNextId,  getObjectById, calculateX, calculateY, deleteLineConnections, refreshConnectingLines, refreshAllLines, getProcessModel } from './CanvasHelper.js' 
import { EventDrawer } from './EventDrawer.js'
import { ProcessDrawer } from './ProcessDrawer.js'
import { LineDrawer } from './LineDrawer.js'

var ShapesEnum = {"None": 0, "Event": 1, "Process": 2, "Line": 3};
Object.freeze(ShapesEnum);

initCustomClasses();

export default {
  name: 'DrawingComponent',
  data() {
    return {
        canvas: null,
        currentShapeType: ShapesEnum.Event,
        currentShape: null,
        selectedObject: null,
        drawers: {}
    };
  },
  methods: {
      deleteShape(){        
        var selectedElement = this.canvas.getActiveObject();
        if (selectedElement) {
            if (selectedElement.type === "lineArrow") {
                deleteLineConnections(this.canvas, selectedElement.id);
            }

            selectedElement.remove();
        }

        var selectedGroup = this.canvas.getActiveGroup();
        if (selectedGroup) {

            selectedGroup.forEachObject(function (o) {
                if (o.type === "lineArrow") {
                    deleteLineConnections(vueApp.canvas, o.id);
                }
                
                o.remove();
            });
            this.canvas.discardActiveGroup();
        }

        activeObject.remove();
        this.canvas.renderAll();
      },

      canvasMouseDown: function(options) {
          var drawer = this.drawers[this.currentShapeType];

          if (drawer) {
            let y = calculateY(options.e.clientY, options.e.target);
            let x = calculateX(options.e.clientX, options.e.target);
            let nextId = getNextId(this.canvas);
            drawer.start(x, y, nextId, options.target);
          }
      },
      canvasMouseMove: function(options) {
          var drawer = this.drawers[this.currentShapeType];

          if (drawer) {
            var y = calculateY(options.e.clientY, options.e.target);
            var x = calculateX(options.e.clientX, options.e.target);

            drawer.continue(x, y);
          }
      },
      canvasMouseUp: function(options) {
          var drawer = this.drawers[this.currentShapeType];

          if (drawer) {
            let y = calculateY(options.e.clientY, options.e.target);
            let x = calculateX(options.e.clientX, options.e.target);

            drawer.finish(x, y, options.target);
            this.setShape(ShapesEnum.None);
          }
      },

      canvasObjectMoving: function(e) {
          refreshConnectingLines(this.canvas, e.target);

          this.canvas.renderAll();
      },

      setShape (shapeType){
          this.currentShapeType = shapeType;

          let draggable = shapeType === 0;
          this.canvas.selection = draggable;
          this.canvas.forEachObject(function (obj) {
              if (obj.type !== 'lineArrow') {
                  obj.lockMovementX = !draggable;
                  obj.lockMovementY = !draggable;
              }
          });
      },

      canvasObjectSelected(){
        this.selectedObject = this.canvas.getActiveObject();
      },
      canvasUnselect() {
        this.selectedObject = null;
      },

      processKeyboard (e) {
          if (46 === e.keyCode) {
            // 46 is Delete key
            deleteShape();
          }
      },

      calculateModel(){
        let model = getProcessModel(this.canvas);
        this.$emit('submitted', model);
      },

      clearCanvas(){
        this.canvas.clear();
        this.canvas.backgroundColor = "white";
        this.canvas.renderAll();
      },

      saveFile: function() {
          var message = JSON.stringify(this.canvas.toJSON());

          var file = new Blob([message], { type: 'text/plain' });
          var url = URL.createObjectURL(file);

          var a = document.createElement("a");
          a.href = url;
          a.download = 'whiteboard.json';
          document.body.appendChild(a);
          a.click();
          setTimeout(function () {
              document.body.removeChild(a);
              window.URL.revokeObjectURL(url);
          }, 0);
      },
      loadFile: function (e) {
          var reader = new FileReader();
          var component = this;
          reader.onload = function (f) {
              component.canvas.loadFromJSON(f.target.result);
              refreshAllLines(component.canvas);
              component.canvas.renderAll();
              component.setShape(ShapesEnum.None);
          };

          reader.readAsText(e.target.files[0]);
      },
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

      window.addEventListener('resize', resizeCanvas, false);

      var component = this;
      function resizeCanvas() {
          component.canvas.setHeight(500);
          component.canvas.setWidth(canvasWrapper.offsetWidth);
          component.canvas.renderAll();
      }

      // resize on init
      resizeCanvas();

      this.drawers = {
        [ShapesEnum.Line]: new LineDrawer(this.canvas),
        [ShapesEnum.Process]: new ProcessDrawer(this.canvas),
        [ShapesEnum.Event]: new EventDrawer(this.canvas)
      };

      this.canvas.calcOffset();
      this.canvas.on('mouse:down', this.canvasMouseDown);
      this.canvas.on('mouse:up', this.canvasMouseUp);
      this.canvas.on('mouse:move', this.canvasMouseMove);
      this.canvas.on('object:moving', this.canvasObjectMoving);
      this.canvas.on('object:selected', this.canvasObjectSelected);
      this.canvas.on('before:selection:cleared', this.canvasUnselect);

      this.canvas.renderAll();
  }
};

</script>

<style lang="scss" scoped>

.canvas-wrapper {
  min-height: 40vh;
}
</style>
