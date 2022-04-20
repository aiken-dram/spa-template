<template>
  <v-list-item two-line>
    <v-list-item-content>
      <v-list-item-title>{{ flag.title }}</v-list-item-title>
      <v-btn-toggle v-model="toggle" multiple>
        <v-tooltip v-for="(g, i) in group" :key="i" bottom>
          <template v-slot:activator="{ on, attrs }">
            <v-btn v-bind="attrs" v-on="on">
              <v-icon :color="g.color">{{ g.icon }}</v-icon>
            </v-btn>
          </template>
          <span v-text="$t(g.tooltip)"></span>
        </v-tooltip>
      </v-btn-toggle>
    </v-list-item-content>
  </v-list-item>
</template>

<script>
import FLAGS from "@/common/flags";

/**
 * Flag from FLAGS
 */
export default {
  name: "BaseTableFilterFlag",

  props: {
    /** value */
    value: Array,

    /** flag from filter flags array */
    flag: Object,
  },

  data: () => ({}),

  methods: {},

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
    /** group from config */
    group() {
      return FLAGS[this.flag.name];
    },
  },

  beforeCreate() {
    this.FLAGS = FLAGS;
  },
};
</script>
