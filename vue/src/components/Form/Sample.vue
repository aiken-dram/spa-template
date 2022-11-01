<template>
  <v-form v-model="valid">
    <base-overlay v-model="overlay" />
    <base-modelstate v-model="modelstate" />

    <v-row>
      <v-col cols="8">
        <v-row dense>
          <v-col>
            <base-select
              v-model="sample.idDistrict"
              t-label="forms.sample.district"
              :dictionary="DICT.Districts"
              clearable
            ></base-select>
          </v-col>

          <v-col>
            <base-select
              v-model="sample.idType"
              t-label="forms.sample.type"
              :dictionary="DICT.SampleTypes"
              rule-preset
              rule-required
            ></base-select>
          </v-col>

          <v-col>
            <base-select
              v-model="sample.idDict"
              t-label="forms.sample.dict"
              :dictionary="DICT.SampleDicts"
              rule-preset
              rule-required
            ></base-select>
          </v-col>
        </v-row>

        <v-row dense>
          <v-col>
            <base-number-field
              v-model.number="sample.number"
              t-label="forms.sample.number"
            ></base-number-field>
          </v-col>

          <v-col>
            <base-date-picker
              v-model="sample.date"
              t-label="forms.sample.date"
            ></base-date-picker>
          </v-col>

          <v-col>
            <base-text-field
              v-model="sample.text"
              t-label="forms.sample.text"
              rule-preset
              :rule-max="120"
              counter="120"
            ></base-text-field>
          </v-col>
        </v-row>
      </v-col>

      <v-col>
        <sample-children-form
          v-model="sample.sampleChildren"
        ></sample-children-form>
      </v-col>
    </v-row>
  </v-form>
</template>

<script>
import { DICT } from "@/common/config";
import SampleService from "@/api/sample";

import SampleChildrenForm from "@/components/Form/SampleChildren";

import BaseOverlay from "@/components/base/Overlay";
import BaseModelstate from "@/components/base/Modelstate";
import BaseTextField from "@/components/base/TextField";
import BaseNumberField from "@/components/base/NumberField";
import BaseSelect from "@/components/base/Select";
import BaseDatePicker from "@/components/base/DateTime/DatePicker";

/** Form for sample */
export default {
  name: "SampleForm",

  props: {
    value: Boolean,
    id: Number,
  },

  data: () => ({
    overlay: false,
    modelstate: {},

    sample: {
      sampleChildren: [],
    },
  }),

  methods: {
    close() {
      this.modelstate = {};
    },

    create() {
      this.sample = {
        sampleChildren: [],
      };
      this.modelstate = {};
    },

    open() {
      this.overlay = true;
      return SampleService.get(this.id)
        .then(({ data }) => {
          this.sample = data;
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
      SampleService.upsert(this.sample)
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
  },

  components: {
    SampleChildrenForm,
    BaseOverlay,
    BaseModelstate,
    BaseTextField,
    BaseNumberField,
    BaseSelect,
    BaseDatePicker,
  },
};
</script>

<style></style>
