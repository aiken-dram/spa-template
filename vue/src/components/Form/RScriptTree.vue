<template>
  <v-card flat>
    <v-form v-show="show" v-model="valid">
      <base-overlay v-model="overlay" />
      <base-modelstate v-model="modelstate" />
      <r-script-dialog ref="RScriptDialog" />

      <v-row>
        <v-col cols="4">
          <base-text-field
            v-model="node.name"
            t-label="forms.rscripttree.name"
            required
            rule-preset
            rule-required
            :rule-max="120"
            counter="120"
          ></base-text-field>
        </v-col>
        <v-col>
          <base-text-field
            v-model="node.description"
            t-label="forms.rscripttree.description"
            rule-preset
            :rule-max="255"
            counter="255"
          ></base-text-field>
        </v-col>
      </v-row>

      <base-text-field
        v-model="node.modules"
        t-label="forms.rscripttree.modules"
        rule-preset
        :rule-max="255"
        counter="255"
      ></base-text-field>

      <v-row>
        <v-col>
          <base-text-field
            v-model="node.icon"
            t-label="forms.rscripttree.icon"
            rule-preset
            :rule-max="50"
            counter="50"
          ></base-text-field>
        </v-col>
        <v-col>
          <base-text-field
            v-model="node.color"
            t-label="forms.rscripttree.color"
            rule-preset
            :rule-max="50"
            counter="50"
          ></base-text-field>
        </v-col>
      </v-row>

      <v-row no-gutters>
        <v-col>
          <v-btn v-show="showRScript" color="primary" @click="editRScript">
            {{ $t("forms.rscripttree.rscript") }}
          </v-btn>
        </v-col>
      </v-row>

      <v-btn class="mt-4" :disabled="!valid" @click="save">
        {{ $t("common.save") }}
      </v-btn>
    </v-form>
  </v-card>
</template>

<script>
import RScriptService from "@/api/rscript";

import BaseOverlay from "@/components/base/Overlay";
import BaseModelstate from "@/components/base/Modelstate";
import BaseTextField from "@/components/base/TextField";

import RScriptDialog from "@/components/Dialog/RScript";

/** Form for R script tree node in statistics admin */
export default {
  name: "RScriptTreeForm",

  data: () => ({
    show: false,
    valid: true,
    overlay: false,
    modelstate: {},

    item: {},
    showRScript: false,
    node: {},
  }),

  methods: {
    add(item) {
      this.overlay = true;
      this.showRScript = false;
      this.item = item;
      this.modelstate = {};
      this.node = {
        idParent: item ? item.id : null,
      };
      this.overlay = false;
      this.show = true;
    },

    edit(item) {
      this.overlay = true;
      this.item = item;
      RScriptService.tree
        .get(item.id)
        .then(({ data }) => {
          this.node = data;
          this.modelstate = {};
          this.showRScript = this.item.children ? false : true;
          this.show = true;
        })
        .catch((error) => {
          this.$root.$error(error);
        })
        .finally(() => {
          this.overlay = false;
        });
    },

    editRScript() {
      //open dialog with rscript
      this.$refs.RScriptDialog.open(this.node.idRScript);
    },

    save() {
      this.overlay = true;
      RScriptService.tree
        .upsert(this.node)
        .then(() => {
          this.modelstate = {};
          this.$emit("save");
          this.show = false;
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

  components: {
    BaseOverlay,
    BaseModelstate,
    BaseTextField,
    RScriptDialog,
  },
};
</script>

<style></style>
