<template>  
  <div>
    <shared-header />

    <div class="container">      
      <div class="row">
        <user-form v-if="!calculated" @submitted="onFormSubmitted"/>
        <chart-content v-if="calculated" :data=calculationResult :labels=labels />
      </div>      
    </div>
  </div>
</template>

<script>
import SharedHeader from '../shared/SharedHeader.vue'
import ChartContent from './ChartContent.vue'
import UserForm from './UserForm.vue'

const axios = require('axios');

export default {
  name: 'CustomerSpread',
  data() {
    return {
      calculated: false,
      calculationResult: [],
      labels: []
    };
  },
  methods: {
    onFormSubmitted (value) {
      var that = this;
      for (var i = value.WorkStart; i < value.WorkEnd; i += 0.5)
      {
        this.labels.push(i);
      }
      postToApi(value)
        .then(function(result) {
          that.calculationResult = result;
          that.calculated = true;
      });
    }
  },
  components: {
    SharedHeader,
    ChartContent,
    UserForm
  }
};

function postToApi(formData) {
    var url = 'https://frss2w6fjk.execute-api.ap-southeast-2.amazonaws.com/prod/spreadCalculator';

    return axios.post(
          url,
          formData
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