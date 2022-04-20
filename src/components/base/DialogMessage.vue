<template>
  <v-snackbar v-model="snack" :timeout="3000" :color="snackColor">
    {{ snackText }}

    <template v-if="showButton" v-slot:action="{ attrs }">
      <v-btn v-bind="attrs" text @click="snack = false">
        {{ $t("common.ok") }}
      </v-btn>
    </template>
  </v-snackbar>
</template>

<script>
/**
 * Base dialog message component
 */
export default {
  name: "BaseDialogMessage",

  data: () => ({
    snack: false,
    snackColor: "info",
    snackText: null,
    showButton: true,
  }),

  methods: {
    open(message, color, disableButton = false) {
      this.snackText = message;
      this.snackColor = color;
      this.snack = true;
      if (disableButton) this.showButton = false;
    },
  },
};
</script>
