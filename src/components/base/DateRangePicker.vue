<template>
  <v-menu
    v-model="showMenu"
    :close-on-content-click="false"
    transition="scale-transition"
    offset-y
    offset-x
    max-width="290px"
    min-width="290px"
  >
    <template v-slot:activator="{ on, attrs }">
      <v-text-field
        v-if="toolbar"
        :value="dateRangeText"
        :label="label"
        prepend-icon="fa-calendar-day"
        solo
        dense
        single-line
        hide-details
        clearable
        @click:clear="onClear"
        readonly
        style="width: 300px"
        v-bind="attrs"
        v-on="on"
      >
        <!-- Pass on all named slots -->
        <slot v-for="slot in Object.keys($slots)" :name="slot" :slot="slot" />
      </v-text-field>
      <v-text-field
        v-else
        :value="dateRangeText"
        :label="label"
        prepend-icon="fa-calendar-day"
        placeholder=" "
        persistent-placeholder
        readonly
        v-bind="attrs"
        v-on="on"
      ></v-text-field>
    </template>
    <v-date-picker
      v-model="val"
      :locale="$t('locale')"
      first-day-of-week="1"
      range
      no-title
      v-bind="$attrs"
    >
      <v-spacer></v-spacer>
      <v-btn text color="primary" @click="onOk">OK</v-btn>
    </v-date-picker>
  </v-menu>
</template>
<script>
/**
 * Base date range picker component
 */
export default {
  name: "BaseDateRangePicker",

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
    /** show date range picker menu */
    showMenu: false,
  }),

  methods: {
    /** confirm date range selection */
    onOk() {
      this.showMenu = false;
      this.$emit("on-change");
    },

    /** clear date range selection */
    onClear() {
      //clear clicked
      this.$emit("input", []);
      this.$emit("on-change");
    },
  },

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

    /** display text of selected date range */
    dateRangeText() {
      return this.value.map((p) => this.$options.filters.date(p)).join(" ~ ");
    },
  },
};
</script>
