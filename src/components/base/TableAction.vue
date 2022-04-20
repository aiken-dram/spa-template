<template>
  <v-tooltip bottom max-width="500">
    <template v-slot:activator="{ on, attrs }">
      <v-icon
        small
        class="mr-2"
        @click="$emit('click')"
        v-bind="attrs"
        v-on="on"
      >
        {{ icon }}
      </v-icon>
    </template>
    <span v-html="tooltip"></span>
  </v-tooltip>
</template>

<script>
export default {
  name: "BaseTableAction",

  props: {
    icon: String,
    tooltip: String,
  },
};
</script>

<style></style>
