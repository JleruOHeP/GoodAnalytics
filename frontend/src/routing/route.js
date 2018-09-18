import Home from '../components/home/Home.vue';
import NotFound from '../components/NotFound.vue';
import CustomerSpread from '../components/customerSpread/CustomerSpread.vue';

const routes = {
    '': Home,
    'chart': CustomerSpread
  };

const routing = {
    observers: [],
    subscribe: function(handler){
        this.observers.push(handler);
    },
    currentComponent: Home,
    setCurrentRoute: function(route) {
        this.currentComponent = routes[route] || NotFound;
        this.observers.forEach(h => h(route));
    }
};

routing.setCurrentRoute(window.location.hash.substring(1));
    
export default routing;
