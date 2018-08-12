<template>  
    <div class="container">      
      <div class="row">
        <h3>Hello!</h3>
      </div>
      <div class="row">
        <div class="col-sm-12">
          <p>This is going to be a web-app that will help small businesses with analytics.</p>
          <p>In most cases there is a certain level of uncertainty when you as a business owner need to make a decision.</p>
          <p>
            This site is going to help to accumulate some statistics about your business and based on it 
            develop a model that will give a better answer to a question - "Why".
          </p>
          <p>
            At the moment this app is still in development.
          </p>
        </div>
      </div>
      <div class="row" id="subscribe-section">
        <div class="col-sm-12">
            <div v-if="!successfulSubmission">
                <p>If you want to know more or to keep  updated about the progress, please subscribe:</p>
                <input type="email" v-model="mail" placeholder="Add email for subscription"/> <button v-on:click="addEmail">Save!</button>
            </div>    
            <div v-if="successfulSubmission">
                <p>Thank you for the subscription! We will be in touch shortly.</p>
            </div>
        </div>
      </div>
    
    </div>    
</template>

<script>
const axios = require('axios');

export default {
  name: 'HomeContent',
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
            alert('nope');
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