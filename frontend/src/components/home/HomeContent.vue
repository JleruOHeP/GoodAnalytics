<template>
  <div>

    <div class="container">

      <div class="row">
        <h3 class="col-sm-offset-2">Hello!</h3>
      </div>

      <div class="row">
        <div class="col-sm-8 col-sm-offset-2">
          <p>First of all - <b>thank you</b> very much for visiting this site!</p>
          
          <p>
            Business analysis from the mathematical point of view always fascinated me. I like to think that a properly defined model is needed for any business. And if you can throw statistical data into this model - it will do wonders!
          </p>

          <p>
            You are a small business owner and you have a model in mind. But there are a number of variables that can affect the performance of the business. It is really hard to hold everything in your head. I want to help you to run simulations over various input parameters to find the optimal setup for your business. Combine internal properties of processes so the profit is maximized.
          </p>

          <p>
            You can save the historical performance of the business and compare the current state with the desired optimal one. And find the best way to correct the actual model towards this optimized model!
          </p>

          <p>
            Whenever you need to make an important business decision there is a certain level of uncertainty. Now you can make decisions based on data instead of hunches.
          </p>
        </div>
      </div>

    </div>

    <hr>

    <div class="container">
      <div class="row">
        <div class="col-sm-8 col-sm-offset-2">            
            <h4>A couple of examples where this service can be helpful:</h4>
        </div>
      </div>
      <div class="row">
        <div class="col-sm-4 col-sm-offset-2">
            <p>
              You have an efficient IT consultancy company. Will you hire a new salesperson or a new developer? It is a question of a fine balance between available work and potential output.
            </p>

            <p>
              As long as the marginal benefit of adding another salesperson exceeds the cost, you should expect the company to keep adding salespeople, which can often result in a sales force that's larger than the engineering team. But when to stop?
            </p>

            <p>
              It will be much better to be able to precisely predict contract growth based on the size of the sales team and compare it to what you can cover with your engineering department.
            </p>
        </div>
        <div class="col-sm-4">
            <img src="https://s3-ap-southeast-2.amazonaws.com/good-analytics/assets/new-hire.jpg" />
            <p class="photo-sub-text">Photo by Andrea Piacquadio from Pexels</p>
        </div>
      </div>
    </div>

    <hr>

    <div class="container">
      <div class="row">
        <div class="col-sm-4 col-sm-offset-2">
            <img src="https://s3-ap-southeast-2.amazonaws.com/good-analytics/assets/fish-chips-2.jpg" />
            <p class="photo-sub-text">Photo by Adrien Olichon from Pexels</p>
        </div>
        <div class="col-sm-4">
          <p>
            You have a nice and cozy fish and chips shop and feel that it is time to grow. Will you run an advertisement campaign or not? What is the expected outcome, price, timeframes of this campaign?
          </p>

          <p>
            Maybe it is better to update your storefront with a zomato reference? Will it help to drive more customers?
          </p>

          <p>
            When you have enough data available there is a very high chance that statistical modeling will give you exact answers to all of those questions. It is much easier to compare apples to apples rather than trying to estimate the outcome using only emotions.
          </p>
        </div>
      </div>
    </div>

    <hr>

    <div class="container">
      <div class="row">
        <div class="col-sm-8 col-sm-offset-2">
            <p>
              This site is going to help to accumulate some statistics about your business and based on it develop a model that will give a better answer to a question - "Why".
            </p>
            
            <p>At the moment this app is still in development. Using top navigation menu you can find a couple of interactive examples.</p>
            <p>Last update - 12 of June 2020.</p>
        </div>
      </div>
    </div>

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
  .photo-sub-text {
    font-size: 10px;
    text-align: center;
    margin-top: 0;
  }
</style>