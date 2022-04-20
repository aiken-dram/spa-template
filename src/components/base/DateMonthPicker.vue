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
        v-model="val"
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
    </template>
    <v-date-picker
      v-model="val"
      type="month"
      :locale="$t('locale')"
      no-title
      scrollable
    >
      <v-spacer></v-spacer>
      <v-btn text color="primary" @click="onOk">
        {{ $t("common.ok") }}
      </v-btn>
    </v-date-picker>
  </v-menu>
</template>

<script>
/**
 * Base month picker component
 */
export default {
  name: "BaseDateMonthPicker",

  /** attrs are passed to child component */
  inheritAttrs: false,

  props: {
    /** date month picker value */
    value: String,

    /** date month picker label */
    label: String,

    /** date month picker is inside toolbar */
    toolbar: {
      type: Boolean,
      default: false,
    },
  },

  data: () => ({
    showMenu: false,
  }),

  methods: {
    /** confirm date month selection */
    onOk() {
      this.showMenu = false;
    },

    /** clear date month selection */
    onClear() {
      //clear clicked
      this.$emit("input", null);
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
  },
};
</script>
