<template>
  <v-chip :color="getColor()" :text-color="getTextColor()" v-bind="$attrs">
    <slot></slot>
  </v-chip>
</template>

<script>
export default {
  name: "BaseChip",

  inheritAttrs: false,

  props: {
    /** chip type */
    type: String
  },

  methods: {
    getColor() {
      if (this.type) {
        if (this.type == "success") return "green";
        if (this.type == "info") return "primary";
        if (this.type == "warning") return "orange";
        if (this.type == "error") return "red";
      }
      return null;
    },
    getTextColor() {
      if (this.type) {
        return "white";
      }
      return null;
    }
  }
};
</script>
