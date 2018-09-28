<template>
  <form>
    <fieldset>
      <legend>Business details</legend>
      
      <div class="input-group fluid">
        <label for="startHour">Start hour</label>
        <input type="number" v-model="startHour" id="startHour" placeholder="Start hour">
      </div>
      
      <div class="input-group fluid">
        <label for="busyHour">Busiest hour</label>
        <input type="number" v-model="busyHour" id="busyHour" placeholder="username">
      </div>

      <div class="input-group fluid">
        <label for="endHour">End hour</label>
        <input type="number" v-model="endHour" id="endHour" placeholder="username">
      </div>

      <div class="input-group fluid">
        <label for="averageCustomers">Average number of customers</label>
        <input type="number" v-model="averageCustomers" id="averageCustomers" placeholder="Average Customers" step="0.5">
      </div>

      <div class="input-group fluid">
        <button type="button" v-on:click="onSubmit">Submit</button>
      </div>
    </fieldset>
  </form>
</template>

<script>

export default {
  name: 'UserForm',
  data() {
    return {
      startHour: 9,
      busyHour: 16,
      endHour: 17,
      averageCustomers: 10
    };
  },
  methods: {
    onSubmit (event) {
      var userInput = {
        "WorkStart": this.startHour,
        "WorkEnd": this.endHour,
        "BusiestHour": this.busyHour,
        "AverageCustomersPerDay": this.averageCustomers
      };
      this.$emit('submitted', userInput)
    }
  }
};

</script>