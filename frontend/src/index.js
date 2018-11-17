import Vue from 'vue';
import routing from './routing/route';

const app = new Vue({
    el: '#app',
    data: {      
      currentComponent: routing.currentComponent
    },
    render (h) { return h(this.currentComponent) }
});

routing.subscribe(() => app.currentComponent = routing.currentComponent);