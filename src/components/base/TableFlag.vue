<template>
  <div>
    <v-tooltip v-if="value" bottom max-width="500">
      <template v-slot:activator="{ on, attrs }">
        <v-icon v-bind="attrs" v-on="on">
          {{ flag().icon }}
        </v-icon>
      </template>
      <span v-html="$t(flag().tooltip)"></span>
    </v-tooltip>
    <v-icon v-else class="icon-invisible">fa-exclamation-circle</v-icon>
  </div>
</template>

<script>
import FLAGS from "@/common/flags";

/**
 * Display flag in table
 */
export default {
  name: "BaseTableFlag",

  props: {
    value: String,
    flagName: String,
  },

  methods: {
    flag() {
      var flags = FLAGS[this.flagName];
      var f = flags.find((p) => p.value == this.value);
      var res = {
        icon: f.icon,
        tooltip: f.tooltip,
      };
      return res;
    },
  },
};
</script>

<style scoped>
.icon-invisible {
  visibility: hidden;
}
</style>
