<template>
  <v-text-field
    v-model="val"
    placeholder=" "
    persistent-placeholder
    v-bind="$attrs"
    v-on="$listeners"
  ></v-text-field>
</template>

<script>
/**
 * Base text field component
 */
export default {
  name: "BaseTextField",

  /** attrs are passed to child component */
  inheritAttrs: false,

  props: {
    /** Text field value */
    value: String,
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

<style></style>
