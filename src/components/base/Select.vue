<template>
  <v-select
    :value="value"
    @input="update"
    :items="citems"
    :loading="loading"
    :solo="toolbar"
    :dense="toolbar"
    :single-line="toolbar"
    :hide-details="toolbar"
    :placeholder="toolbar ? null : ' '"
    v-bind="$attrs"
  >
    <!-- Pass on all named slots -->
    <slot v-for="slot in Object.keys($slots)" :name="slot" :slot="slot" />

    <!-- Pass on all scoped slots -->
    <template
      v-for="slot in Object.keys($scopedSlots)"
      :slot="slot"
      slot-scope="scope"
      ><slot :name="slot" v-bind="scope"
    /></template>
  </v-select>
</template>

<script>
import { mapGetters } from "vuex";
import store from "@/store";
import { DICT_FETCH } from "@/store/actions.type";

/**
 * Base select component
 */
export default {
  name: "BaseSelect",

  /** attrs are passed to child component */
  inheritAttrs: false,

  props: {
    /** select value */
    value: [Number, String],

    /** (optinal) select dictionary name */
    dictionary: String,

    /** (optional) select items */
    items: Array,

    /** select is inside toolbar */
    toolbar: {
      type: Boolean,
      default: false,
    },
  },

  data: () => ({
    /** dictionary is being loaded from api*/
    loading: true,
  }),

  mounted() {
    if (this.dictionary) {
      //if dictionary is provided, fetch it from api
      store.dispatch(DICT_FETCH, this.dictionary).then(() => {
        this.loading = false;
      });
    } else this.loading = false;
  },

  methods: {
    /** update value */
    update(newValue) {
      this.$emit("input", newValue);
    },
  },

  computed: {
    ...mapGetters(["dict"]),

    /** items for select (either dictionary or items prop) */
    citems: {
      get() {
        return this.dictionary ? this.dict(this.dictionary) : this.items;
      },
    },
  },
};
</script>
