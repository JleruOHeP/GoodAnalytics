<template>
    <section class="hero">
      
      <div class="container">
          <div class="hero-inner"> 
            <div class="background-fill hero-content">
              <div class="row">
                <div class="col-sm-4 col-sm-offset-4">
                  <img src="https://s3-ap-southeast-2.amazonaws.com/good-analytics/assets/1.png" />
                </div>
              </div>

              <div class="row">
                <div class="col-sm-4 col-sm-offset-4">
                  <p class="lead"> Explore. Analyse. Implement.</p>
                </div>
              </div>

              <div class="row">
                <div class="col-sm-4 col-sm-offset-4">
                  <a href="#subscribe-section" class="button primary">I want to know more!</a>
                </div>
              </div>
            </div>
          </div>
      </div>

      <div class="container">
          <svg class="background-fill" xmlns="http://www.w3.org/2000/svg" xmlns:xlink="http://www.w3.org/1999/xlink" 
               x="0px" y="0px" viewBox="0 0 1920 140" xml:space="preserve" 
               style="margin-bottom: -9px; enable-background:new 0 0 1920 140;">
              <path class="svg-white" d="M960,92.9C811.4,93.3,662.8,89.4,515.3,79c-138.6-9.8-277.1-26.2-409-53.3C97.8,24,0,6.5,0,0c0,0,0,140,0,140
                    l960-1.2l960,1.2c0,0,0-140,0-140c0,2.7-42.1,11.3-45.8,12.2c-45.1,11-91.5,20.1-138.4,28.1c-176.2,30.1-359.9,43.8-542.9,48.9
                    C1115.4,91.4,1037.7,92.7,960,92.9z"></path>
          </svg>
      </div>
    </section>
</template>

<script>
export default {
  name: 'HomeHero'
};
</script>

<style lang="scss" scoped>
  .background-fill {
    background-color: rgba(93, 51, 251, 0.5);
    background-image: -webkit-linear-gradient(0deg, rgba(93, 51, 251, 0.75), rgba(78, 239, 237, 0.75));
    background-image: linear-gradient(90deg, rgba(93, 51, 251, 0.75), rgba(78, 239, 237, 0.75));
  }

  .svg-white {
    fill: var(--header-back-color);
  }
  
  .hero-inner {
    display: table;
    width: 100%;
    height: 100%;
    position: relative;
    z-index: 4;
  }

  .hero-inner .hero-content {
    padding-top: 3.428rem;
    display: table-cell;
    width: 100%;
    height: 100%;
    vertical-align: middle;
    text-align: center !important;
  }

  .hero .lead {
    margin-bottom: 2rem;
    opacity: .75;
    line-height: 1.5;
    font-weight: 300;
    font-size: 1.25rem;
  }
</style>
