<template>
  <div>
    <v-card v-for="(p, i) in val" :key="i" class="mt-2">
      <v-toolbar flat color="grey lighten-4" dense>
        {{ $t("forms.rscriptparam.param") }}
        <v-spacer></v-spacer>

        <v-btn icon color="error" @click="del(p.id)">
          <v-icon>fa-times-circle</v-icon>
        </v-btn>
      </v-toolbar>

      <v-card-text>
        <v-row dense>
          <v-col cols="4">
            <base-text-field
              v-model="p.name"
              t-label="forms.rscriptparam.name"
              required
              rule-preset
              rule-required
              :rule-max="120"
              counter="120"
            ></base-text-field>
          </v-col>
          <v-col>
            <base-text-field
              v-model="p.description"
              t-label="forms.rscriptparam.description"
              rule-preset
              :rule-max="255"
              counter="255"
            ></base-text-field>
          </v-col>
        </v-row>
        <v-row dense>
          <v-col cols="4">
            <base-select
              v-model="p.idType"
              t-label="forms.rscriptparam.type"
              :dictionary="DICT.RScriptParamTypes"
              rule-preset
              rule-required
            ></base-select>
          </v-col>
          <v-col>
            <base-text-field
              v-model="p.hint"
              t-label="forms.rscriptparam.hint"
              rule-preset
              :rule-max="255"
              counter="255"
            ></base-text-field>
          </v-col>
        </v-row>
      </v-card-text>
    </v-card>

    <v-btn color="primary" @click="add()" class="mt-2">
      <v-icon left>fa-plus</v-icon> {{ $t("common.add") }}
    </v-btn>
  </div>
</template>

<script>
import { DICT } from "@/common/config";

import BaseTextField from "@/components/base/TextField";
import BaseSelect from "@/components/base/Select";

export default {
  name: "RScriptParamsForm",

  props: {
    value: Array,
  },

  methods: {
    add() {
      var maxId =
        this.val.length > 0
          ? Math.max.apply(
              Math,
              this.val.map((p) => p.id)
            )
          : 0;
      var p = {
        id: maxId + 1,
        isNew: true,
      };
      this.val.push(p);
      this.$nextTick(() => {
        this.$emit("validate");
      });
    },
    del(nn) {
      //console.log(nn);
      for (var i = 0; i < this.val.length; i++) {
        if (this.val[i].id === nn) {
          this.val.splice(i, 1);
        }
      }
    },
  },

  beforeCreate() {
    this.DICT = DICT;
  },

  computed: {
    /** get and set value */
    val: {
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
    BaseSelect,
  },
};
</script>

<style></style>
