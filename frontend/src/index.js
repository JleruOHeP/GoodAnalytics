import Vue from 'vue';
import 'mini.css';

import Home from './components/Home.vue'
import NotFound from './components/NotFound.vue'

  const routes = {
    '/': Home
  }

  new Vue({
    el: '#app',
    data: {
      currentRoute: window.location.pathname
    },
    computed: {
      ViewComponent () {
        return routes[this.currentRoute] || NotFound
      }
    },
    render (h) { return h(this.ViewComponent) }
  })