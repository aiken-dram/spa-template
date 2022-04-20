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
        :label="label"
        :value="val | date"
        prepend-icon="fa-calendar-day"
        solo
        dense
        single-line
        hide-details
        readonly
        style="width: 300px"
        v-bind="attrs"
        v-on="on"
      ></v-text-field>
      <v-text-field
        v-else-if="!readonly"
        :value="val | date"
        :label="label"
        prepend-icon="fa-calendar-day"
        placeholder=" "
        persistent-placeholder
        readonly
        v-bind="attrs"
        v-on="on"
      ></v-text-field>
      <v-text-field
        v-else
        :value="val | date"
        :label="label"
        prepend-icon="fa-calendar-day"
        placeholder=" "
        persistent-placeholder
        readonly
      ></v-text-field>
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
<script>
/**
 * Base date picker component
 */
export default {
  name: "BaseDatePicker",

  /** attrs are passed to child component */
  inheritAttrs: false,

  props: {
    /** date picker value */
    value: String,

    /** date picker label */
    label: String,

    /** date picker is readonly */
    readonly: Boolean,

    /** date picker is inside toolbar */
    toolbar: {
      type: Boolean,
      default: false,
    },
  },

  data: () => ({
    /** show menu with date select */
    showMenu: false,
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
};
</script>
