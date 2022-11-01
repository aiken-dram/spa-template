<template>
  <v-container class="py-0">
    <v-row>
      <v-col cols="12">
        <base-text-field
          v-model="user.name"
          t-label="profile.userName"
          rule-preset
          :rule-max="120"
          counter="120"
        ></base-text-field>
      </v-col>

      <v-col cols="12">
        <base-text-field
          v-model="user.description"
          t-label="profile.desc"
          rule-preset
          :rule-max="255"
          counter="255"
        ></base-text-field>
      </v-col>

      <slot></slot>
    </v-row>
  </v-container>
</template>

<script>
import BaseTextField from "@/components/base/TextField";

/** Current user form in user's profile */
export default {
  name: "CurrentUserForm",

  props: {
    value: Object,
  },

  computed: {
    /** get and set value */
    user: {
      get() {
        return this.value;
      },
      set(value) {
        this.$emit("input", value);
      },
    },
  },

  components: {
    BaseTextField,
  },
};
</script>

<style></style>
