<template>
  <v-row>
    <v-col cols="12" sm="6">
      <base-text-field
        required
        :label="$t('forms.user.login')"
        :rules="[
          (v) =>
            !!v ||
            $t('forms.common.mustNotBeEmpty', {
              field: $t('forms.user.login'),
            }),
          (v) =>
            (v || '').length <= 20 ||
            $t('forms.common.maxLength', { length: '20' }),
        ]"
        counter="20"
        v-model="user.login"
      ></base-text-field>

      <base-text-field
        v-if="isNew"
        required
        :label="$t('forms.user.password')"
        v-model="user.password"
        :rules="[
          (v) =>
            !!v ||
            $t('forms.common.mustNotBeEmpty', {
              field: $t('forms.user.password'),
            }),
        ]"
        autocomplete="new-password"
      ></base-text-field>

      <base-text-field
        v-else
        :label="$t('forms.user.password')"
        v-model="user.password"
        autocomplete="new-password"
        :hint="$t('forms.user.passwordHint')"
        persistent-hint
      ></base-text-field>

      <v-row no-gutters>
        <v-col cols="6">
          <base-checkbox
            :label="$t('forms.user.isActive')"
            v-model="user.isActive"
          ></base-checkbox>
        </v-col>

        <v-col cols="6">
          <base-date-picker
            :label="$t('forms.user.passwordExpirationDate')"
            v-model="user.passDate"
          ></base-date-picker>
        </v-col>
      </v-row>

      <base-text-field
        :label="$t('forms.user.userName')"
        required
        v-model="user.name"
        :rules="[
          (v) =>
            !!v ||
            $t('forms.common.mustNotBeEmpty', {
              field: $t('forms.user.userName'),
            }),
          (v) =>
            (v || '').length <= 120 ||
            $t('forms.common.maxLength', { length: '120' }),
        ]"
        counter="120"
      ></base-text-field>

      <base-text-field
        :label="$t('forms.user.description')"
        v-model="user.desc"
        :rules="[
          (v) =>
            (v || '').length <= 255 ||
            $t('forms.common.maxLength', { length: '255' }),
        ]"
        counter="255"
      ></base-text-field>
    </v-col>

    <v-col cols="12" sm="6">
      <base-select-multiple
        :label="$t('forms.user.groups')"
        v-model="user.groups"
        :dictionary="DICT.AccessGroups"
      ></base-select-multiple>

      <base-select-multiple
        :label="$t('forms.user.roles')"
        v-model="user.roles"
        :dictionary="DICT.AccessRoles"
        :hint="$t('forms.user.rolesHint')"
      ></base-select-multiple>
    </v-col>
  </v-row>
</template>

<script>
import { DICT } from "@/common/config";

import BaseCheckbox from "@/components/base/Checkbox";
import BaseTextField from "@/components/base/TextField";
import BaseDatePicker from "@/components/base/DatePicker";
import BaseSelectMultiple from "@/components/base/SelectMultiple";

export default {
  name: "UserForm",

  props: {
    value: Object,
  },

  beforeCreate() {
    //add imported objects
    this.DICT = DICT;
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

    isNew() {
      return !this.value.idUser;
    },
  },

  components: {
    BaseCheckbox,
    BaseTextField,
    BaseDatePicker,
    BaseSelectMultiple,
  },
};
</script>

<style></style>
