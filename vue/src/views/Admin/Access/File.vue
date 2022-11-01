<template>
  <v-stepper v-model="step" vertical>
    <v-stepper-step :complete="step > 1" step="1">
      {{ $t("admin.access.file.selectFile") }}
    </v-stepper-step>

    <v-stepper-content step="1">
      <v-col cols="6">
        <v-overlay :value="overlay">
          <v-progress-circular indeterminate size="64" />
        </v-overlay>
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
              <span v-html="message" />
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
      <base-signal-r
        :api="uploadFileApi"
        :subject="SUBJECTS.UserProcessFile"
        ref="UploadFileSignalR"
      >
        <template v-slot:finished>
          <v-btn color="primary" @click="reset()">
            {{ $t("common.close") }}
          </v-btn>
        </template>
      </base-signal-r>
    </v-stepper-content>
  </v-stepper>
</template>

<script>
import { SUBJECTS } from "@/common/config";
import UserService from "@/api/user";
import BaseSignalR from "@/components/base/SignalR";

export default {
  name: "AccessFile",

  data: () => ({
    step: 1,
    valid: false,
    overlay: false,
    file: null,

    process: [],
  }),

  methods: {
    /** upload file */
    uploadFile() {
      this.step = 2;
      this.$refs.UploadFileSignalR.start();
    },
    /** reset form */
    reset() {
      this.file = null;
      this.modelstate = {};
      this.process = [];
      this.step = 1;
    },

    /** upload file api */
    uploadFileApi(idConnection) {
      let formData = new FormData();
      formData.append("file", this.file);
      formData.append("idConnection", idConnection);
      return UserService.upload(formData);
    },
  },

  created() {
    this.SUBJECTS = SUBJECTS;
  },

  components: {
    BaseSignalR,
  },
};
</script>
