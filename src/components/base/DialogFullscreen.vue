<template>
  <v-dialog
    :value="value"
    @input="update"
    fullscreen
    transition="dialog-bottom-transition"
  >
    <v-card>
      <v-toolbar dark flat color="primary">
        <v-btn v-if="closeButton" dark icon @click.stop="close">
          <v-icon>fa-times-circle</v-icon>
        </v-btn>
        <v-toolbar-title>{{ title }}</v-toolbar-title>
        <v-spacer></v-spacer>
        <v-toolbar-items>
          <slot name="buttons"></slot>
        </v-toolbar-items>

        <template v-slot:extension v-if="extended">
          <slot name="extension"></slot>
        </template>
      </v-toolbar>

      <v-card-text>
        <slot></slot>
      </v-card-text>
    </v-card>
  </v-dialog>
</template>

<script>
/**
 * Base fullscreen dialog component
 */
export default {
  name: "BaseDialogFullscreen",

  props: {
    /** show dialog */
    value: {
      type: Boolean,
      default: false,
    },

    /** show close button left from title (default: true) */
    closeButton: {
      type: Boolean,
      default: true,
    },

    /** title of dialog */
    title: String,

    /** extended dialog (defailt: true) */
    extended: {
      type: Boolean,
      default: true,
    },
  },

  methods: {
    /** close dialog */
    close() {
      this.$emit("close");
    },

    /** update value */
    update(newValue) {
      this.$emit("input", newValue);
    },
  },
};
</script>
