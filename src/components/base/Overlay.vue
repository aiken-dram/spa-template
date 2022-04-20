<template>
  <v-overlay :value="value" v-bind="$attrs" :absolute="true">
    <v-progress-circular indeterminate :size="size"></v-progress-circular>
  </v-overlay>
</template>

<script>
/**
 * Base overlay component
 */
export default {
  name: "BaseOverlay",

  inheritAttrs: false,

  props: {
    /** overlay value */
    value: {
      type: Boolean,
      default: false,
    },

    /** size of loading circle, default 64 */
    size: {
      type: Number,
      default: 64,
    },
  },
};
</script>
