<template>  
  <div>
    <shared-header />

    <drawing-component @submitted="onModelSubmitted"  v-if="!calculated"/>

    <div class="container" v-if="calculated">
      <div class="row">
        <h2 class="col-sm-offset-2">Simulation results:</h2>
      </div>

      <div class="row">
        <div class="col-sm-8 col-sm-offset-2">
          <ul>
            <li v-for="(value, name) in calculationResult">
              {{name}} : {{ value }}
            </li>
          </ul>
          <button type="button" class="primary" @click="resetForm">Reset</button>
        </div>
      </div>
    </div>
  </div>
</template>

<script>
import SharedHeader from '../shared/SharedHeader.vue'
import DrawingComponent from './DrawingComponent.vue'

const axios = require('axios');

export default {
  name: 'DiagramDesigner',
  data() {
    return {
      calculated: false,
      calculationResult: []
    };
  },
  methods: {
    onModelSubmitted (value) {
      var that = this;
      postToApi(value)
        .then(function(result) {
          that.calculationResult = result.variables;
          that.calculated = true;
      });
    },
    resetForm(){
      this.calculated = false;
    }
  },
  components: {
    SharedHeader,
    DrawingComponent
  }
};
  
function postToApi(processModel) {
    var url = 'https://api.good-analytics.org/projectcalculator';

    return axios.post(
          url,
          processModel
        )
        .then(function (response) {
          return response.data;
        })
        .catch(function (error) {
            console.log(error);
            return [];
        });
}

</script>