<template>  
  <div>
    <shared-header />

    <div class="container">

      <div class="row">
        <h3 class="col-sm-offset-2">Customers distribution example</h3>
      </div>

      <div class="row">
        <div class="col-sm-8 col-sm-offset-2">
          <p>
            Business models can be very simple. As simple as a binary decision - to be or not to be. Or if you want to put it into another perspective - will some new customer enter your shop or not?
          </p>

          <p>
            Answer to that question can be important if you want to expand your business and hire an additional cashier. But you are not sure if it worth it or not... 
          </p>

          <p>
            If you could model how busy is your shop, could know if people are passing by because they do not want to wait in the queue it would be so much easier to maybe get someone on a part time only? Especially if your mornings are a dead season.
          </p>
        </div>
      </div>

    </div>

    <div class="container" v-if="!calculated">
      <div class="row">
        <div class="col-sm-8 col-sm-offset-2">
          <p>
             Lets say that you have some statistical data about your customers:
          </p>
        </div>
      </div>
      <div class="row">
        <div class="col-sm-8 col-sm-offset-2">
          <user-form @submitted="onFormSubmitted"/>
        </div>
      </div>
    </div>

    <div class="container" v-if="calculated">
      <div class="row">
        <div class="col-sm-8 col-sm-offset-2">
          <chart-content :data=calculationResult :labels=labels />
        </div>
      </div>
      <div class="row">
        <div class="col-sm-8 col-sm-offset-2">
          <p>
            With this graph you can easier see if there are any potential queues that might turn away new customers or the opposite and your existing service is throttling.
          </p>
          <p>
            <button type="button" @click="resetForm">Reset</button>
          </p>
        </div>
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
      this.labels = [];
      for (var i = value.WorkStart; i < value.WorkEnd; i += 0.5)
      {
        this.labels.push(i);
      }

      var that = this;
      postToApi(value)
        .then(function(result) {
          that.calculationResult = result;
          that.calculated = true;
      });
    },
    resetForm (){
      this.calculated = false;
    }
  },
  components: {
    SharedHeader,
    ChartContent,
    UserForm
  }
};

function postToApi(formData) {
    var url = 'https://api.good-analytics.org/spreadCalculator';

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