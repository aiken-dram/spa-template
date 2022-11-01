<template>
  <v-form v-model="valid">
    <base-overlay v-model="overlay" />
    <base-modelstate v-model="modelstate" />
    <v-row>
      <v-col>
        <base-text-field
          v-model="dict.dict"
          required
          t-label="forms.sampleDict.dict"
          rule-preset
          rule-required
          :rule-max="120"
          counter="120"
        ></base-text-field>

        <base-text-field
          v-model="dict.description"
          t-label="forms.sampleDict.description"
          rule-preset
          :rule-max="255"
          counter="255"
        ></base-text-field>
      </v-col>
    </v-row>
  </v-form>
</template>

<script>
import { SampleDictService } from "@/api/dictionary";

import BaseOverlay from "@/components/base/Overlay";
import BaseModelstate from "@/components/base/Modelstate";
import BaseTextField from "@/components/base/TextField";

/** Form for sample dictionary */
export default {
  name: "SampleDictForm",

  props: {
    value: Boolean,
    item: Object,
  },

  data: () => ({
    overlay: false,
    modelstate: {},

    dict: {},
  }),

  methods: {
    close() {
      this.modelstate = {};
    },

    create() {
      this.dict = {};
      this.modelstate = {};
    },

    open() {
      //direct open, no api calls
      this.dict = this.item;
      this.modelstate = {};
    },

    save() {
      this.overlay = true;
      SampleDictService.upsert(this.dict)
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
    this.open();
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
    BaseTextField,
    BaseOverlay,
    BaseModelstate,
  },
};
</script>

<style></style>
