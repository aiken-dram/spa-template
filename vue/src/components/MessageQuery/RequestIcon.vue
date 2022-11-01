<template>
  <v-tooltip bottom max-width="500">
    <template v-slot:activator="{ on, attrs }">
      <v-icon :color="icon.color" class="fix-spin" v-bind="attrs" v-on="on">
        {{ icon.fa }}
      </v-icon>
    </template>
    <span v-html="$t(`messagequery.request.state${value}`)" />
  </v-tooltip>
</template>

<script>
/** Icon for request in message query */
export default {
  name: "RequestIcon",
  props: {
    value: String,
  },

  computed: {
    icon() {
      switch (this.value) {
        case "QUEUE":
          return { color: "info", fa: "fa-hourglass-start" };
        case "PROCESSING":
          return { color: "success", fa: "fa-cog fa-spin" };
        case "READY":
          return { color: "amber", fa: "fa-envelope" };
        case "ERROR":
          return { color: "error", fa: "fa-exclamation-circle" };
        case "DELIVERED":
          return { color: "", fa: "fa-envelope" };
      }
      return { color: "", fa: "" };
    },
  },
};
</script>

<style>
.fix-spin {
  text-align: center;
  display: inline-flex !important;
  align-items: center;
  justify-content: center;
}
</style>
