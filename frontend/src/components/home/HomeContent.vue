<template>
  <div>

    <div class="container">

      <div class="row">
        <h3 class="col-sm-offset-3">Hello!</h3>
      </div>

      <div class="row">
        <div class="col-sm-9 col-sm-offset-3">
          <p>First of all - <b>thank you</b> very much for visiting this site!</p>
          <p>This is going to be a web-app that will help small businesses with analytics.</p>
          <p>In most cases there is a certain level of uncertainty when you as a business owner need to make a decision.</p>
          <p>
            This site is going to help to accumulate some statistics about your business and based on it 
            develop a model that will give a better answer to a question - "Why".
          </p>
          <p>At the moment this app is still in development.</p>
          <p>Last update - 4th of September 2019.</p>
        </div>
      </div>

    </div>

    <hr>

    <div class="container">
      <div class="row">
        <div class="col-sm-3 col-sm-offset-3">            
            <p>A couple of examples where this service can be helpful:</p>
        </div>
      </div>
      <div class="row">
        <div class="col-sm-3 col-sm-offset-3">
            <h5>Example 1</h5>
            <p>To run advertisement compaing or not? What is the expected outcome, price, timeframes?</p>
        </div>
        <div class="col-sm-3">
            <img src="https://s3-ap-southeast-2.amazonaws.com/good-analytics/assets/ad-campaign.jpg" />
        </div>
      </div>
    </div>

    <hr>

    <div class="container">
      <div class="row">
        <div class="col-sm-3 col-sm-offset-3">
            <img src="https://s3-ap-southeast-2.amazonaws.com/good-analytics/assets/fish-chips.JPG" />
        </div>
        <div class="col-sm-3">
          <h5>Example 2</h5>
          <p>As a fish and chips shop - if you would update your storefront with a zomato reference, will it help to drive more customers?</p>
        </div>
      </div>
    </div>

    <hr>

  </div>
</template>

<script>
export default {
  name: 'HomeContent'
};
</script>

<style lang="scss" scoped>
  hr { 
    border: 0; 
    height: 1px; 
    background-image: linear-gradient(to right, rgba(0, 0, 0, 0), rgba(0, 0, 0, 0.75), rgba(0, 0, 0, 0)); 
  }
</style>