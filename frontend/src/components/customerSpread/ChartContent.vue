
<script>
import { Bar } from 'vue-chartjs'

export default {
  name: 'ChartContent',
  props:{
    data: Array,
    labels: Array
  },
  extends: Bar,
  mounted () {
    // Overwriting base render method with actual data.
    this.renderChart({
      labels: this.labels,
      datasets: [
        {
          label: this.name,
          backgroundColor: '#f87979',
          data: this.data
        }
      ]
    })
  }
};

</script>