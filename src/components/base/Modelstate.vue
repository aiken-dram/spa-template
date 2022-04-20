<template>
  <div v-bind="$attrs">
    <div v-for="(err, i) in errors" :key="i">
      <div v-if="Array.isArray(value[err])">
        <v-alert
          v-for="(e, j) in value[err]"
          :key="j"
          type="error"
          class="mt-4"
        >
          {{ e }}
        </v-alert>
      </div>
      <div v-else>
        <v-alert type="error" class="mt-4">
          {{ value[err] }}
        </v-alert>
      </div>
    </div>
  </div>
</template>

<script>
/**
 * Base modelstate component
 */
export default {
  name: "BaseModelstate",

  /** attrs are passed to child component */
  inheritAttrs: false,

  props: {
    /** modelstate value */
    value: Object,
  },

  computed: {
    errors() {
      return Object.keys(this.value);
    },
  },
};
</script>
