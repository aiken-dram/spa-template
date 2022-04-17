<template>
  <v-stepper v-model="step" vertical>
    <v-stepper-step :complete="step > 1" step="1">
      {{ $t("admin.access.file.selectFile") }}
    </v-stepper-step>

    <v-stepper-content step="1">
      <v-col cols="6">
        <v-overlay :value="overlay">
          <v-progress-circular indeterminate size="64"></v-progress-circular>
        </v-overlay>
        <v-alert type="error" v-show="modelstate['Error']">
          {{ modelstate["Error"] }}
        </v-alert>
        <v-form ref="formUpload" v-model="valid" class="ml-2">
          <v-file-input
            ref="fileUpload"
            v-model="file"
            :rules="[(v) => !!v || $t('admin.access.file.selectFileToUpload')]"
            accept=".txt,.csv"
            :label="$t('admin.access.file.textFile')"
            persistent-hint
            :hint="$t('admin.access.file.fileHint')"
          >
            <template v-slot:message="{ message }">
              <span v-html="message"></span>
            </template>
            <template v-slot:append-outer>
              <v-btn
                color="primary"
                :disabled="!valid"
                @click.stop="uploadFile"
              >
                {{ $t("common.process") }}
              </v-btn>
            </template>
          </v-file-input>
        </v-form>
      </v-col>
    </v-stepper-content>
    <v-stepper-step :complete="step > 2" step="2">
      {{ $t("admin.access.file.processResult") }}
    </v-stepper-step>

    <v-stepper-content step="2">
      <v-list flat>
        <v-list-item v-for="p in process" :key="p.message" color="p.state">
          <v-list-item-icon v-if="p.state == 'success'">
            <v-icon color="success">fa-check</v-icon>
          </v-list-item-icon>
          <v-list-item-icon v-if="p.state == 'error'">
            <v-icon color="error">fa-exclamation-triangle</v-icon>
          </v-list-item-icon>
          <div v-html="p.message"></div>
        </v-list-item>
      </v-list>
      <v-btn color="primary" @click="reset()">Закрыть</v-btn>
    </v-stepper-content>
  </v-stepper>
</template>

<script>
import { UserService } from "@/plugins/api";

export default {
  name: "AccessFile",

  data: () => ({
    step: 1,
    valid: false,
    modelstate: {},
    overlay: false,
    file: null,

    process: [],
  }),

  methods: {
    uploadFile() {
      this.overlay = true;
      let formData = new FormData();
      formData.append("file", this.file);
      UserService.upload(formData)
        .then((response) => {
          this.process = response.data.items;
          this.step = 2;
        })
        .catch((error) => {
          this.modelstate = error;
        })
        .finally(() => {
          this.overlay = false;
        });
    },
    reset() {
      this.file = null;
      this.modelstate = {};
      this.process = [];
      this.step = 1;
    },
  },
};
</script>
