<template>
  <v-dialog
    v-model="dialog"
    :max-width="options.width"
    :style="{ zIndex: options.zIndex }"
    @keydown.esc="cancel"
  >
    <v-card>
      <v-toolbar dark :color="options.color" dense flat>
        <v-toolbar-title class="white--text">{{ t.title }}</v-toolbar-title>
      </v-toolbar>
      <v-card-text v-show="!!error" class="pa-4">
        {{ error.Error ? error.Error : error }}
      </v-card-text>
      <v-card-actions class="pt-0">
        <v-spacer></v-spacer>
        <v-btn color="grey" text @click.native="cancel"> {{ t.ok }} </v-btn>
      </v-card-actions>
    </v-card>
  </v-dialog>
</template>

<script>
/**
 * Vuetify Error Dialog component
 *
 * Insert component where you want to use it:
 * <error ref="error"></error>
 *
 * Call it:
 * this.$refs.error.open('Error title', 'error message', { color: 'red' });
 *
 * Alternatively you can place it in main App component and access it globally via this.$root.$error
 * <template>
 *   <v-app>
 *     ...
 *     <error ref="error"></error>
 *   </v-app>
 * </template>
 *
 * mounted() {
 *   this.$root.$error = this.$refs.error.open
 * }
 */
export default {
  name: "BaseDialogError",

  data: () => ({
    dialog: false,
    error: {
      Error: null,
    },
    options: {
      color: "error",
      width: 290,
      zIndex: 200,
    },
  }),
  methods: {
    open(error, options) {
      this.error = error;
      this.options = Object.assign(this.options, options);
      this.dialog = true;
    },
    cancel() {
      this.dialog = false;
    },
  },
  computed: {
    /** not sure why $t doesnt register here */
    t() {
      return {
        ok: this.$i18n.t("common.ok"),
        title: this.$i18n.t("common.error"),
      };
    },
  },
};
</script>
