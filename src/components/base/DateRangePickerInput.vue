<template>
  <v-row no-gutters>
    <v-col>
      <date-picker-input v-model="dateFrom" toolbar></date-picker-input>
    </v-col>
    <v-col>
      <date-picker-input v-model="dateTo" toolbar></date-picker-input>
    </v-col>
  </v-row>
</template>

<script>
import DatePickerInput from "./DatePickerInput";

/**
 * Base date range picker with text input field component
 * !!!NOT FINISHED!!!
 */
export default {
  name: "BaseDateRangePickerInput",

  /** attrs are passed to child component */
  inheritAttrs: false,

  props: {
    /** date range picker value */
    value: Array,

    /** date range picker label */
    label: String,

    /** date range picker is inside toolbar */
    toolbar: {
      type: Boolean,
      default: false,
    },
  },

  data: () => ({
    dFrom: null,
    dTo: null,
  }),

  computed: {
    /** get and set value */
    val: {
      get() {
        return this.value;
      },
      set(value) {
        this.$emit("input", value);
      },
    },
  },

  components: {
    DatePickerInput,
  },
};
</script>
