<template>
  <v-form v-model="valid">
    <base-overlay v-model="overlay" />
    <base-modelstate v-model="modelstate" />
    <v-row>
      <v-col>
        <base-number-field
          v-model.number="district.idDistrict"
          t-label="$t('forms.district.idDistrict')"
          required
          rule-preset
          rule-required
        ></base-number-field>

        <base-text-field
          v-model="district.name"
          t-label="forms.district.name"
          required
          rule-preset
          rule-required
          :rule-max="200"
          counter="200"
        ></base-text-field>
      </v-col>
    </v-row>
  </v-form>
</template>

<script>
import { DistrictService } from "@/api/dictionary";

import BaseOverlay from "@/components/base/Overlay";
import BaseModelstate from "@/components/base/Modelstate";
import BaseNumberField from "@/components/base/NumberField";
import BaseTextField from "@/components/base/TextField";

/** district form in dictionary */
export default {
  name: "DistrictForm",

  props: {
    value: Boolean,
    id: Number,
  },

  data: () => ({
    overlay: false,
    modelstate: {},

    district: {},
  }),

  methods: {
    close() {
      this.modelstate = {};
    },

    create() {
      this.district = {};
      this.modelstate = {};
    },

    open() {
      this.overlay = true;
      return DistrictService.get(this.id)
        .then(({ data }) => {
          this.district = data;
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
      DistrictService.upsert(this.district)
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
  },

  components: {
    BaseOverlay,
    BaseModelstate,
    BaseNumberField,
    BaseTextField,
  },
};
</script>

<style></style>
