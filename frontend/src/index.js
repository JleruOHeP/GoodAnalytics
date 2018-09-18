import Vue from 'vue';
import routing from './routing/route';
// import 'mini.css';
import './styles/flavors/mini-nord.scss';

const app = new Vue({
    el: '#app',
    data: {      
      currentComponent: routing.currentComponent
    },
    render (h) { return h(this.currentComponent) }
});

routing.subscribe(() => app.currentComponent = routing.currentComponent);