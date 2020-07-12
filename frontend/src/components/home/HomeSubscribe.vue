<template>  
    <div class="container" id="subscribe-section">
      
      <div v-if="!successfulSubmission">
      <div class="row" >
        <div class="col-sm-8 col-sm-offset-2" >
          If you want to know more or to get updates about the app, please subscribe:
        </div>
      </div>
      <div class="row" >
        <div class="col-sm-4 col-sm-offset-2">
          <input type="email" v-model="mail" placeholder="Add email for subscription" style="width:100%;height:100%;"/> 
        </div>
        <div class="col-sm-4">
          <button v-on:click="addEmail">Save!</button>
        </div>
      </div>
      </div>
      
      <div v-if="successfulSubmission">
      <div class="row" >
        <div class="col-sm-8 col-sm-offset-2" >
          Thank you for the subscription! We will be in touch shortly.
        </div>
      </div>
      </div>
      
    </div>    
</template>

<script>
const axios = require('axios');

export default {
  name: 'HomeSubscribe',
  data() {
    return {
      mail: "",
      successfulSubmission: false
    };
  },
  methods: {
      addEmail : function(){
        if (validateEmail(this.mail)) {
            this.successfulSubmission = postToApi(this.mail);
        }
        else {
            alert('Bad email address');
        }        
      }
  }
};

function validateEmail(email) {
    var re = /^(([^<>()\[\]\\.,;:\s@"]+(\.[^<>()\[\]\\.,;:\s@"]+)*)|(".+"))@((\[[0-9]{1,3}\.[0-9]{1,3}\.[0-9]{1,3}\.[0-9]{1,3}\])|(([a-zA-Z\-0-9]+\.)+[a-zA-Z]{2,}))$/;
    return re.test(String(email).toLowerCase());
};

function postToApi(email) {
    var baseUrl = 'https://yobcnyc7ja.execute-api.ap-southeast-2.amazonaws.com/default/expressionOfInterest';
    var query = baseUrl + '?mail=' + email;

    var success = true;
    axios.post(query)
        .then(function (response) {            
        })
        .catch(function (error) {
            console.log(error);
            success = false;
        });

    return success;
}
</script>