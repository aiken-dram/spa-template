<template>
  <v-list-item two-line>
    <v-list-item-content>
      <v-list-item-title>{{ flag.title }}</v-list-item-title>
      <v-btn-toggle v-model="toggle" multiple>
        <v-tooltip v-for="(g, i) in flag.group" :key="i" bottom>
          <template v-slot:activator="{ on, attrs }">
            <v-btn v-bind="attrs" v-on="on">
              <v-icon :color="g.color"> {{ g.icon }}</v-icon>
            </v-btn>
          </template>
          <span v-text="g.title"></span>
        </v-tooltip>
      </v-btn-toggle>
    </v-list-item-content>
  </v-list-item>
</template>

<script>
/**
 * Base api table filter flag group component
 */
export default {
  name: "BaseTableFilterFlagGroup",

  props: {
    /** value */
    value: Array,

    /** flag from filter flags array */
    flag: Object,
  },

  computed: {
    /** get and set toggle for flag */
    toggle: {
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
