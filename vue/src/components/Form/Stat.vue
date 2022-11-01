<template>
  <v-card flat>
    <v-card-title> {{ node.name }}</v-card-title>

    <v-progress-linear
      v-if="loading"
      color="deep-purple accent-4"
      indeterminate
      rounded
      height="6"
    ></v-progress-linear>

    <v-stepper v-else v-model="step" vertical>
      <!-- STEP 1 -->
      <v-stepper-step :complete="step > 1" step="1">
        {{ $t("stat.creatingRequest") }}
      </v-stepper-step>

      <v-stepper-content step="1">
        <base-modelstate v-model="modelstate"> </base-modelstate>
        <v-form>
          <div v-for="(f, i) in script.formFields" :key="f.id">
            <div v-if="f.type == 'DICT.DISTRICT'">
              <base-select
                v-model="script.formFields[i].value"
                :label="f.name"
                :dictionary="DICT.Districts"
                class="shrink ml-2"
                clearable
                style="width: 300px"
              ></base-select>
            </div>
          </div>
        </v-form>
        <v-btn
          color="primary"
          :disabled="Object.keys(modelstate).length > 0"
          @click="createRequest()"
        >
          {{ $t("stat.createRequest") }}
        </v-btn>
      </v-stepper-content>

      <!-- STEP 2 -->
      <v-stepper-step :complete="step > 2" step="2">
        {{ $t("stat.processingRequest") }}
      </v-stepper-step>

      <v-stepper-content step="2">
        <request-process
          ref="RequestProcess"
          @on-finished="onFinished"
        ></request-process>
      </v-stepper-content>

      <!-- STEP 3 -->
      <v-stepper-step :complete="step > 3" step="3">
        {{ $t("stat.requestResult") }}
      </v-stepper-step>

      <v-stepper-content step="3">
        <request-result-preview
          ref="RequestResultPreview"
        ></request-result-preview>
      </v-stepper-content>
    </v-stepper>
  </v-card>
</template>

<script>
import { MQ, DICT } from "@/common/config";

import StatService from "@/api/stat";

import BaseSelect from "@/components/base/Select";
import BaseModelstate from "@/components/base/Modelstate";
import RequestProcess from "@/components/MessageQuery/RequestProcess";
import RequestResultPreview from "@/components/MessageQuery/RequestResultPreview";

/** Form for setting parameters for statistics request */
export default {
  name: "StatForm",

  data: () => ({
    loading: true,
    step: 1,
    node: {},
    script: {},

    form: {
      id: null,
      args: [],
    },

    modelstate: {},
  }),

  methods: {
    open(node) {
      this.step = 1;
      this.loading = true;
      this.modelstate = {};
      this.node = node;
      StatService.form(node.idRScript)
        .then(({ data }) => {
          this.script = data;
          this.form.id = data.idRScript;
        })
        .catch((error) => {
          this.modelstate = error;
        })
        .finally(() => {
          this.loading = false;
        });
    },

    createRequest() {
      //need to get form's filled values into json
      this.modelstate = {};
      this.form.args = this.script.formFields.map((p) => p.value);

      var data = {
        type: MQ.RScript,
        json: JSON.stringify(this.form),
      };
      //console.log(data);
      this.step = 2;
      this.$refs.RequestProcess.start(data);
    },

    onFinished(id) {
      //show preview result of process
      this.step = 3;
      this.$refs.RequestResultPreview.open(id);
    },
  },

  mounted() {
    this.DICT = DICT;
  },

  components: {
    BaseSelect,
    BaseModelstate,
    RequestProcess,
    RequestResultPreview,
  },
};
</script>

<style></style>
