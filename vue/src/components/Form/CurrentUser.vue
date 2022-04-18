<template>
  <v-container class="py-0">
    <v-row>
      <v-col cols="12">
        <base-text-field
          :label="$t('profile.userName')"
          v-model="user.name"
          :rules="[
            (v) =>
              (v || '').length <= 120 ||
              $t('forms.common.maxLength', { length: '120' }),
          ]"
          counter="120"
        ></base-text-field>
      </v-col>

      <v-col cols="12">
        <base-text-field
          :label="$t('profile.desc')"
          v-model="user.description"
          :rules="[
            (v) =>
              (v || '').length <= 255 ||
              $t('forms.common.maxLength', { length: '255' }),
          ]"
          counter="255"
        ></base-text-field>
      </v-col>

      <slot></slot>
    </v-row>
  </v-container>
</template>

<script>
import BaseTextField from "@/components/base/TextField";

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
