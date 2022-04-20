<template>
  <v-dialog
    :value="value"
    @input="update"
    scrollable
    :max-width="maxWidth"
    v-bind="$attrs"
  >
    <v-card>
      <v-toolbar dark flat color="primary">
        <v-toolbar-title>{{ title }}</v-toolbar-title>
        <slot name="toolbar"></slot>
      </v-toolbar>

      <slot name="top"></slot>
      <v-divider></v-divider>

      <v-card-text style="height: 100%">
        <slot></slot>
      </v-card-text>

      <v-divider></v-divider>

      <v-card-actions>
        <v-spacer></v-spacer>
        <slot name="buttons"></slot>
      </v-card-actions>
    </v-card>
  </v-dialog>
</template>

<script>
/**
 * Base dialog component
 */
export default {
  name: "BaseDialog",

  /** attrs are passed to child component */
  inheritAttrs: false,

  props: {
    /** show dialog */
    value: {
      type: Boolean,
      default: false,
    },

    /** dialog title */
    title: String,

    /** maximum width of dialog (default: 1000px) */
    maxWidth: {
      type: String,
      default: "1000px",
    },
  },

  methods: {
    /** update value */
    update(newValue) {
      this.$emit("input", newValue);
    },
  },
};
</script>
