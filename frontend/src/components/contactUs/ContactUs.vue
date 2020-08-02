<template>  
  <div>
    <shared-header />

    <div class="container">

      <div class="row">
        <h3 class="col-sm-offset-2">Get in touch</h3>
      </div>

      <div class="row">
        <div class="col-sm-8 col-sm-offset-2">
          <p>Want to get in touch? I'd love to hear from you. You can reach me using the form below, or if you prefer just a straight email - send it to <a href="mailto:goodanalyticsau@gmail.com">goodanalyticsau@gmail.com</a> (sorry for @google email...)</p>
        </div>
      </div>

      <div class="row" v-if="!submitted">
        <div class="col-sm-8 col-sm-offset-2">
          <form>
            <fieldset>
              <div class="input-group fluid">
                <label for="startHour">Name</label>
                <input type="text" v-model="name" placeholder="What is your name?">
              </div>
              
              <div class="input-group fluid">
                <label for="busyHour">Email</label>
                <input type="email" v-model="email" placeholder="What is your email">
              </div>
              
              <div class="input-group fluid">
                <label for="busyHour">Message</label>
                <textarea v-model="message" cols="10" rows="5" style="width:100%" placeholder="What do you want to say" />
              </div>

              <div class="input-group fluid">
                <button type="button" class="primary" v-on:click="onSubmit">Submit</button>
              </div>
            </fieldset>
          </form>
        </div>
      </div>

      <div class="row" v-if="submitted">
        <div class="col-sm-8 col-sm-offset-2">
          <p>
            Thank you for the message! I will be in touch shortly.
          </p>
        </div>
      </div>

    </div>
  </div>
</template>

<script>
import SharedHeader from '../shared/SharedHeader.vue'

const axios = require('axios');

export default {
  name: 'ContactUs',
  data() {
    return {
      submitted: false,
      name: '',
      email: '',
      message: ''
    };
  },
  methods: {
    onSubmit () {
      var that = this;
      if (!this.validateEmail(this.email))
        return;
      
      grecaptcha.ready(function() {
          grecaptcha.execute('6Lc_nbEZAAAAAEHT7sfnNUifR8lNv0puQSKbG_sc', {action: 'submit'})
             .then(function(token) {
                let userMessage = {
                  Name: that.name,
                  Email: that.email,
                  Message: that.message,
                  CaptchaToken: token
                };
                that.submitted = postToApi(userMessage);
             });
      }); 
    },
    validateEmail(email) {
      var re = /^(([^<>()\[\]\\.,;:\s@"]+(\.[^<>()\[\]\\.,;:\s@"]+)*)|(".+"))@((\[[0-9]{1,3}\.[0-9]{1,3}\.[0-9]{1,3}\.[0-9]{1,3}\])|(([a-zA-Z\-0-9]+\.)+[a-zA-Z]{2,}))$/;
      return re.test(String(email).toLowerCase());
    },
    resetForm (){
      this.submitted = false;
    }
  },
  components: {
    SharedHeader
  }
};

function postToApi(userMessage) {
    var url = 'https://api.good-analytics.org/contactushandler';

    var success = true;
    axios.post(
          url,
          userMessage
        )
        .then(function (response) {            
        })
        .catch(function (error) {
            console.log(error);
            success = false;
        });

    return success;
}
</script>