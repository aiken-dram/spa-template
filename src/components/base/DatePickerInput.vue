<template>
  <div>
    <v-text-field
      v-if="readonly"
      :value="val | date"
      :label="label"
      prepend-icon="fa-calendar-day"
      placeholder=" "
      persistent-placeholder
      readonly
    ></v-text-field>
    <v-text-field
      v-else
      v-model="input"
      :background-color="error ? 'red lighten-4' : 'white'"
      :error="error"
      :label="label"
      :solo="toolbar"
      :dense="toolbar"
      :single-line="toolbar"
      :hide-details="toolbar"
      :clearable="toolbar"
      v-facade="'##.##.####'"
      @blur="inputDate()"
      placeholder=" "
      persistent-placeholder
    >
      <template v-slot:append-outer>
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
            <v-icon v-bind="attrs" v-on="on">fa-calendar-day</v-icon>
          </template>
          <v-date-picker
            v-model="val"
            :locale="$t('locale')"
            first-day-of-week="1"
            v-bind="$attrs"
            no-title
            @input="showMenu = false"
          ></v-date-picker>
        </v-menu>
      </template>
    </v-text-field>
  </div>
</template>
<script>
function isValidDate(parts) {
  // First check for the pattern
  //if (!/^\d{1,2}\/\d{1,2}\/\d{4}$/.test(dateString)) return false;

  // Parse the date parts to integers
  //var parts = dateString.split(".");
  var day = parseInt(parts[0], 10);
  var month = parseInt(parts[1], 10);
  var year = parseInt(parts[2], 10);

  // Check the ranges of month and year
  if (year < 1000 || year > 3000 || month == 0 || month > 12) return false;

  var monthLength = [31, 28, 31, 30, 31, 30, 31, 31, 30, 31, 30, 31];

  // Adjust for leap years
  if (year % 400 == 0 || (year % 100 != 0 && year % 4 == 0))
    monthLength[1] = 29;

  // Check the range of the day
  return day > 0 && day <= monthLength[month - 1];
}

/**
 * Base date picker with text input field component
 * This only works with RU data format DD.MM.YYYY
 */
export default {
  name: "BaseDatePickerInput",

  /** attrs are passed to child component */
  inheritAttrs: false,

  props: {
    /** date picker value */
    value: String,

    /** date picker label */
    label: String,

    /** date picker is readonly */
    readonly: Boolean,

    /** date range picker is inside toolbar */
    toolbar: {
      type: Boolean,
      default: false,
    },
  },

  data: () => ({
    /** show menu with date select */
    showMenu: false,

    /** input date */
    input: null,

    /** date is not correct */
    error: false,
  }),

  methods: {
    formatDate(d) {
      return this.$options.filters.date(d);
    },

    toInt(x) {
      var parsed = parseInt(x);
      if (isNaN(parsed)) {
        return 0;
      }
      return parsed;
    },

    inputDate() {
      if (this.input) {
        var sd = this.input.split(".");
        if (sd.length == 3) {
          if (isValidDate(sd)) {
            var d = new Date(this.input);
            if (d instanceof Date && !isNaN(d.valueOf()))
              this.val = d.toISOString();
            else {
              this.error = true;
            }
          } else {
            this.error = true;
          }
        } else {
          this.error = true;
        }
      } else {
        //input is empty, set null to val
        this.val = null;
      }
    },
  },

  watch: {
    val: {
      handler() {
        this.input = this.formatDate(this.val);
      },
    },
  },

  mounted() {
    this.input = this.formatDate(this.val);
  },

  computed: {
    /** get and set value */
    val: {
      get() {
        return this.value;
      },
      set(value) {
        this.error = false;
        this.$emit("input", value);
      },
    },
  },
};
</script>
