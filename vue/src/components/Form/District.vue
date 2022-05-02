<template>
  <v-row>
    <v-col>
      <base-text-field
        required
        :label="$t('forms.district.idDistrict')"
        type="number"
        :rules="[
          (v) =>
            !!v ||
            $t('forms.common.mustNotBeEmpty', {
              field: $t('forms.district.idDistrict'),
            }),
        ]"
        v-model.number="district.idDistrict"
      ></base-text-field>

      <base-text-field
        required
        :label="$t('forms.district.name')"
        :rules="[
          (v) =>
            !!v ||
            $t('forms.common.mustNotBeEmpty', {
              field: $t('forms.district.name'),
            }),
          (v) =>
            (v || '').length <= 200 ||
            $t('forms.common.maxLength', { length: '200' }),
        ]"
        counter="200"
        v-model="district.name"
      ></base-text-field>
    </v-col>
  </v-row>
</template>

<script>
import BaseTextField from "@/components/base/TextField";

export default {
  name: "DistrictForm",

  props: {
    value: Object,
  },

  beforeCreate() {
    //add imported objects
  },

  computed: {
    /** get and set value */
    district: {
      get() {
        return this.value;
      },
      set(value) {
        this.$emit("input", value);
      },
    },

    isNew() {
      return !this.value.idDistrict;
    },
  },

  components: {
    BaseTextField,
  },
};
</script>

<style></style>
