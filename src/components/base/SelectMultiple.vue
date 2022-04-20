<template>
  <v-select
    :value="value"
    @input="update"
    :items="dict(dictionary)"
    :loading="loading"
    placeholder=" "
    persistent-placeholder
    multiple
    chips
    v-bind="$attrs"
  ></v-select>
</template>

<script>
import { mapGetters } from "vuex";
import store from "@/store";
import { DICT_FETCH } from "@/store/actions.type";

export default {
  name: "BaseSelectMultiple",

  /** attrs are passed to child component */
  inheritAttrs: false,

  props: {
    /** select multiple value */
    value: Array,

    /** select multiple dictionary name */
    dictionary: String,
  },

  data: () => ({
    /** dictionary is being loaded from api */
    loading: true,
  }),

  mounted() {
    //fetch dictionary from api
    store.dispatch(DICT_FETCH, this.dictionary).then(() => {
      this.loading = false;
    });
  },

  methods: {
    /** update value */
    update(newValue) {
      this.$emit("input", newValue);
    },
  },

  computed: {
    ...mapGetters(["dict"]),
  },
};
</script>
