<template>
  <div>
    <header>{{ label }}</header>
    <v-progress-circular v-if="loading" indeterminate></v-progress-circular>
    <v-radio-group v-else v-model="val" v-bind="$attrs" :readonly="!isEditable">
      <v-radio
        v-for="(d, i) in citems"
        :key="i"
        :label="d.text"
        :value="d.value"
      >
      </v-radio>
    </v-radio-group>
  </div>
</template>

<script>
import { mapGetters } from "vuex";
import store from "@/store";
import { DICT_FETCH } from "@/store/actions.type";

/**
 * Base radio group component
 */
export default {
  name: "BaseRadioGroup",

  /** attrs are passed to child component */
  inheritAttrs: false,

  props: {
    /** raio group value */
    value: [String, Number],

    /** radio group label */
    label: String,

    /** (optional) radio group dictionary name */
    dictionary: String,

    /** (optional) raio group items */
    items: Array,

    /** radio group is editable */
    isEditable: {
      type: Boolean,
      default: true,
    },
  },

  data: () => ({
    /** dictionary is being loaded from api */
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

  computed: {
    ...mapGetters(["dict"]),

    /** items for radio group (either dictionary or items prop) */
    citems: {
      get() {
        return this.dictionary ? this.dict(this.dictionary) : this.items;
      },
    },

    /** get and set value */
    val: {
      get() {
        return this.value;
      },
      set(newValue) {
        this.$emit("input", newValue);
      },
    },
  },
};
</script>
