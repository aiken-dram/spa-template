<template>
  <v-form v-model="valid">
    <base-overlay v-model="overlay" />
    <base-modelstate v-model="modelstate" />
    <v-row>
      <v-col cols="12" sm="6">
        <base-text-field
          v-model="user.login"
          required
          t-label="forms.user.login"
          rule-preset
          rule-required
          :rule-max="20"
          counter="20"
        ></base-text-field>

        <base-text-field
          v-if="isNew"
          v-model="user.password"
          required
          t-label="forms.user.password"
          rule-preset
          rule-required
          autocomplete="new-password"
        ></base-text-field>

        <base-text-field
          v-else
          v-model="user.password"
          t-label="forms.user.password"
          autocomplete="new-password"
          t-hint="forms.user.passwordHint"
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
          v-model="user.name"
          t-label="$t('forms.user.userName')"
          required
          rule-preset
          rule-required
          :rule-max="120"
          counter="120"
        ></base-text-field>

        <base-text-field
          v-model="user.description"
          t-label="forms.user.description"
          rule-preset
          :rule-max="255"
          counter="255"
        ></base-text-field>
      </v-col>

      <v-col cols="12" sm="6">
        <base-select-multiple
          v-model="user.groups"
          t-label="forms.user.groups"
          :dictionary="DICT.AccessGroups"
        ></base-select-multiple>

        <base-select-multiple
          v-model="user.roles"
          t-label="forms.user.roles"
          :dictionary="DICT.AccessRoles"
          t-hint="forms.user.rolesHint"
        ></base-select-multiple>

        <base-select-multiple
          v-model="user.districts"
          t-label="forms.user.districts"
          :dictionary="DICT.Districts"
          deletable-chips
          t-hint="forms.user.districtsHint"
        ></base-select-multiple>
      </v-col>
    </v-row>
  </v-form>
</template>

<script>
import { DICT } from "@/common/config";
import UserService from "@/api/user";

import BaseOverlay from "@/components/base/Overlay";
import BaseModelstate from "@/components/base/Modelstate";
import BaseCheckbox from "@/components/base/Checkbox";
import BaseTextField from "@/components/base/TextField";
import BaseDatePicker from "@/components/base/DateTime/DatePicker";
import BaseSelectMultiple from "@/components/base/SelectMultiple";

/** Form for user in account admin */
export default {
  name: "UserForm",

  props: {
    value: Boolean,
    id: Number,
  },

  data: () => ({
    overlay: false,
    modelstate: {},

    user: {},
  }),

  methods: {
    close() {
      this.modelstate = {};
    },

    create() {
      this.user = {};
      this.modelstate = {};
    },

    open() {
      this.overlay = true;
      return UserService.get(this.id)
        .then(({ data }) => {
          this.user = data;
          this.modelstate = {};
          this.overlay = false;
        })
        .catch((error) => {
          this.$root.$error(error);
          this.$emit("close");
        });
    },

    save() {
      this.overlay = true;
      UserService.upsert(this.user)
        .then(() => {
          this.modelstate = {};
          this.$emit("refresh");
          this.$emit("close");
          this.$root.$message(this.$i18n.t("common.editSaved"), "success");
        })
        .catch((error) => {
          this.modelstate = error;
        })
        .finally(() => {
          this.overlay = false;
        });
    },
  },

  beforeCreate() {
    //add imported objects
    this.DICT = DICT;
  },

  mounted() {
    if (this.id != null) this.open();
    else this.create();
  },

  computed: {
    /** get and set value */
    valid: {
      get() {
        return this.value;
      },
      set(value) {
        this.$emit("input", value);
      },
    },

    isNew() {
      return !this.user.idUser;
    },
  },

  components: {
    BaseOverlay,
    BaseModelstate,
    BaseCheckbox,
    BaseTextField,
    BaseDatePicker,
    BaseSelectMultiple,
  },
};
</script>

<style></style>
