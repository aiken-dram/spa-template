<template>
  <base-dialog-fullscreen
    dense
    v-model="show"
    :title="$t('forms.rscript.dialog')"
    @close="close"
  >
    <template v-slot:buttons>
      <v-btn text :disabled="!valid" @click.stop="save">
        {{ $t("common.save") }}
      </v-btn>
    </template>

    <v-form v-model="valid">
      <base-overlay v-model="overlay" />
      <base-modelstate v-model="modelstate" />

      <v-row>
        <v-col>
          <v-row dense>
            <v-col cols="4">
              <base-text-field
                v-model="rscript.name"
                t-label="forms.rscript.name"
                required
                rule-preset
                rule-required
                :rule-max="120"
                counter="120"
              ></base-text-field>
            </v-col>
            <v-col>
              <base-text-field
                v-model="rscript.description"
                t-label="forms.rscript.description"
                rule-preset
                :rule-max="255"
                counter="255"
              ></base-text-field>
            </v-col>
          </v-row>

          <v-row dense>
            <v-col cols="6">
              <base-text-field
                v-model="rscript.scriptFile"
                t-label="forms.rscript.scriptFile"
                required
                rule-preset
                rule-required
                :rule-max="255"
                counter="255"
              ></base-text-field>
            </v-col>
            <v-col cols="6">
              <base-text-field
                v-model="rscript.contentType"
                t-label="forms.rscript.contentType"
                required
                rule-preset
                rule-required
                :rule-max="50"
                counter="50"
              ></base-text-field>
            </v-col>
          </v-row>

          <base-text-field
            v-model="rscript.resultFile"
            t-label="forms.rscript.resultFile"
            required
            rule-preset
            rule-required
            :rule-max="255"
            counter="255"
          ></base-text-field>

          <r-script-params-form v-model="rscript.scriptParams" />
        </v-col>

        <v-col>
          <base-textarea
            v-model="rscript.scriptContent"
            t-label="forms.rscript.scriptContent"
            required
            rule-preset
            rule-required
            rows="1"
            auto-grow
          ></base-textarea>
        </v-col>
      </v-row>
    </v-form>
  </base-dialog-fullscreen>
</template>

<script>
import RScriptService from "@/api/rscript";

import BaseDialogFullscreen from "@/components/base/Dialog/DialogFullscreen";
import BaseOverlay from "@/components/base/Overlay";
import BaseModelstate from "@/components/base/Modelstate";
import BaseTextField from "@/components/base/TextField";
import BaseTextarea from "@/components/base/Textarea";

import RScriptParamsForm from "@/components/Form/RScriptParams";

/** Dialog for editing R script in statistics admin */
export default {
  name: "RScriptDialog",

  data() {
    return {
      rscript: {
        scriptParams: [],
      },
      show: false,
      valid: true,
      overlay: false,
      modelstate: {},
    };
  },

  methods: {
    close() {
      this.show = false;
    },
    open(id) {
      if (id) {
        //edit existing rscript
        this.show = true;
        this.modelstate = {};
        this.overlay = true;
        RScriptService.rscript
          .get(id)
          .then(({ data }) => {
            //console.log(data);
            this.rscript = data;
          })
          .catch((error) => {
            this.$root.$error(error);
            this.show = false;
          })
          .finally(() => {
            this.overlay = false;
          });
      } else {
        //new rscript
        this.modelstate = {};
        this.rscript = {
          scriptParams: [],
        };
        this.show = true;
      }
    },
    save() {
      RScriptService.rscript
        .upsert(this.rscript)
        .then(() => {
          this.modelstate = {};
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
    BaseDialogFullscreen,
    BaseOverlay,
    BaseModelstate,
    BaseTextField,
    BaseTextarea,
    RScriptParamsForm,
  },
};
</script>

<style></style>
